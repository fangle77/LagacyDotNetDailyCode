using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Data
{
    public interface INoSqlData
    {
        string TableName { get; set; }
        string KeyColumn { get; set; }
        string ValueColumn { get; set; }

        INoSqlData CreateNoSqlData(string tableName);
        INoSqlData CreateNoSqlData(string tableName, string keyColumn, string valueColum);

        bool Save(string key, string value);
        bool Delete(string key);

        dynamic LoadDynamicModel();
    }
}