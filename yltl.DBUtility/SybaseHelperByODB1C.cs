using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;

using System.Data;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.Common;

using System.Windows.Forms;

using yltl.DWDY.Common;

namespace yltl.DBUtility
{
    /// <summary>
    /// Sybase 数据库操作类
    /// </summary>
    public class SybaseHelperByODBC : yltl.DBUtility.IDataBaseOper
    {
        /// <summary>
        /// 默认是 oracle 的数据库连接
        /// </summary>
        private static SybaseHelperByODBC dbOper_Sybase; //access 数据库的连接
        private static SybaseHelperByODBC dbOper_Custom;//自定义的数据库连接


        protected OdbcTransaction _Trans;//事务处理
        protected OdbcConnection oleconn;

        private string strConntionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + "Application.StartupPath" + "\\DataBase\\voltagedb_c.mdb"; //数据库连接字符串

        private log4net.ILog logger = log4net.LogManager.GetLogger("AccessHelper");// 

        #region 3个构造函数

        /// <summary>
        /// 通过连接串，返回数据库操作类
        ///  "Driver=Sybase.ASEOLEDBProvider;" +
        ///  "Server Name=ServerName,5000;" +
        ///  "Initial Catalog=DataBaseName;" +
        ///  "User id=UserName;" +
        ///  "Password=Secret;";
        /// </summary>
        /// <param name="ConnectionString">自定义连接串</param>
        public SybaseHelperByODBC(string ConnectionString)
        {
            this.strConntionString = ConnectionString;
            connOpen();
        }

