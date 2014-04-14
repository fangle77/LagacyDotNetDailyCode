using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

using System.Data;
using System.Collections;
using System.Xml;
using System.IO;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;

using yltl.DWDY.Common;


namespace yltl.DBUtility
{
    /// <summary>
    /// SqlServer 数据库操作类
    /// </summary>
    public class SqlServerHelper : yltl.DBUtility.IDataBaseOper
    {
        
        /// <summary>
        /// 默认是 oracle 的数据库连接
        /// </summary>
        private static SqlServerHelper dbOper_SqlServer;//sqlserver 的数据库连接
        private static SqlServerHelper dbOper_Custom;//自定义的数据库连接

        protected SqlTransaction _Trans;//事务处理
        protected SqlConnection oleconn;

        private string strConntionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + "Application.StartupPath" + "\\DataBase\\voltagedb_c.mdb"; //数据库连接字符串

        private log4net.ILog logger = log4net.LogManager.GetLogger("SqlServerHelper");// 

        #region 3个构造函数
        public SqlServerHelper(string ConnectionString)
        {
            this.strConntionString = ConnectionString;
            connOpen();
        }

        public SqlServerHelper(eDBType at)
        {
            this.strConntionString = ReadXmlGetConnectString(at.ToString());

            connOpen();
        }
        /// <summary>
        /// 构造数据库操作类
        /// </summary>
        /// <param name="at2">数据库类型</param>
        /// <param name="ServerName">服务器名称或net服务名</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="UserIdName">用户名</param>
        /// <param name="Password">密码</param>
        public SqlServerHelper(eDBType at2, string ServerName, string DataBaseName, string UserIdName, string Password)
        {
            switch (at2)
            {
                case eDBType.SqlServer:
                    //this.strConntionString = "Provider=SQLOLEDB;server=" + ServerName + ";database=" + DataBaseName + ";uid=" + UserIdName + ";pwd=" + Password + "";
                    this.strConntionString = "Server=" + ServerName + ";Database=" + DataBaseName + ";Uid=" + UserIdName + ";Pwd=" + Password + "";

                    break;
                case eDBType.Custom:
                    break;
                default:
                    break;
            }
            connOpen();
        }

        private string xmlName = "SystemConfig.xml"; //系统配置文件的名称
        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <param name="AccessType">数据库类型</param>
        /// <returns>数据库连接字符串</returns>
        private string  ReadXmlGetConnectString(string AccessType)
        {
            string xmlPath = Application.StartupPath + "//" + xmlName;
            string temp = string.Empty;

            temp= CommFunc.ReadXmlGetAttribute(xmlPath, "AccessConnection", 
                                                AccessType, "ConnectionString");

            //解密返回

            return Encrypt.DeCode(temp);
        }

        #endregion 3个构造函数

        #region  获取数据库连接
        /// <summary>
        /// 根据数据库类型获取操作类
        /// </summary>
        /// <param name="at">数据库类型</param>
        /// <returns>DataBaseOper 数据操作类</returns>
        public static SqlServerHelper GetDataBaseOper(eDBType at)
        {
            switch (at)
            {
                case eDBType.SqlServer :
                    if (dbOper_SqlServer == null)
                    {
                        return dbOper_SqlServer = new SqlServerHelper(at);
                    }
                    return dbOper_SqlServer;

                    break;
                case eDBType.Custom:
                    break;
                default : 
                    break;
            }

            return null;
        }

        /// <summary>
        /// 在自定义数据库连接中根据数据库类型获取数据库连接
        /// </summary>
        /// <param name="at">数据库的连接方式</param>
        /// <param name="at2">自定义数据库的数据库类型</param>
        /// <param name="ServerName">(net)服务器名称</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="UserIdName">用户</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static SqlServerHelper GetDataBaseOper(eDBType at, eDBType at2, string ServerName, string DataBaseName, string UserIdName, string Password)
        {

            switch (at)
            {
                case eDBType.Access:
                case eDBType.Oracle:
                case eDBType.SqlServer:
                    break;
                case eDBType.Custom:
                    if (dbOper_Custom != null)
                    {
                        dbOper_Custom.ConnClose(); //先关闭 再连接 
                    }
                    dbOper_Custom = new SqlServerHelper(at2, ServerName, DataBaseName, UserIdName, Password);
                    return dbOper_Custom;
                    break;
                default: 
                    break;
            }
            return null;
        }

