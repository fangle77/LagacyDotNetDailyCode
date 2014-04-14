using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelExportorForWin
{
    public partial class UCDragElement : UserControl
    {
        public UCDragElement()
        {
            InitializeComponent();

            lstDestionation.AllowDrop = true;
            lstSource.AllowDrop = true;
            lstMapping.AllowDrop = true;

            lstSource.Items.AddRange(new string[] { "station_name", "Col2", "Col3", "Col4" });

            lstDestionation.Items.AddRange(new string[] { "测点名称", "D2", "D3" });
        }

        private Dictionary<string, ColumnElement> _SourceColumns;
        private Dictionary<string, ColumnElement> _DestinationColumns;

        private string _sourceName;
        private string _destinationName;

        #region 属性

        /// <summary>
        /// 获取或设置源列名的数据表名
        /// </summary>
        public string SourceTableName
        {
            get { return _sourceName; }
            set
            {
                _sourceName = value;
                if (!string.IsNullOrEmpty(_sourceName))
                {
                    InvokeMethod(() =>
                    {
                        gpbSource.Text = string.Format("源列名-[{0}]", _sourceName);
                    });
                }
            }
        }

        /// <summary>
        /// 获取或设置目标列的数据集表名
        /// </summary>
        public string DestinationName
        {
            get { return _destinationName; }
            set
            {
                _destinationName = value;
                if (!string.IsNullOrEmpty(_destinationName))
                {
                    InvokeMethod(() =>
                    {
                        gpbDestination.Text = string.Format("目标列名-[{0}]", _destinationName);
                    });
                }
            }
        }

        /// <summary>
        /// 获取或设置当前源列中的值
        /// </summary>
        public List<ColumnElement> SourceColumns
        {
            get
            {
                return GetColumnElements(_SourceColumns, lstSource);
            }
            set
            {
                _SourceColumns = SetColumns(value, lstSource);
            }
        }

        /// <summary>
        /// 获取或设置当前目标列中的项
        /// </summary>
        public List<ColumnElement> DestinationColumns
        {
            get { return GetColumnElements(_DestinationColumns, lstDestionation); }
            set { _DestinationColumns = SetColumns(value, lstDestionation); }
        }

        /// <summary>
        /// 获取映射关系
        /// </summary>
        /// <returns></returns>
        public List<MappingElement> GetMappingColloction()
        {
            if (lstMapping.Items.Count == 0) return new List<MappingElement>();
            if (_SourceColumns == null || _SourceColumns.Count == 0
                || _DestinationColumns == null || _DestinationColumns.Count == 0)
                return new List<MappingElement>();

            List<MappingElement> list = new List<MappingElement>(lstMapping.Items.Count);
            foreach (var item in lstMapping.Items)
            {
                if (item == null) continue;
                string map = item.ToString();

                string[] maps = map.Split('-');
                if (maps.Length >= 2)
                {
                    if (_SourceColumns.ContainsKey(maps[0]) && _DestinationColumns.ContainsKey(maps[1]))
                    {
                        list.Add(new MappingElement(_SourceColumns[maps[0]], _DestinationColumns[maps[1]]));
                    }
                }
            }
            return list;

        }

        private Dictionary<string, ColumnElement> SetColumns(List<ColumnElement> value, ListBox lst)
        {
            Dictionary<string, ColumnElement> dic = null;
            InvokeMethod(() =>
            {
                lst.Items.Clear();
                lstMapping.Items.Clear();
            });

            if (value == null)
            {
                dic = null;
            }
            else
            {
                if (dic == null) dic = new Dictionary<string, ColumnElement>(value.Count);
                foreach (var item in value)
                {
                    if (dic.ContainsKey(item.Name) == false)
                    {
                        dic.Add(item.Name, item);
                    }
                }
                InvokeMethod(() =>
                {
                    lst.Items.AddRange(value.Select<ColumnElement, string>(ce =>
                    {
                        return ce.Name;
                    }).ToArray());
                });
            }
            return dic;
        }

        private List<ColumnElement> GetColumnElements(Dictionary<string, ColumnElement> dic, ListBox lst)
        {
            if (dic != null)
            {
                List<ColumnElement> list = new List<ColumnElement>(lst.Items.Count);

                foreach (var item in lst.Items)
                {
                    if (item == null) continue;
                    string name = item.ToString();
                    if (dic.ContainsKey(name))
                    {
                        list.Add(dic[name]);
                    }
                }

                return list;
            }
            return new List<ColumnElement>();
        }

        #endregion

        private void lstSource_DragEnter(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null) return;
            listbox.Tag = true;

            if (e.Data.GetDataPresent(typeof(ListBox)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lstSource_DragDrop(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null) return;

            var lst = (ListBox)e.Data.GetData(typeof(ListBox));

            Point ptScreen = new Point(e.X, e.Y);
            Point ptList = listbox.PointToClient(ptScreen);
            int index = ptList.Y / listbox.ItemHeight;

            if (lst != listbox)//不是拖放到自身
            {
                //选中到拖放到的节点
                if (index < 0) index = 0;
                else if (index > listbox.Items.Count - 1) index = listbox.Items.Count - 1;

                listbox.SelectedIndex = index;

                if (lst == lstMapping && lstMapping.SelectedItem != null)//从关联中移动过来的=删除匹配关系
                {
                    RemoveMappRelation(lstMapping.SelectedItem);
                }
                else//源移动到目标或目标移动到源=建立匹配关系
                {
                    CreateMapRelation();
                }
            }
            else if (lst == listbox)
            {
                index = MoveItemToIndex(lst, lst.SelectedItem, index);
            }
        }

        /// <summary>
        /// 移除映射关系
        /// </summary>
        private void RemoveMappRelation(object item)
        {
            if (item == null) return;
            string s = item.ToString();
            string[] items = s.Split('-');
            if (items.Length > 1)
            {
                lstSource.Items.Add(items[0]);
                lstDestionation.Items.Add(items[1]);
                lstMapping.Items.Remove(s);
            }
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int MoveItemToIndex(ListBox lst, object item, int index)
        {
            if (item != null)
            {
                lst.Items.Remove(item);

                if (index < 0) index = 0;
                else if (index > lst.Items.Count) index = lst.Items.Count;

                lst.Items.Insert(index, item);
            }
            return index;
        }

        /// <summary>
        /// 创建关联关系
        /// </summary>
        private void CreateMapRelation()
        {
            if (lstSource.SelectedItem != null && lstDestionation.SelectedItem != null)
            {
                string item = string.Format("{0}-{1}", lstSource.SelectedItem, lstDestionation.SelectedItem);
                lstSource.Items.Remove(lstSource.SelectedItem);
                lstDestionation.Items.Remove(lstDestionation.SelectedItem);
                lstMapping.Items.Add(item);
            }
        }

        private void lstSource_MouseMove(object sender, MouseEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null) return;
            if (listbox.Items.Count > 0)
            {
                int count = e.Y / listbox.ItemHeight;
                if (count >= 0 && listbox.Items.Count > count)
                {
                    listbox.SelectedIndex = count;
                }
            }
        }

        private void lstSource_MouseDown(object sender, MouseEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null) return;

            if (e.Button == MouseButtons.Left && listbox.SelectedIndex >= 0)
            {
                listbox.DoDragDrop(listbox, DragDropEffects.Copy);
            }
        }

        private void InvokeMethod(Action act)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(act);
            }
            else act();
        }
    }
}
