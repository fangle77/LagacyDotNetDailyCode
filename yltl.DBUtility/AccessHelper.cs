using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace yltl.DBUtility
{
    /// <summary>
    /// Access数据库访问操作类
    /// </summary>
    internal class AccessHelper : IDBOperator, IDBSchema, IOperationEvent
    {
        private string _ConnectionString;

        public AccessHelper(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        #region IDBOperator 成员

        public event Action<string> LogHandler;

        public event Action<Exception> AfterExceptionThrow;

        public eDBType DBType { get { return eDBType.Access; } }

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        public bool TestConnection(out string msg)
        {
            msg = "";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    connection.Open();
                    msg = "连接成功";

                    WriteLog("测试连接成功");

                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public int ExecuteSql(string sql)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {

                    WriteLog("[Access]" + sql);

                    connection.Open();
                    var cmd = new OleDbCommand(sql, connection);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return -1;
            }
        }

        public int ExecuteSql(string sql, params System.Data.Common.DbParameter[] cmdParms)
        {
            try
            {
                WriteLog("[Access]ExecuteSql(sql,param):" + sql);
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    connection.Open();
                    OleDbCommand cmd = new OleDbCommand(sql, connection);

                    cmd.Parameters.AddRange(cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return -1;
            }
        }

        public System.Data.DataTable GetDataTable(string sql)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    connection.Open();

                    WriteLog("[Access] public DataTable GetDataTable(string sql)：" + sql);
                    WriteLog("查询开始时间：");

                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(sql, connection);

                    WriteLog("查询结束时间：");
                    da.Fill(dt);
                    WriteLog("da.Fill(dt)结束时间：");

                    return dt;
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return null;
            }
        }


        public System.Data.DataTable GetDataTable(string sql, params System.Data.Common.DbParameter[] cmdParms)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    connection.Open();

                    WriteLog("[Access] public DataTable GetDataTable(string sql)：" + sql);
                    WriteLog("查询开始时间：");

                    DataTable dt = new DataTable();

                    OleDbCommand cmd = new OleDbCommand(sql, connection);
                    cmd.Parameters.AddRange(cmdParms);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    WriteLog("查询结束时间：");
                    da.Fill(dt);

                    cmd.Parameters.Clear();
                    WriteLog("da.Fill(dt)结束时间：");

                    return dt;
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return null;
            }
        }

        public System.Data.Common.DbDataReader GetDataReader(string sql)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    WriteLog("[Access]" + sql);

                    connection.Open();
                    var cmd = new OleDbCommand(sql, connection);
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection
                        | CommandBehavior.Default);
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return null;
            }
        }

        public string ExecuteScalar(string sql)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {

                    WriteLog(sql);
                    connection.Open();
                    var cmd = new OleDbCommand(sql, connection);
                    object o = cmd.ExecuteScalar();

                    string s = null;
                    if (o != null && Object.Equals(o, System.DBNull.Value) == false)
                    {
                        s = o.ToString();
                    }

                    WriteLog("查询结果：" + s);

                    return s;
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return null;
            }
        }

        #endregion


        #region private method

        private void WriteLog(string log)
        {
            if (LogHandler != null) LogHandler(string.Format("{0}  {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), log));
        }

        #endregion

        #region IDBSchema 成员

        public DataTable GetTables()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    connection.Open();
                    DataTable schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    return schema;
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return null;
            }
        }

        public DataTable GetColumns(string tableName)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    connection.Open();

                    if (tableName == string.Empty) tableName = null;

                    DataTable schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName });// new object[] { null, null, null, "TABLE" });
                    return schema;
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return null;
            }
        }

        public DataTable GetViews()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConnectionString))
                {
                    connection.Open();
                    DataTable schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Views, null);// new object[] { null, null, null, "TABLE" });
                    return schema;
                }
            }
            catch (Exception ex)
            {
                if (AfterExceptionThrow != null) AfterExceptionThrow(ex);
                else throw ex;
                return null;
            }
        }

        #endregion
    }
}
