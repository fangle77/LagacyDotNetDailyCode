using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelExportorForWin
{
    class ExportToExcel
    {
        public void Export(MapElementCollection mapping, System.Data.DataTable dt)
        {
            if (mapping == null) return;
            var colMapping = mapping.GetColumnMapping();

            if (colMapping == null || colMapping.Count == 0) return;

            yltl.ExcelHelper.WinFormExcelExporter.DataTableToExcel(dt, colMapping);
        }
    }
}
