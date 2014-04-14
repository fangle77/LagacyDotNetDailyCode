using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExcelExportorForWin
{
    class ExcelColumnInfo : IGetColumnInfo
    {
        #region IGetColumnInfo 成员

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public List<ColumnElement> GetColumnElements()
        {
            throw new NotImplementedException();
        }

        #endregion

        private string _excelFile;

        public ExcelColumnInfo(string excelFile)
        {
            if (File.Exists(excelFile) == false)
            {
                throw new FileNotFoundException("文件不存在", excelFile);
            }

            _excelFile = excelFile;
        }
    }
}