        #endregion 获取数据库连接

        #region 连接属性的设置
        public string ConnectionString
        {
            set { strConntionString = value; }
            get { return strConntionString; }
        }

        private string serverName;
        public string ServerName
        {
            set { serverName = value; }
            get { return serverName; }
        }
        private string databaseName;
        public string DataBaseName
        {
            set { databaseName = value; }
            get { return databaseName; }
        }
        private string uidName;
        public string UserIddName
        {
            set { uidName = value; }
            get { return uidName; }
        }
        private string pwd;
        public string Password
        {
            set { pwd = value; }
            get { return pwd; }
        }

        #endregion 连接属性的设置

        #region 连接的管理
        //打开操作
        private void connOpen()
        {
            try
            {
                if (this.oleconn == null)
                {
                    oleconn = new SqlConnection(strConntionString);
                    oleconn.Open();
                }
                else
                {
                    if (oleconn.State != ConnectionState.Open)
                    {
                        this.oleconn.Open();
                    }
                }
            }
            catch (SqlException oleex)
            {
                throw new Exception(oleex.Message);
            }
        }
        //关闭操作
        /// <summary>
        /// 断开连接 
        /// </summary>
        public void ConnClose()
        {
            try
            {
                if ((oleconn != null) && (oleconn.State != ConnectionState.Closed))
                    oleconn.Close();
            }
            catch (SqlException oleex)
            {
                throw new Exception(oleex.Message);
            }
        }

        /// <summary>
        /// 是否连接
        /// </summary>
        /// <returns></returns>
        public bool IsConnection()
        {
            try
            {
                oleconn.Close();
                oleconn.Open();
                return true;
            }
            catch
            {
                return false; 
            }
        }

        #endregion 连接的管理

        #region 事务处理的管理
        //启动事务处理
        /// <summary>
        /// 启动事务处理
        /// </summary>
        public void BeginTransaction()
        {
            _Trans = oleconn.BeginTransaction(IsolationLevel.ReadCommitted);//  '在当前事务中启动命令
        }
        //提交事务处理
        /// <summary>
        /// 提交事务处理
        /// </summary>
        public void Commit()
        {
            _Trans.Commit();
        }
        //回滚事务的处理
        /// <summary>
        /// 回滚事务的处理
        /// </summary>
        public void RollBack()
        {
            _Trans.Rollback();
        }
        #endregion 事务处理的管理

