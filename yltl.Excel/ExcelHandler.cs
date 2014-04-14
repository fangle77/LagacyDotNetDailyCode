/*
 * create by zwb 2011.12.22
 */

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Xls = Excel;

namespace yltl.ExcelHelper
{
    /// <summary>
    /// 提供导出Excel方法
    /// </summary>
    internal partial class ExcelHandler
    {
        /// <summary>
        /// 静态变量：指示是否安装了Excel，如果是则为true，否则为false，默认null
        /// </summary>
        private static bool? _HasInstalledExcel = null;

        public Action<int, int> ProcessHandler { get; set; }

        public Action<string> ProcessErrorHandler { get; set; }

        private void AddError(string errorMsg)
        {
            if (ProcessErrorHandler != null) ProcessErrorHandler(errorMsg);
        }

        private void AddProcess(int current, int total)
        {
            if (ProcessHandler != null)
            {
                System.Windows.Forms.Application.DoEvents();
                ProcessHandler(current, total);
            }
        }

        private string NumTochr(int Num)
        {
            int n = 64 + Num;
            return "" + (Char)n;
        }

        private string NumToExeclRowStr(int Num)
        {
            int X, Y;
            if (Num < 27)
            {
                return NumTochr(Num);
            }

            X = Num / 26;
            Y = Num - X * 26;
            return NumTochr(X) + NumTochr(Y);
        }

        private string ResolveSheetName(string sheetName)
        {

            //• 确认输入的名称不多于 31 个字符。
            //• 确认名称中不包含下列任一字符: :  / ? * [ 或 ] 。
            //• 确认工作表名称不为空。
            if (string.IsNullOrEmpty(sheetName)) return sheetName;
            var reg = new System.Text.RegularExpressions.Regex(@"[/?\[\]:：*。]+");

            sheetName = reg.Replace(sheetName, "");
            if (sheetName.Length > 31)
                sheetName = sheetName.Substring(0, 31);
            return sheetName;
        }

        public void DataSet2Excel(DataSet ds, Dictionary<string, string> dicColumnNameMapping)
        {
            DataSet2Excel(ds, dicColumnNameMapping, null);
        }

        public void DataSet2Excel(DataSet ds, Dictionary<string, string> dicColumnNameMapping, string fileName)
        {
            if (ds == null || ds.Tables.Count == 0)
                return;

            if (ValidateIsInstallExcel() == false) return;

            #region 自动适应超出6万行就分Sheet
            int maxcount = 60000;
            for (int count = 0; count < ds.Tables.Count; count++)
            {

                if (ds.Tables[count].Rows.Count > maxcount)
                {
                    int zs = ds.Tables[count].Rows.Count;
                    int j = zs / maxcount;
                    int j1 = zs % maxcount;
                    if (j1 > 0)
                        j = j + 1;
                    for (int a = 0; a < j; a++)
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        for (int i = 0; i < ds.Tables[count].Columns.Count; i++)
                        {
                            System.Data.DataColumn dc = new System.Data.DataColumn(ds.Tables[count].Columns[i].ColumnName);
                            dt.Columns.Add(dc);
                        }

                        int qs = a * maxcount;
                        int js = qs + maxcount;
                        if (js > zs)
                            js = zs;
                        for (int m = qs; m < js; m++)
                        {
                            System.Data.DataRow dr = dt.NewRow();
                            for (int n = 0; n < dt.Columns.Count; n++)
                            {
                                dr[n] = ds.Tables[count].Rows[m][n];
                            }
                            dt.Rows.Add(dr);
                        }
                        dt.TableName = ds.Tables[count].TableName + (a + 1).ToString();
                        ds.Tables.Add(dt);
                    }
                    ds.Tables.Remove(ds.Tables[count]);
                    count--;

                }
            }

            #endregion 自动适应超出6万行就分Sheet

