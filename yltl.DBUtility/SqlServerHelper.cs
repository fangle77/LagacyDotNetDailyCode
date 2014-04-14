using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace yltl.DBUtility
{
    /// <summary>
    /// SqlServer数据库操作类
    /// </summary>
    internal class SqlServerHelper : IDBOperator
    {
        private string _ConnectionString;

        public SqlServerHelper(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        #region IDBOperator 成员

        public event Action<string> LogHandler;

        public event Action<Exception> AfterExceptionThrow;

        public eDBType DBType { get { return eDBType.SqlServer; } }

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
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    SqlConnection.ClearPool(connection);

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
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {

                    WriteLog("[SqlServer]" + sql);

                    connection.Open();
                    var cmd = new SqlCommand(sql, connection);
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
                WriteLog("[SqlServer]ExecuteSql(sql,param):" + sql);
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);

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
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();

                    WriteLog("[Access] public DataTable GetDataTable(string sql)：" + sql);
                    WriteLog("查询开始时间：");

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, connection);

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
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();

                    WriteLog("[SqlServer] public DataTable GetDataTable(string sql,param)：" + sql);
                    WriteLog("查询开始时间：");

                    DataTable dt = new DataTable();

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddRange(cmdParms);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);


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
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    WriteLog("[SqlServer]GetDataReader:" + sql);

                    connection.Open();
                    var cmd = new SqlCommand(sql, connection);
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
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    WriteLog(sql);
                    connection.Open();
                    var cmd = new SqlCommand(sql, connection);
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

        private void WriteLog(string log)
        {
            if (LogHandler != null) LogHandler(string.Format("{0}  {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), log));
        }

        #region IDBSchema 成员

        public DataTable GetTables()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();
                    return connection.GetSchema("Tables");
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
                if (tableName == "") tableName = null;

                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();
                    return connection.GetSchema("Columns", new string[] { null, null, tableName });
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
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();
                    return connection.GetSchema("Views");
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