        #region 返回对象 eg：DataReader DataTable 等
        //返回所有行的记录集oleDbDataReader：查询、存储过程、自定义函数
        /// <summary>
        /// 返回所有行的记录集SqlDataReader：查询、存储过程、自定义函数
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <returns>返回：oleDbDataReader对象</returns>
        public DbDataReader GetDataReader(string sql)
        {

#if DEBUG
            logger.Info("[SqlServer]public DbDataReader GetDataReader(string sql)：" + sql);
#endif

            SqlCommand cmd;
            SqlDataReader dr;
            try
            {
                connOpen();
                cmd = new SqlCommand(sql, oleconn);
                dr = cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (SqlException oledbex)
            {
                logger.Error("[SqlServer]public DbDataReader GetDataReader(string sql)：" + oledbex);

                throw new Exception(oledbex.Message);
            }
            return dr;
        }
        //返回所有行的记录集oleDbDataReader：查询、存储过程、自定义函数
        /// <summary>
        /// 返回所有行的记录集oleDbDataReader：查询、存储过程、自定义函数
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <param name="dr">out 传入的 SqlDataReader 无需初始化该对象 </param>
        public void GetDataReader(string sql, out DbDataReader dr)
        {
            if (isMulitThread)
            {
                lock (padLock)
                {

                    #region 枷锁
#if DEBUG
                    logger.Info("[SqlServer]public void GetDataReader(string sql, out DbDataReader dr)：" + sql);
#endif

                    SqlCommand cmd;


                    try
                    {
                        connOpen();
                        cmd = new SqlCommand(sql, oleconn);
                        dr = cmd.ExecuteReader(CommandBehavior.Default);
                    }
                    catch (SqlException oledbex)
                    {
                        logger.Error("[SqlServer]public void GetDataReader(string sql, out DbDataReader dr)：" + oledbex);

                        throw new Exception(oledbex.Message);
                    }

                    #endregion
                }
            }
            else
            {
                #region 枷锁
#if DEBUG
                logger.Info("[SqlServer]public void GetDataReader(string sql, out DbDataReader dr)：" + sql);
#endif

                SqlCommand cmd;


                try
                {
                    connOpen();
                    cmd = new SqlCommand(sql, oleconn);
                    dr = cmd.ExecuteReader(CommandBehavior.Default);
                }
                catch (SqlException oledbex)
                {
                    logger.Error("[SqlServer]public void GetDataReader(string sql, out DbDataReader dr)：" + oledbex);

                    throw new Exception(oledbex.Message);
                }

                #endregion 
            }

        }

        /// <summary>
        /// 返回适配器 SqlDataAdapter
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <returns></returns>
        public DbDataAdapter GetDataAdapter(string sql)
        {

#if DEBUG
            logger.Info("[SqlServer]public DbDataAdapter GetDataAdapter(string sql)：" + sql);
#endif

            SqlDataAdapter da;
            try
            {
                connOpen();
                da = new SqlDataAdapter(sql, oleconn);
            }
            catch (SqlException oledbex)
            {

                logger.Error("[SqlServer]public DbDataAdapter GetDataAdapter(string sql)：" + oledbex);

                throw new Exception(oledbex.Message);
            }

            return da;
        }

        //返回DataTable
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sql"> 需要执行的sql语句</param>
        /// <returns>返回：DataTable对象</returns>
        public DataTable GetDataTable(string sql)
        {
            if (isMulitThread)
            {
                lock (padLock)
                {

                    #region 枷锁

#if DEBUG
                    logger.Info("[SqlServer]public DataTable GetDataTable(string sql)：" + sql);
#endif
                    DataTable dt = new DataTable();
                    SqlDataAdapter da;

                    try
                    {
                        connOpen();
                        da = new SqlDataAdapter(sql, oleconn);
                        da.Fill(dt);
                    }
                    catch (SqlException oleex)
                    {

                        logger.Error("[SqlServer]public DataTable GetDataTable(string sql)：" + oleex);

                        throw oleex;
                    }

                    return dt;
                    #endregion
                }
            }
            else
            {
                #region 不加锁的代码

#if DEBUG
                logger.Info("[SqlServer]public DataTable GetDataTable(string sql)：" + sql);
#endif
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                try
                {
                    connOpen();
                    da = new SqlDataAdapter(sql, oleconn);
                    da.Fill(dt);
                }
                catch (SqlException oleex)
                {

                    logger.Error("[SqlServer]public DataTable GetDataTable(string sql)：" + oleex);

                    throw oleex;
                }

                return dt;
                #endregion
            }
        }

        /// <summary>
        ///  out 返回DataTable
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <param name="dt">out 传入的 DataTable 无需初始化该对象</param>
        public void GetDataTable(string sql, out DataTable dt)
        {


#if DEBUG
            logger.Info("[SqlServer]public void GetDataTable(string sql, out DataTable dt)：" + sql);
#endif

            dt = new DataTable();
            SqlDataAdapter da;
            try
            {
                connOpen();
                da = new SqlDataAdapter(sql, oleconn);
                da.Fill(dt);
            }
            catch (SqlException oleex)
            {

                logger.Error("[SqlServer]public void GetDataTable(string sql, out DataTable dt)：" + oleex);

                throw new Exception(oleex.Message);
            }
        }
        

        //返回DataSet
        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="sql"> 需要执行的sql语句</param>
        /// <returns>返回：DataSet对象</returns>
        public DataSet GetDataSet(string sql)
        {
            if (isMulitThread)
            {
                lock (padLock)
                {
                    #region 枷锁
#if DEBUG
                    logger.Info("[SqlServer]public DataSet GetDataSet(string sql)：" + sql);
#endif

                    DataSet ds = new DataSet();
                    SqlDataAdapter da;
                    try
                    {
                        connOpen();
                        da = new SqlDataAdapter(sql, oleconn);
                        da.Fill(ds);
                    }
                    catch (SqlException oleex)
                    {
                        logger.Error("[SqlServer]public DataSet GetDataSet(string sql)：" + oleex);

                        throw new Exception(oleex.Message);
                    }
                    return ds;
                    #endregion 枷锁
                }
            }
            else
            {
                #region 不枷锁
#if DEBUG
                logger.Info("[SqlServer]public DataSet GetDataSet(string sql)：" + sql);
#endif

                DataSet ds = new DataSet();
                SqlDataAdapter da;
                try
                {
                    connOpen();
                    da = new SqlDataAdapter(sql, oleconn);
                    da.Fill(ds);
                }
                catch (SqlException oleex)
                {
                    logger.Error("[SqlServer]public DataSet GetDataSet(string sql)：" + oleex);

                    throw new Exception(oleex.Message);
                }
                return ds;
                #endregion 枷锁
            }
        }
        //返回DataSet
        /// <summary>
        ///  out 返回DataSet
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <param name="ds">out 传入的 DataSet 无需初始化该对象</param>
        public void GetDataSet(string sql, out DataSet ds)
        {

#if DEBUG
            logger.Info("[SqlServer]public void GetDataSet(string sql, out DataSet ds)：" + sql);
#endif

            ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                connOpen();
                da = new SqlDataAdapter(sql, oleconn);
                da.Fill(ds);
            }
            catch (SqlException oleex)
            {
                logger.Error("[SqlServer]public void GetDataSet(string sql, out DataSet ds)：" + oleex);

                throw new Exception(oleex.Message);
            }
        }

