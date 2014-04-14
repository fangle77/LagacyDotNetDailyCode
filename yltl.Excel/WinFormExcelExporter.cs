using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yltl.ExcelHelper
{
    /// <summary>
    /// CS模式下的导出方法
    /// </summary>
    public class WinFormExcelExporter
    {
        /// <summary>
        /// 导出Excel，以旧列名-新列名词典为标头
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dicCoumnNameMapping"></param>
        public static void DataTableToExcel(System.Data.DataTable dt, Dictionary<string, string> dicCoumnNameMapping, string fileName)
        {
            ExcelHandler eh = new ExcelHandler();
            SheetExcelForm frm = new SheetExcelForm();
            eh.ProcessHandler = frm.AddProcess;
            eh.ProcessErrorHandler = new Action<string>((msg) =>
            {
                MessageBox.Show(msg);
            });
            try
            {
                frm.Show();
                Delay(20);
                var ds = new System.Data.DataSet();
                ds.Tables.Add(dt);
                eh.DataSet2Excel(ds, dicCoumnNameMapping, fileName);
                ds.Tables.Remove(dt);
                ds.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出Excel错误:" + ex.Message);
            }
            finally
            {
                Delay(20);
                frm.Close();
            }
        }

        /// <summary>
        /// 导出Excel，以旧列名-新列名词典为标头
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dicCoumnNameMapping"></param>
        public static void DataTableToExcel(System.Data.DataTable dt, Dictionary<string, string> dicCoumnNameMapping)
        {
            DataTableToExcel(dt, dicCoumnNameMapping, null);
        }

        /// <summary>
        /// 直接将DataTable内容导出为Excel，
        /// </summary>
        /// <param name="dt"></param>
        public static void DataTableToExcel(System.Data.DataTable dt)
        {
            DataTableToExcel(dt, null, null);
        }

        /// <summary>
        /// 将DataSet导出为excel，一个Table对应一个sheet
        /// </summary>
        /// <param name="ds"></param>
        public static void DataSetToExcel(System.Data.DataSet ds, string fileName)
        {
            ExcelHandler eh = new ExcelHandler();
            SheetExcelForm frm = new SheetExcelForm();
            eh.ProcessHandler = frm.AddProcess;
            eh.ProcessErrorHandler = new Action<string>((msg) =>
            {
                MessageBox.Show(msg);
            });
            try
            {
                frm.Show();
                Delay(20);
                eh.DataSet2Excel(ds, null, fileName);
                ds.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出Excel错误:" + ex.Message);
            }
            finally
            {
                Delay(20);
                frm.Close();
            }
        }

        /// <summary>
        /// 按照模板导出
        /// </summary>
        /// <param name="dt">要导出的datatable</param>
        /// <param name="templateFile">要导出的excel模板文件</param>
        /// <param name="saveFile">要保存的文件名，可以为空</param>
        public static void DataTableToExcelWithTemplate(System.Data.DataTable dt, string templateFile, string saveFile)
        {
            ExcelHandler eh = new ExcelHandler();
            SheetExcelForm frm = new SheetExcelForm();
            eh.ProcessHandler = frm.AddProcess;
            eh.ProcessErrorHandler = new Action<string>((msg) =>
            {
                MessageBox.Show(msg);
            });
            try
            {
                frm.Show();
                Delay(20);
                string tmpFile = eh.ExportWithTemplate(dt, templateFile, saveFile);

                //MessageBox.Show("导出完成");
                try
                {
                    if (!string.IsNullOrEmpty(saveFile)) System.Diagnostics.Process.Start(saveFile);
                }
                catch { }

                try
                {
                    if (!string.IsNullOrEmpty(tmpFile)) System.IO.File.Delete(tmpFile);
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出Excel错误:" + ex.Message);
            }
            finally
            {
                Delay(20);
                frm.Close();
            }
        }

        private static void Delay(int ms)
        {
            int t = System.Environment.TickCount;
            while (System.Environment.TickCount - t < ms)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}