            try
            {
                int totalCount = 0;
                int currentCount = 0;
                for (int n = 0; n < ds.Tables.Count; n++)
                {
                    totalCount += ds.Tables[n].Rows.Count;
                }

                object omissing = System.Reflection.Missing.Value;
                Excel.ApplicationClass xlapp = new Excel.ApplicationClass();

                xlapp.DefaultFilePath = "";
                xlapp.DisplayAlerts = true;
                xlapp.SheetsInNewWorkbook = 1;

                Excel.Workbook xlworkbook = xlapp.Workbooks.Add(omissing);
                for (int i = ds.Tables.Count - 1; i >= 0; i--)
                {
                    System.Data.DataTable tmpDataTable = ds.Tables[i];
                    currentCount = DataTable2Excel(tmpDataTable, totalCount, currentCount, dicColumnNameMapping, omissing, xlworkbook);
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    xlworkbook.SaveCopyAs(fileName);
                    xlworkbook.Close(false, null, null);
                    xlapp.Quit();
                }
                else
                {
                    xlapp.Visible = true;
                }
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(xlworkbook);
                ReleaseExcelObject(xlworkbook, xlapp);
            }
            catch (Exception ex)
            {
                GC.Collect();
                AddError(ex.Message);
            }
        }

        private int DataTable2Excel(System.Data.DataTable dt, int totalCount, int currentCount, Dictionary<string, string> dicColumnNameMapping,
            object omissing, Excel.Workbook xlworkbook)
        {
            try
            {
                var tmpDataTable = dt.Copy();

                #region 表头
                if (dicColumnNameMapping != null)
                {
                    ResolveDatatableColumns(tmpDataTable, dicColumnNameMapping);
                }

                //var dr = tmpDataTable.NewRow();
                //for (int i = 0; i < tmpDataTable.Columns.Count; i++)
                //{
                //    dr[i] = tmpDataTable.Columns[i].ColumnName;
                //}
                //tmpDataTable.Rows.InsertAt(dr, 0);
                #endregion

                #region 快速导出数据

                int rowNum = tmpDataTable.Rows.Count;
                int columnNum = tmpDataTable.Columns.Count;
                int rowIndex = 1;
                int columnIndex = 0;

                Excel.Worksheet xlworksheet = (Excel.Worksheet)xlworkbook.Worksheets.Add(omissing, omissing, 1, omissing);
                xlworksheet.Name = tmpDataTable.TableName == null ? xlworksheet.Name : ResolveSheetName(tmpDataTable.TableName);
                int colnum = tmpDataTable.Columns.Count;
                Excel.Range r = xlworksheet.get_Range("A1", NumToExeclRowStr(colnum) + "1");

                object[] objHeader = new object[colnum];

                columnIndex = 0;//表头
                for (int j = 0; j < columnNum; j++)
                {
                    objHeader[columnIndex] = tmpDataTable.Columns[j].ColumnName;
                    columnIndex++;
                }
                r.Value2 = objHeader;

                //将DataTable中的数据导入Excel中
                for (int f = 0; f < rowNum; f++)
                {
                    rowIndex++;
                    columnIndex = 0;
                    for (int j = 0; j < columnNum; j++)
                    {
                        string danyinhao = "";//"'";
                        objHeader[columnIndex] = danyinhao + tmpDataTable.Rows[f][j].ToString();
                        columnIndex++;
                    }
                    r = xlworksheet.get_Range("A" + (f + 2), NumToExeclRowStr(colnum) + (f + 2));
                    r.Value2 = objHeader;
                    currentCount++;
                    AddProcess(currentCount, totalCount);
                }
                //r.EntireColumn.WrapText = true;
                r.EntireColumn.AutoFit();
                #endregion 快速导出数据
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
            }
            return currentCount;
        }

        private bool ValidateIsInstallExcel()
        {
            if (_HasInstalledExcel != null) return _HasInstalledExcel.Value;

            try
            {
                Xls.Application excel;
                excel = new Xls.ApplicationClass();

                if (excel == null)
                {
                    AddError("请先安装Excel!");
                    _HasInstalledExcel = false;
                }
                else
                {
                    _HasInstalledExcel = true;
                    excel.Quit();
                    excel = null;
                    GC.Collect();
                }
            }
            catch
            {
                AddError("请先安装Excel!");
                _HasInstalledExcel = false;
            }
            return _HasInstalledExcel.Value;
        }

        #region 列处理方法
        /// <summary>
        /// 处理DataTable的列：替换名称、排序等
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colOldName_colNewName_pair"></param>
        public void ResolveDatatableColumns(DataTable dt, Dictionary<string, string> colOldName_colNewName_pair)
        {
            colOldName_colNewName_pair = DicKeyToUpper(colOldName_colNewName_pair);
            colOldName_colNewName_pair = RemoveEmptyValue(colOldName_colNewName_pair);

            string newName = string.Empty;
            string tempColName = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tempColName = dt.Columns[i].ColumnName.ToUpper();
                colOldName_colNewName_pair.TryGetValue(tempColName, out newName);
                if (string.IsNullOrEmpty(newName))
                {
                    dt.Columns.RemoveAt(i);
                    i--;
                }
                else
                {
                    dt.Columns[i].ColumnName = newName;
                }
            }