        #endregion 返回对象 eg：DataReader DataTable 等

        #region 执行简单的sql语句，返回首行首列及是否存在的判断等的简单调用

        //执行插入、删除、更新语句.有事务 就进行事务处理 
        /// <summary>
        /// 执行插入、删除、更新语句.有事务 就进行事务处理 
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        public int ExecuteSql(string sql)
        {
#if DEBUG
            logger.Info("[SqlServer]public int ExecuteSql(string sql)：" + sql);
#endif

            SqlCommand cmd;
            

            try
            {    
                connOpen();
                cmd = new SqlCommand();
                //
                cmd.Connection = oleconn;
                cmd.Transaction = _Trans;
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException oleex)
            {
                logger.Error("[SqlServer]public int ExecuteSql(string sql)：" + oleex);

                throw new Exception(oleex.Message);
            }  
            return -1;

        }

        //返回首行首列
        /// <summary>
        /// 返回首行首列.类型是String
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <returns>字符串类型的首行首列的值</returns>
        public string ExecuteScalar(string sql)
        {

#if DEBUG
            logger.Info("[SqlServer]public string ExecuteScalar(string sql)：" + sql);
#endif

            SqlCommand cmd;
            object obj;
            try
            {
                connOpen();
                cmd = new SqlCommand();
                cmd.Connection = oleconn;
                cmd.CommandText = sql;
                obj = cmd.ExecuteScalar();
            }
            catch (SqlException oleex)
            {
                logger.Error("[SqlServer]public string ExecuteScalar(string sql)：" + oleex);

                throw new Exception(oleex.Message);
            }
            if (obj == null)
                return string.Empty;
            return obj.ToString();
        }
        //判断是否存在
        /// <summary>
        /// 判断数据表是否有此记录
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <returns>返回 True 表示存在，False 表示不存在</returns>
        public bool IsExist(string sql)
        {
#if DEBUG
            logger.Info("[SqlServer]public bool isExists(string sql)：" + sql);
#endif

            SqlCommand cmd;
            object obj;
            try
            {
                connOpen();
                cmd = new SqlCommand();
                cmd.Connection = oleconn;
                cmd.CommandText = sql;
                obj = cmd.ExecuteScalar();
            }
            catch (SqlException oleex)
            {
                logger.Error("[SqlServer]public bool isExists(string sql)：" + oleex);

                throw new Exception(oleex.Message);
            }
            if (obj == null)
                return false;
            else
                return true;
        }

        #endregion 执行简单的sql语句，返回首行首列及是否存在的判断等的简单调用

