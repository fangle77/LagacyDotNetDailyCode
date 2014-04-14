using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace yltl.ExcelHelper
{
    /// <summary>
    /// BS模式下导出EXCEL的方法
    /// </summary>
    public class WebExcelExporter
    {
        public Action<string> ErrorMessageHandler { get; set; }
        public Action<Exception> ErrorHandler { get; set; }

        private void AddMessage(string msg)
        {
            if (ErrorMessageHandler != null) ErrorMessageHandler(msg);
        }

        public void DataTableToExcle(System.Data.DataTable dt, Dictionary<string, string> dicCoumnNameMapping, string fileName)
        {
            try
            {
                string exportDir = "~/ExcelFile/";//注意:该文件夹您须事先在服务器上建好才行
                if (System.IO.Directory.Exists(GetPhysicalPath(exportDir)) == false)
                {
                    System.IO.Directory.CreateDirectory(GetPhysicalPath(exportDir));
                }

                if (string.IsNullOrEmpty(fileName)) fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";

                //设置文件在服务器上的路径
                string physicalFileName = GetPhysicalPath(System.IO.Path.Combine(exportDir, fileName));

                ExcelHandler eh = new ExcelHandler();
                eh.ProcessErrorHandler = this.ErrorMessageHandler;

                var ds = new System.Data.DataSet();
                ds.Tables.Add(dt);
                eh.DataSet2Excel(ds, dicCoumnNameMapping, physicalFileName);
                ds.Tables.Remove(dt);
                ds.Dispose();

                //==============返回客户端
                ResponeToClient(physicalFileName, fileName);
            }
            catch (Exception ex)
            {
                if (this.ErrorMessageHandler != null) ErrorMessageHandler("导出过程中出错，" + ex.Message);
                if (ErrorHandler != null) ErrorHandler(ex);

                GC.Collect();
            }
        }

        private void ResponeToClient(string physicalFileName, string fileName)
        {
            if (System.IO.File.Exists(physicalFileName) == false)
            {
                if (ErrorMessageHandler != null)
                {
                    ErrorMessageHandler("不存在的文件，可能是导出失败");
                }
                return;
            }

            string fileURL = physicalFileName;

            //获取路径后从服务器下载文件至本地
            System.IO.FileStream fs = System.IO.File.Open(fileURL, System.IO.FileMode.Open);
            byte[] byteFile = new byte[fs.Length];
            fs.Read(byteFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            System.IO.File.Delete(fileURL);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Charset = "GB2312";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 

            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename="
                + System.Web.HttpContext.Current.Server.UrlEncode(fileName));
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度 

            // 指定返回的是一个不能被客户端读取的流，必须被下载 
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";

            // 把文件流发送到客户端 
            System.Web.HttpContext.Current.Response.BinaryWrite(byteFile);
            // 停止页面的执行 
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private string GetPhysicalPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// 按照模板导出
        /// </summary>
        /// <param name="dt">要导出的datatable</param>
        /// <param name="templateFile">要导出的excel模板文件</param>
        /// <param name="saveFile">要保存的文件名，可以为空</param>
        public void DataTableToExcelWithTemplate(System.Data.DataTable dt, string templateFile, string saveFile)
        {
            ExcelHandler eh = new ExcelHandler();
            try
            {
                string exportDir = "~/ExcelFile/";//注意:该文件夹您须事先在服务器上建好才行
                if (System.IO.Directory.Exists(GetPhysicalPath(exportDir)) == false)
                {
                    System.IO.Directory.CreateDirectory(GetPhysicalPath(exportDir));
                }

                if (string.IsNullOrEmpty(saveFile)) saveFile = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + ".xls";

                //设置文件在服务器上的路径
                string physicalFileName = GetPhysicalPath(System.IO.Path.Combine(exportDir, saveFile));

                eh.ProcessErrorHandler = this.ErrorMessageHandler;

                var ds = new System.Data.DataSet();
                ds.Tables.Add(dt);

                string tmpFile = eh.ExportWithTemplate(dt, templateFile, physicalFileName, false);

                ds.Tables.Remove(dt);
                ds.Dispose();

                //==============返回客户端
                ResponeToClient(physicalFileName, saveFile);
                try
                {
                    if (!string.IsNullOrEmpty(tmpFile))
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(tmpFile);
                        if (fi.Exists)
                        {
                            fi.Attributes = System.IO.FileAttributes.Normal;
                            System.IO.File.Delete(tmpFile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    AddMessage(ex.Message);
                }
            }
            catch (Exception ex)
            {
                if (this.ErrorMessageHandler != null) ErrorMessageHandler("导出过程中出错，" + ex.Message);
                if (ErrorHandler != null) ErrorHandler(ex);

                GC.Collect();
            }
        }
    }
}