            SortDatatableColumns(dt, colOldName_colNewName_pair);
        }

        /// <summary>
        /// 替换DataTable的列名为Excel中显示的名称
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="oldColName_NewColName_Dictionary"></param>
        private void ReplaceDatatableColumnName(DataTable dt, Dictionary<string, string> oldColName_NewColName_Dictionary)
        {
            if (oldColName_NewColName_Dictionary == null || oldColName_NewColName_Dictionary.Count == 0) return;

            oldColName_NewColName_Dictionary = DicKeyToUpper(oldColName_NewColName_Dictionary);
            string newName = string.Empty;
            string tempColName = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tempColName = dt.Columns[i].ColumnName.ToUpper();
                oldColName_NewColName_Dictionary.TryGetValue(tempColName, out newName);
                if (string.IsNullOrEmpty(newName))
                {
                }
                else
                {
                    dt.Columns[i].ColumnName = newName;
                }
            }
        }

        /// <summary>
        /// 排列DataTable的列，使最终导出到Excel的列顺序正确
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sortPair"></param>
        private void SortDatatableColumns(DataTable dt, Dictionary<string, string> sortPair)
        {
            int sortIndex = sortPair.Count - 1;
            foreach (string value in sortPair.Values)
            {
                if (string.IsNullOrEmpty(value) || dt.Columns[value] == null) continue;
                dt.Columns[value].SetOrdinal(sortIndex);
            }
        }

        /// <summary>
        /// 移除空值的列
        /// </summary>
        /// <param name="dic"></param>
        private Dictionary<string, string> RemoveEmptyValue(Dictionary<string, string> dic)
        {
            Dictionary<string, string> temDic = new Dictionary<string, string>();
            foreach (string key in dic.Keys)
            {
                if (string.IsNullOrEmpty(dic[key])) continue;

                temDic.Add(key, dic[key]);
            }
            return temDic;
        }

        /// <summary>
        /// 将词典的key转换为大写
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private Dictionary<string, string> DicKeyToUpper(Dictionary<string, string> dic)
        {
            Dictionary<string, string> newDic = new Dictionary<string, string>();
            foreach (string key in dic.Keys)
            {
                newDic.Add(key.ToUpper(), dic[key]);
            }
            return newDic;
        }
        #endregion
    }

    internal partial class ExcelHandler
    {
        public string ExportWithTemplate(DataTable dt, string templateFile, string saveFileName)
        {
            return ExportWithTemplate(dt, templateFile, saveFileName, true);
        }

        /// <summary>
        /// 按照模板导出
        /// </summary>
        /// <param name="dt">要导出的Datatable，需要与模板文件又相同的列名</param>
        /// <param name="templateFile">模板文件</param>
        /// <param name="saveFileName">保存的文件名</param>
        /// <param name="isShowExcel">当保存的文件名为空时是否显示excle</param>
        public string ExportWithTemplate(DataTable dt, string templateFile, string saveFileName, bool isShowExcel)
        {
            if (ValidateIsInstallExcel() == false) return "";

            string templateFileCopy = CopyTemplateFile(templateFile);
            if (!string.IsNullOrEmpty(templateFileCopy))
            {
                templateFile = templateFileCopy;
            }

            Excel.ApplicationClass xlapp;
            Excel.Workbook book;
            OpenTemplateExcel(templateFile, out xlapp, out book);

            if (book.Worksheets.Count < 1)
            {
                AddError(string.Format("模板文件中没有Sheet", templateFile));

                book.Close(false, null, null);
                xlapp.Quit();

                ReleaseExcelObject(book, xlapp);
                return "";
            }


            //复制模板
            object omissing = System.Reflection.Missing.Value;
            var sheet = (Excel.Worksheet)(book.Worksheets)[1];

            int headRowRowIndex = -1;//列标题所在的行索引
            Dictionary<string, int> dicColName_Index;
            MapColumns(dt, sheet, out headRowRowIndex, out dicColName_Index);

            if (headRowRowIndex == -1)//没有找到对应的标题
            {
                AddError(string.Format("从模板文件\"{0}\"中没有发现需要的列标头", templateFile));

                book.Close(false, omissing, omissing);
                xlapp.Quit();

                ReleaseExcelObject(sheet, book, xlapp);
                return "";
            }

            //开始导出到Excel
            if (dt.Rows.Count == 0)
            {
                xlapp.Quit();
                AddError("没有数据可以导出");
                AddProcess(1, 1);
                return "";
            }

            int startRowIndex = headRowRowIndex + 1;
            int total = dt.Rows.Count;
            int current = 0;
            for (int i = 0; i < dt.Rows.Count; i++)//行
            {
                int rowIndex = startRowIndex + i;
                for (int j = 0; j < dt.Columns.Count; j++)//列
                {
                    if (dicColName_Index.ContainsKey(dt.Columns[j].ColumnName) == false) continue;

                    int colIndex = dicColName_Index[dt.Columns[j].ColumnName];
                    Excel.Range range = (Excel.Range)sheet.Cells[rowIndex, colIndex];
                    range.Value2 = dt.Rows[i][j];
                }

                AddProcess(current++, total);
            }

            if (!string.IsNullOrEmpty(saveFileName))
            {
                book.SaveAs(saveFileName, omissing, omissing, omissing, omissing, omissing, Excel.XlSaveAsAccessMode.xlNoChange, omissing, omissing, omissing, omissing, omissing);
                book.Close(false, omissing, omissing);
                xlapp.Quit();
            }
            else
            {
                if (isShowExcel) xlapp.Visible = true;
                //book.Close(false, omissing, omissing);
                else
                {
                    book.Close(true, omissing, omissing);
                    xlapp.Quit();
                }
            }

            ReleaseExcelObject(sheet, book, xlapp);
            return templateFileCopy;
        }

        private static void MapColumns(DataTable dt, Excel.Worksheet sheet, out int headRowRowIndex, out Dictionary<string, int> dicColName_Index)
        {
            //定位开始行及各列对应的列索引 
            //100行100列之内，否则，遍历的太多了
            int columnCount = 50 > sheet.UsedRange.Columns.Count ? sheet.UsedRange.Columns.Count : 50; //sheet.UsedRange.Columns.Count;
            int ur = 50 > sheet.UsedRange.Rows.Count ? sheet.UsedRange.Rows.Count : 50; //sheet.UsedRange.Rows.Count;

            headRowRowIndex = -1;//标题开始的行索引
            dicColName_Index = new Dictionary<string, int>();
            for (int r = 1; r < ur; r++)
            {
                bool foundedHeadRow = false;
                for (int c = 1; c <= columnCount; c++)
                {
                    var range = (Excel.Range)(sheet.Cells[r, c]);//取一个单元格

                    if (range.Value2 == null) continue;
                    string colName = range.Value2.ToString().Trim();//列名

                    if (dt.Columns.Contains(colName))
                    {
                        foundedHeadRow = true;
                        headRowRowIndex = r;
                        if (dicColName_Index.ContainsKey(colName) == false)
                        {
                            dicColName_Index.Add(colName, c);
                        }
                    }
                }
                if (foundedHeadRow) break;
            }
        }

        private static void OpenTemplateExcel(string templateFile, out Excel.ApplicationClass xlapp, out Excel.Workbook book)
        {
            object omissing = System.Reflection.Missing.Value;
            xlapp = new Excel.ApplicationClass();

            xlapp.Application.DisplayAlerts = false;
            xlapp.Visible = false;

            book = xlapp.Workbooks.Open(templateFile, omissing, omissing, omissing, omissing, omissing,
  omissing, omissing, omissing, omissing, omissing, omissing, omissing, omissing, omissing);

        }

        private void ReleaseExcelObject(params object[] objs)
        {
            if (objs == null || objs.Length == 0) return;
            for (int i = 0; i < objs.Length; i++)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objs[i]);
                objs[i] = null;
            }

            GC.Collect();
        }

        private string CopyTemplateFile(string templateFile)
        {
            FileInfo fi = new FileInfo(templateFile);
            if (fi.Exists == false) return "";

            string file = fi.DirectoryName.TrimEnd('\\') + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + fi.Extension.TrimStart('.');

            try
            {
                File.Copy(templateFile, file);
            }
            catch
            {
                return "";
            }

            return file;
        }
    }
}