        #region 存储过程的相关操作
        /// <summary>
        /// 存储过程的方式执行多条SQL语句
        /// </summary>
        /// <param name="sqlList">多条SQL语句的数组 类型：ArrayList</param>
        /// <returns>True：执行成功，False：执行失败</returns>
        public bool ExecuteSqlTran(ArrayList sqlList)
        {
            connOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.oleconn;
            SqlTransaction tx = oleconn.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                for (int n = 0; n < sqlList.Count; n++)
                {
                    string strsql = sqlList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 执行存储过程,无返回值的
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数数组</param>
        /// <param name="prams">是否启动事务</param>
        public void ExecuteProc(string procName, DbParameter[] prams, bool blTrans)
        {
            SqlCommand cmd;
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new SqlCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (SqlParameter param in prams)
                    cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                if (blTrans) this.Commit();
            }
            catch (Exception ex)
            {
                if (blTrans) this.RollBack();
                throw new Exception("在执行存储过程时：" + ex.Message);
            }
        }

        /// <summary>
        /// 执行存储过程，返回DataTable数据集
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数集</param>
        /// <param name="blTrans">是否启动事务处理</param>
        /// <returns>返回DataTable数据集</returns>
        public DataTable ExecuteProcRetDataTable(string procName, DbParameter[] prams, bool blTrans)
        {
            SqlCommand cmd;
            DataTable dt = new DataTable();
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new SqlCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (SqlParameter param in prams)
                    cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                if (blTrans) this.Commit();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                if (blTrans) this.RollBack();
                throw new Exception("在执行存储过程时：" + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 执行存储过程，返回DataSet数据集
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数集</param>
        /// <param name="blTrans">是否启动事务处理</param>
        /// <returns>返回DataSet数据集</returns>
        public DataSet ExecuteProcRetDataSet(string procName, DbParameter[] prams, bool blTrans)
        {
            SqlCommand cmd;
            DataSet ds = new DataSet();
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new SqlCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (SqlParameter param in prams)
                    cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                if (blTrans) this.Commit();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                if (blTrans) this.RollBack();
                throw new Exception("在执行存储过程时：" + ex.Message);
            }

            return ds;
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数集</param>
        /// <param name="blTrans">是否启动事务处理</param>
        /// <returns>返回SqlDataReader对象</returns>
        public DbDataReader ExecuteProcRetDataReader(string procName, DbParameter[] prams, bool blTrans)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new SqlCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (SqlParameter param in prams)
                    cmd.Parameters.Add(param);
                dr = cmd.ExecuteReader();
                if (blTrans) this.Commit();
            }
            catch (Exception ex)
            {
                if (blTrans) this.RollBack();
                throw new Exception("在执行存储过程时：" + ex.Message);
            }
            return dr;
        }
        #endregion 存储过程的相关操作

        #region 及时关闭连接 | 执行带参数的SQL语句

        // Create By CDH 2009.0910.0912 
        // 及时地关闭数据库连接
        // Modified By CDH 2009.0927.0844

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public int ExecuteNoneQuery(string sqlText)
        {

#if DEBUG
            logger.Info("[SqlServer]public int ExecuteNoneQuery(string sqlText)：" + sqlText);
#endif

            using (SqlConnection connection = new SqlConnection(strConntionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlText,connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        logger.Info("[SqlServer]public int ExecuteNoneQuery(string sqlText)：" + e);

                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(strConntionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataTable Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(strConntionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlTextList">SQL语句的哈希表（key为sql语句，value是该语句的DbParameter[]）</param>
        public void ExecuteSqlTran(Hashtable sqlTextList)
        {
            using (SqlConnection conn = new SqlConnection(strConntionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in sqlTextList)
                        {
                            string cmdText = myDE.Key.ToString();
                            DbParameter[] cmdParms = (DbParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlText">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteNoneQuery(string sqlText, params DbParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(strConntionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlText, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sqlText">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteSingle(string sqlText, params DbParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(strConntionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlText, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public DbDataReader ExecuteReader(string sqlText, params DbParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(strConntionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, sqlText, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlText">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sqlText, params DbParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(strConntionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sqlText, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public DataTable QueryTable(string sqlText, params DbParameter[] cmdParms)
        {
            return this.Query(sqlText, cmdParms).Tables[0];
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        private static readonly object padLock = new object();
        private bool isMulitThread = false;
        /// <summary>
        /// 是否是多线程访问
        /// </summary>
        public bool IsMulitThread
        {
            get { return isMulitThread; }
            set { isMulitThread = value; }
        }

        //空函数，实现接口
        public DataTable QueryTablePagging(int pageIndex, int pageSize, string uniqueCols, string countSQL, string showString,
                     string queryString, string whereString, string orderString,
             out int pageCount, ref int recordCount)
        {
            pageCount = -1;
            recordCount = -1;
            return null;
        }

    }
}