        /// <summary>
        /// 返回数据库操作类
        /// </summary>
        /// <param name="at">数据库类型</param>
        public SybaseHelperByODBC(eDBType at)
        {
            string[] temp;
            string pwd = string.Empty;

            temp = Encrypt.DeCode(ReadXmlGetConnectString(at.ToString())).Split(';');

            if (temp.Length == 2)
            {
                this.strConntionString = temp[0];
                pwd = temp[1];
            }
            else
            {
                this.strConntionString = temp[0];
                pwd = "";
            }

            if (at == eDBType.Access)
            {
                //jet OleDB:Database Password=123456
                //this.strConntionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath +  this.strConntionString;
                this.strConntionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + this.strConntionString + ";jet OleDB:Database Password=" + pwd + "";
            }
            connOpen();
        }
        /// <summary>
        /// 构造数据库操作类
        /// </summary>
        /// <param name="ServerName">服务器名称或net服务名</param>
        /// <param name="DataBaseName"></param>
        /// <param name="UserIdName"></param>
        /// <param name="Password"></param>
        public SybaseHelperByODBC(eDBType at2, string ServerName, string DataBaseName, string UserIdName, string Password)
        {
            switch (at2)
            {
                case eDBType.Access:
                    //this.strConn = ReadXmlGetConnectString(at.ToString());
                    break;
                case eDBType.Oracle:
                    //Provider=OraOLEDB.Oracle.1;Persist Security Info=True;User ID=sjy;Password=sjy;Data Source=sjyweb
                    //this.strConntionString = " Data Source=" + ServerName + ";User ID=" + UserIdName + ";Password=" + Password + "";
                    this.strConntionString = "Provider=OraOLEDB.Oracle.1;Persist Security Info=True;User ID=" + UserIdName + ";Password=" + Password + ";Data Source=" + ServerName + "";
                    break;
                case eDBType.SqlServer:
                    this.strConntionString = "Provider=SQLOLEDB;server=" + ServerName + ";database=" + DataBaseName + ";uid=" + UserIdName + ";pwd=" + Password + "";
                    break;
                case eDBType.Custom:
                    break;
                case eDBType.Sybase:
                    //this.strConntionString = "Provider=Sybase.ASEOLEDBProvider.2;Data Source=" + ServerName + ";Initial Catalog=" + DataBaseName + ";User id=" + UserIdName + ";Password=" + Password + "";
                    this.strConntionString = "Dsn=" + ServerName + ";Uid=" + UserIdName + ";Pwd=" + Password + "";
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
        private string ReadXmlGetConnectString(string AccessType)
        {
            string xmlPath = Application.StartupPath + "//" + xmlName;

            return CommFunc.ReadXmlGetAttribute(xmlPath, "AccessConnection",
                                                AccessType, "ConnectionString");

        }

        #endregion 3个构造函数

        #region  获取数据库连接
        /// <summary>
        /// 根据数据库类型获取操作类
        /// </summary>
        /// <param name="at">数据库类型</param>
        /// <returns>DataBaseOper 数据操作类</returns>
        public static SybaseHelperByODBC GetDataBaseOper(eDBType at)
        {
            switch (at)
            {
                case eDBType.Access:
                    if (dbOper_Sybase == null)
                    {
                        dbOper_Sybase = new SybaseHelperByODBC(at);
                    }
                    return dbOper_Sybase;

                    break;
                case eDBType.Sybase:

                    if (dbOper_Sybase == null)
                    {
                        dbOper_Sybase = new SybaseHelperByODBC(at);
                    }
                    return dbOper_Sybase;

                    break;
                case eDBType.Custom:
                    break;

                default: //默认是本地库
                    if (dbOper_Sybase == null)
                    {
                        dbOper_Sybase = new SybaseHelperByODBC(at);
                    }
                    return dbOper_Sybase;
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
        public static SybaseHelperByODBC GetDataBaseOper(eDBType at, eDBType at2, string ServerName, string DataBaseName, string UserIdName, string Password)
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
                    dbOper_Custom = new SybaseHelperByODBC(at2, ServerName, DataBaseName, UserIdName, Password);
                    return dbOper_Custom;
                    break;
                default: //默认是本地库
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
                    oleconn = new OdbcConnection(strConntionString);
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
            catch (OdbcException oleex)
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
                {
                    oleconn.Close();
                    oleconn.Dispose();
                    oleconn = null;
                }
            }
            catch (OdbcException oleex)
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
            logger.Info("[Access] public DbDataReader GetDataReader(string sql)：" + sql);
#endif

            OdbcCommand cmd;
            OdbcDataReader dr;
            try
            {
                connOpen();
                cmd = new OdbcCommand(sql, oleconn);
                dr = cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (OdbcException oledbex)
            {
                logger.Error("[Access] public DbDataReader GetDataReader(string sql)：" + oledbex);

                throw new Exception(oledbex.Message);
            }
            return dr;
        }
        //返回所有行的记录集oleDbDataReader：查询、存储过程、自定义函数
        /// <summary>
        /// 返回所有行的记录集oleDbDataReader：查询、存储过程、自定义函数
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <param name="dr">out 传入的 OdbcDataReader 无需初始化该对象 </param>
        public void GetDataReader(string sql, out DbDataReader dr)
        {
            OdbcCommand cmd;
            try
            {
                connOpen();
                cmd = new OdbcCommand(sql, oleconn);
                dr = cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (OdbcException oledbex)
            {
                throw new Exception(oledbex.Message);
            }
        }

        /// <summary>
        /// 返回适配器 OdbcDataAdapter
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <returns></returns>
        public DbDataAdapter GetDataAdapter(string sql)
        {
            OdbcDataAdapter da;
            try
            {
                connOpen();
                da = new OdbcDataAdapter(sql, oleconn);
            }
            catch (OdbcException oledbex)
            {
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
#if DEBUG
            {
                logger.Info("[Access] public DataTable GetDataTable(string sql)：" + sql);
                logger.Info("查询开始时间：" + DateTime.Now.ToString());

            }
#endif

            DataTable dt = new DataTable();
            OdbcDataAdapter da;
            try
            {

                connOpen();
                da = new OdbcDataAdapter(sql, oleconn);

#if DEBUG
                {
                    logger.Info("查询结束时间：" + DateTime.Now.ToString());
                }
#endif

                da.Fill(dt);
#if DEBUG
                {
                    logger.Info("da.Fill(dt)结束时间：" + DateTime.Now.ToString());
                }
#endif

            }
            catch (OdbcException oleex)
            {
                logger.Error("[Access] public DataTable GetDataTable(string sql)：" + oleex);

                throw oleex;
            }
            return dt;
        }

        /// <summary>
        ///  out 返回DataTable
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <param name="dt">out 传入的 DataTable 无需初始化该对象</param>
        public void GetDataTable(string sql, out DataTable dt)
        {
            dt = new DataTable();
            OdbcDataAdapter da;
            try
            {
                connOpen();
                da = new OdbcDataAdapter(sql, oleconn);
                da.Fill(dt);
            }
            catch (OdbcException oleex)
            {
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
#if DEBUG
            {
                logger.Info("[Access] public DataSet GetDataSet(string sql)：" + sql);
                logger.Info("查询开始时间：" + DateTime.Now.ToString());

            }
#endif

            DataSet ds = new DataSet();
            OdbcDataAdapter da;
            try
            {
                connOpen();
                da = new OdbcDataAdapter(sql, oleconn);
#if DEBUG
                {
                    logger.Info("查询结束时间：" + DateTime.Now.ToString());
                }
#endif

                da.Fill(ds);
#if DEBUG
                {
                    logger.Info("da.Fill(ds)结束时间：" + DateTime.Now.ToString());
                }
#endif

            }
            catch (OdbcException oleex)
            {
                logger.Error("[Access] public DataSet GetDataSet(string sql)：" + oleex);

                throw new Exception(oleex.Message);
            }
            return ds;
        }
        //返回DataSet
        /// <summary>
        ///  out 返回DataSet
        /// </summary>
        /// <param name="sql">需要执行的sql语句</param>
        /// <param name="ds">out 传入的 DataSet 无需初始化该对象</param>
        public void GetDataSet(string sql, out DataSet ds)
        {
            ds = new DataSet();
            OdbcDataAdapter da;
            try
            {
                connOpen();
                da = new OdbcDataAdapter(sql, oleconn);
                da.Fill(ds);
            }
            catch (OdbcException oleex)
            {
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
            logger.Info("[Access] public int ExecuteSql(string sql)：" + sql);
#endif

            OdbcCommand cmd;


            try
            {
                connOpen();
                cmd = new OdbcCommand();
                cmd.Connection = oleconn;
                cmd.Transaction = _Trans;
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
            catch (OdbcException oleex)
            {
                logger.Error("[Access] public int ExecuteSql(string sql)：" + oleex);

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
            logger.Info("[Access] public string ExecuteScalar(string sql)：" + sql);
#endif

            OdbcCommand cmd;
            object obj;
            try
            {
                connOpen();
                cmd = new OdbcCommand();
                cmd.Connection = oleconn;
                cmd.CommandText = sql;
                obj = cmd.ExecuteScalar();
            }
            catch (OdbcException oleex)
            {
                logger.Error("[Access] public string ExecuteScalar(string sql)：" + oleex);

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
            logger.Info("[Access] public bool isExists(string sql)：" + sql);
#endif

            OdbcCommand cmd;
            object obj;
            try
            {
                connOpen();
                cmd = new OdbcCommand();
                cmd.Connection = oleconn;
                cmd.CommandText = sql;
                obj = cmd.ExecuteScalar();
            }
            catch (OdbcException oleex)
            {
                logger.Error("[Access] public bool isExists(string sql)：" + oleex);

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
            OdbcCommand cmd = new OdbcCommand();
            cmd.Connection = this.oleconn;
            OdbcTransaction tx = oleconn.BeginTransaction();
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
            OdbcCommand cmd;
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new OdbcCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (OdbcParameter param in prams)
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
            OdbcCommand cmd;
            DataTable dt = new DataTable();
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new OdbcCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (OdbcParameter param in prams)
                    cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                if (blTrans) this.Commit();
                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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
            OdbcCommand cmd;
            DataSet ds = new DataSet();
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new OdbcCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (OdbcParameter param in prams)
                    cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                if (blTrans) this.Commit();

                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
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
        /// 执行存储过程，返回OdbcDataReader对象
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数集</param>
        /// <param name="blTrans">是否启动事务处理</param>
        /// <returns>返回OdbcDataReader对象</returns>
        public DbDataReader ExecuteProcRetDataReader(string procName, DbParameter[] prams, bool blTrans)
        {
            OdbcCommand cmd;
            OdbcDataReader dr;
            try
            {
                if (blTrans) this.BeginTransaction();
                cmd = new OdbcCommand(procName, oleconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _Trans;
                foreach (OdbcParameter param in prams)
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

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public int ExecuteNoneQuery(string sqlText)
        {
            using (OdbcConnection connection = new OdbcConnection(strConntionString))
            {
                using (OdbcCommand cmd = new OdbcCommand(sqlText, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.Odbc.OdbcException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
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
            using (OdbcConnection connection = new OdbcConnection(strConntionString))
            {
                using (OdbcCommand cmd = new OdbcCommand(SQLString, connection))
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
                    catch (System.Data.Odbc.OdbcException e)
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
            using (OdbcConnection connection = new OdbcConnection(strConntionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OdbcDataAdapter command = new OdbcDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.Odbc.OdbcException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlTextList">SQL语句的哈希表（key为sql语句，value是该语句的OdbcParameter[]）</param>
        public void ExecuteSqlTran(Hashtable sqlTextList)
        {
            using (OdbcConnection conn = new OdbcConnection(strConntionString))
            {
                conn.Open();
                using (OdbcTransaction trans = conn.BeginTransaction())
                {
                    OdbcCommand cmd = new OdbcCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in sqlTextList)
                        {
                            string cmdText = myDE.Key.ToString();
                            OdbcParameter[] cmdParms = (OdbcParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();


                        }
                        trans.Commit();
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
            using (OdbcConnection connection = new OdbcConnection(strConntionString))
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlText, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.Odbc.OdbcException E)
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
            using (OdbcConnection connection = new OdbcConnection(strConntionString))
            {
                using (OdbcCommand cmd = new OdbcCommand())
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
                    catch (System.Data.Odbc.OdbcException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回OdbcDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OdbcDataReader</returns>
        public DbDataReader ExecuteReader(string sqlText, params DbParameter[] cmdParms)
        {
            OdbcConnection connection = new OdbcConnection(strConntionString);
            OdbcCommand cmd = new OdbcCommand();
            try
            {
                PrepareCommand(cmd, connection, null, sqlText, cmdParms);
                OdbcDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.Odbc.OdbcException e)
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
            using (OdbcConnection connection = new OdbcConnection(strConntionString))
            {
                OdbcCommand cmd = new OdbcCommand();
                PrepareCommand(cmd, connection, null, sqlText, cmdParms);
                using (OdbcDataAdapter da = new OdbcDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.Odbc.OdbcException ex)
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

        private static void PrepareCommand(OdbcCommand cmd, OdbcConnection conn, OdbcTransaction trans, string cmdText, DbParameter[] cmdParms)
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
                foreach (OdbcParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 获取当前页应该显示的记录
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页容量</param>
        /// <param name="uniqueCols">唯一性字段组合</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="queryString">查询字符串，支持联合查询</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的DataTable</returns>
        /// <remarks>
        /// Created By CDH 2009.1109
        /// </remarks>
        public DataTable QueryTablePagging(int pageIndex, int pageSize, string uniqueCols, string countSQL, string showString,
            string queryString, string whereString, string orderString, out int pageCount, ref int recordCount)
        {
            using (OdbcConnection connection = new OdbcConnection(strConntionString))
            {
                if (pageIndex < 1) pageIndex = 1;
                if (pageSize < 1) pageSize = 10;
                if (string.IsNullOrEmpty(showString)) showString = "*";
                if (string.IsNullOrEmpty(orderString)) orderString = uniqueCols + " desc";
                string innerOrder = orderString.ToLower().IndexOf(" desc") > 0 ? "desc" : "asc";
                string outerOrder = innerOrder == "desc" ? "asc" : "desc";

                connection.Open();

                string myVw = string.Format(" ( {0} ) tempVw ", queryString);

                OdbcCommand cmdCount = new OdbcCommand(countSQL, connection);

                recordCount = Convert.ToInt32(cmdCount.ExecuteScalar());

                if ((recordCount % pageSize) > 0)
                    pageCount = recordCount / pageSize + 1;
                else
                    pageCount = recordCount / pageSize;

                //2009.1207.CDH
                if (pageIndex > pageCount && recordCount != 0)
                    pageIndex = pageCount;

                OdbcCommand cmdRecord;

                if (pageIndex == 1)//第一页
                {
                    cmdRecord = new OdbcCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ",
                        pageSize, showString, myVw, whereString, orderString), connection);
                }
                else if (pageIndex == pageCount)
                {//最末页
                    int pageLowerBound = recordCount - (pageSize * (pageIndex - 1));
                    cmdRecord = new OdbcCommand(
                        string.Format("select top {0} {1} from {2} {3} order by {4} {5}",
                        pageLowerBound, showString, myVw, whereString, uniqueCols, outerOrder),
                        connection);
                }
                else if (pageIndex > pageCount)//超出总页数
                {
                    cmdRecord = new OdbcCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString), connection);
                }
                else
                {
                    int pageLowerBound = pageSize * pageIndex;
                    //int pageUpperBound = pageLowerBound - pageSize;
                    //string recordIDs = recordID(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, uniqueCols, myVw, whereString, orderString), pageUpperBound, connection);
                    //cmdRecord = new OdbcCommand(string.Format("select {0} from {1} where id in ({2}) order by {3} ", showString, myVw, recordIDs, orderString), connection);
                    cmdRecord = new OdbcCommand(
                        string.Format("select top {0} * from (select top {1} {2} from {3} {4} order by {5} {6}) order by {5} {7}",
                        pageSize, pageLowerBound, showString, myVw, whereString, uniqueCols, innerOrder, outerOrder),
                        connection);
                }

                OdbcDataAdapter dataAdapter = new OdbcDataAdapter(cmdRecord);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);

                return dt;
            }
        }

        /// <remarks>
        /// Created By CDH 2009.1109
        /// </remarks>
        private string recordID(string query, int passCount, DbConnection conn)
        {
            OdbcCommand cmd = new OdbcCommand(query, (OdbcConnection)conn);
            string result = string.Empty;
            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (passCount < 1)
                    {
                        result += "," + dr.GetInt32(0);
                    }
                    passCount--;
                }
            }
            return result.Substring(1);
        }


        #endregion

        private bool isMulitThread = false;
        /// <summary>
        /// 是否是多线程访问
        /// </summary>
        public bool IsMulitThread
        {
            get { return isMulitThread; }
            set { isMulitThread = value; }
        }

    }
}
