using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelExportorForWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            UCDragElement uc = new UCDragElement();
            uc.Dock = DockStyle.Fill;
            panList.Controls.Add(uc);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {

                int tables = (int)numTables.Value;
                int cols = (int)numCols.Value;
                AddInfo("开始构造");
                var mapGroup = new MappingGroup();
                mapGroup.Name = "名称";
                mapGroup.ErrorHappened += new Action<string>(mapGroup_ErrorHappened);

                for (int i = 0; i < tables; i++)
                {
                    MapElementCollection mec = new MapElementCollection();
                    mec.Name = "table" + i % (tables + 1);

                    MappingElement ele1 = new MappingElement(
                       new ColumnElement("dept_name"), new ColumnElement("单位名称"));
                    mec.Add(ele1);

                    MappingElement ele = new MappingElement(
                        new ColumnElement("dept_id"), new ColumnElement("单位ID"));
                    mec.Add(ele);



                    mapGroup.Add(mec);
                }

                AddInfo("构造完成");
                yltl.Common.ConfigHelper<MappingGroup>.Reflesh(mapGroup);
                AddInfo("保存完成");
            }));
            th.Start();
        }

        void mapGroup_ErrorHappened(string obj)
        {
            AddInfo(obj);
        }

        private void AddInfo(string info)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                richTextBox1.AppendText(DateTime.Now.ToString("HH:mm:ss fff") + "\t" + info + "\r\n");
            }));
        }

        private Point mouse_offset;
        private void lblTestDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouse_offset = new Point(-e.X, -e.Y);//
            }
        }

        private void lblTestDrag_MouseMove(object sender, MouseEventArgs e)
        {
            var ctrl = sender as Control;
            (ctrl).Cursor = Cursors.Arrow;
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);//设置偏移
                (ctrl).Location = (ctrl).Parent.PointToClient(mousePos);
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            var lst = sender as ListBox;
            if (lst == null) return;
            //if (e.Button == MouseButtons.Left && this.lstSource.SelectedIndex >= 0)
            //{
            //    lst.DoDragDrop(this.lstSource, DragDropEffects.Copy);
            //}
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Text"))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            //object item = e.Data.GetData("Text");
            //this.lstDestionation.Items.Add(item);
            //this.lstSource.Items.Remove(item);
        }

        private void lstSource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel ee = new ExportToExcel();

            var mp = yltl.Common.ConfigHelper<MappingGroup>.ReloadFile();

            var dbo = yltl.DBUtility.DBFactory.Create(yltl.DBUtility.eDBType.Oracle, "Data Source=lan_189;User ID=user_ahdy_shi;Password=user_ahdy_shi");

            var dt = dbo.GetDataTable("select * from sys_dept");

            ee.Export(mp.Maps[0], dt);
        }
    }
}
