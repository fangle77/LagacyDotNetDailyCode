using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace yltl.DBUtility
{
    /// <summary>
    /// Oracle数据库操作类
    /// </summary>
    internal class OracleHelper : IDBOperator
    {
        private string _ConnectionString;
        //Data Source=lan_189;User ID=user_ahdy_shi;Password=user_ahdy_shi
        public OracleHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region IDBOperator 成员

        public event Action<string> LogHandler;

        public event Action<Exception> AfterExceptionThrow;

        public eDBType DBType { get { return eDBType.Oracle; } }

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set
            {
                _ConnectionString = value;
            }
        }

        public bool TestConnection(out string msg)
        {
            msg = "";
            try
            {
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    OracleConnection.ClearPool(connection);

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
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {

                    WriteLog("[Oracle]" + sql);

                    connection.Open();
                    var cmd = new OracleCommand(sql, connection);
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
                WriteLog("[Oracle]ExecuteSql(sql,param):" + sql);
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(sql, connection);

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
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();

                    WriteLog("[Oracle] public DataTable GetDataTable(string sql)：" + sql);
                    WriteLog("查询开始时间：");

                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(sql, connection);

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
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();

                    WriteLog("[Oracle] public DataTable GetDataTable(string sql,param)：" + sql);
                    WriteLog("查询开始时间：");

                    DataTable dt = new DataTable();

                    OracleCommand cmd = new OracleCommand(sql, connection);
                    cmd.Parameters.AddRange(cmdParms);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);


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
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    WriteLog("[Oracle]GetDataReader:" + sql);

                    connection.Open();
                    var cmd = new OracleCommand(sql, connection);
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
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    WriteLog("[Oracle]" + sql);
                    connection.Open();
                    var cmd = new OracleCommand(sql, connection);
                    object o = cmd.ExecuteScalar();

                    string s = null;
                    if (o != null && Object.Equals(o, System.DBNull.Value) == false)
                    {
                        s = o.ToString();
                    }

                    WriteLog("[Oracle]查询结果：" + s);

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
            string sql = @"select a.*,b.COMMENTS from user_tables a
    inner join user_tab_comments b on a.TABLE_NAME=b.TABLE_NAME order by a.TABLE_NAME ";
            return GetDataTable(sql);
        }

        public DataTable GetColumns(string tableName)
        {
            string table = "";
            if (!string.IsNullOrEmpty(tableName))
            {
                table = string.Format(" where a.table_name='{0}'", tableName.ToUpper());
            }
            string sql = string.Format(@"select a.*,b.comments from user_tab_columns a 
 inner join user_col_comments b
 on a.TABLE_NAME=b.table_name and a.COLUMN_NAME=b.column_name {0} ", table);

            return GetDataTable(sql);
        }

        public DataTable GetViews()
        {
            string sql = "select * from user_views";
            return GetDataTable(sql);
        }

        #endregion
    }
}
