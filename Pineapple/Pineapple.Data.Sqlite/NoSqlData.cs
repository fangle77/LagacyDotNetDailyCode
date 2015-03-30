using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Dapper;
using Pineapple.Core.Dyanamic;
using Pineapple.Model;
using System.Linq;

namespace Pineapple.Data.Sqlite
{
    public class NoSqlData : INoSqlData
    {
        private string keyColumn = "key";
        private string valueColumn = "value";

        public string TableName { get; set; }

        public string KeyColumn
        {
            get { return keyColumn; }
            set { keyColumn = value; }
        }

        public string ValueColumn
        {
            get { return valueColumn; }
            set { valueColumn = value; }
        }

        public bool Save(string key, string value)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                int i = cnn.Query<int>(string.Format("select count(1) from {0} where {1}=@Key", TableName, keyColumn)
                    , new { Key = key }).FirstOrDefault();
                int rows = 0;
                if (i > 0)
                {
                    rows = cnn.Execute(string.Format("Update {0} set {2}=@Value where {1}=@Key", TableName, keyColumn, valueColumn)
                        , new { Key = key, Value = value });
                }
                else
                {
                    rows = cnn.Execute(string.Format("Insert into {0}({1},{2}) values(@Key,@Value)", TableName, keyColumn, valueColumn)
                        , new { Key = key, Value = value });
                }
                return rows > 0;
            }
        }

        public bool Delete(string key)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                int i = cnn.Execute(string.Format("delete from {0} where {1}=@Key", TableName, keyColumn)
                    , new { Key = key });
                return i > 0;
            }
        }

        public dynamic LoadDynamicModel()
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                var obj = cnn.Query<dynamic>("select {1} as Key, {2} as Value from {0}", TableName).ToList();
                dynamic dModel = new DynamicModel();
                foreach (var o in obj)
                {
                    dModel.Set(o.Key, o.Value);
                }
                return dModel;
            }
        }

        public INoSqlData CreateNoSqlData(string tableName)
        {
            return new NoSqlData() { TableName = tableName };
        }

        public INoSqlData CreateNoSqlData(string tableName, string keyColumn, string valueColum)
        {
            return new NoSqlData() { TableName = tableName, KeyColumn = keyColumn, ValueColumn = valueColum };
        }
    }
}
