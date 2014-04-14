using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Xml;
using System.IO;


namespace yltl.DBUtility
{
    /// <summary>
    /// 数据库操作类，无法直接实例化，只能通过GetDataBaseOper(AccessType at)获取数据库的操作对象。
    /// 保证数据库的有效连接和最少的连接
    /// </summary>
    public class DataBaseOper
    {
        private static OracleHelper dbOper;//默认是 oracle 的数据库连接
        private static OracleHelper dbOper_Gpms; //连接gpms的数据库
        private static AccessHelper dbOper_Access; //access 数据库的连接
        private static SybaseHelper dbOper_Sybase; //access 数据库的连接
        private static SqlServerHelper dbOper_SqlServer;//sqlserver 的数据库连接
        private static IDataBaseOper dbOper_Custom;//自定义的数据库连接 只有接口中定义的方法。
        private static OracleHelper dbOper_Gpms_APP; //连接gpms的数据库 中剥离出去的表的

        //private static IList<IDataBaseOper> dbOper_Custom_list;//自定义的数据库连接 只有接口中定义的方法。
        private static Hashtable dbOper_Custom_Hashtable;//自定义的数据库连接 只有接口中定义的方法 保持连接的

        #region  获取数据库连接
        /// <summary>
        ///  根据数据库类型获取操作类
        ///  创建者：ycf 
        ///  创建时间： 20100304
        /// <example >调用示例
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(ConnectionString)
        /// </code>
        /// 返回：IDataBaseOper 
        /// </example>        /// 
        /// </summary>
        /// <param name="ConnectionString">access数据库连接字符串</param>
        /// <returns></returns>
        public static IDataBaseOper GetDataBaseOper_ConnectionString(string ConnectionString)
        {
            dbOper_Access = new AccessHelper(ConnectionString);

            return dbOper_Access;


        }
        /// <summary>
        /// 根据数据库类型获取操作类
        /// <example >调用示例
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(AccessConnectionType.Oralce)
        /// </code>
        /// 返回：IDataBaseOper 
        /// </example>
        /// 创建者：wls 20090902
        /// </summary>
        /// <param name="at">数据库类型</param>
        /// <returns>DataBaseOper 数据操作类</returns>
        public static IDataBaseOper GetDataBaseOper(eDBType dbtype)
        {
            switch (dbtype)
            {
                case eDBType.Access:
                    if (dbOper_Access == null)
                    {
                        dbOper_Access = new AccessHelper(dbtype);
                    }
                    return dbOper_Access;

                    break;
                case eDBType.Sybase:
                    if (dbOper_Sybase == null)
                    {
                        dbOper_Sybase = new SybaseHelper(dbtype);
                    }
                    return dbOper_Sybase;

                    break;
                case eDBType.Oracle:
                    if (dbOper == null)
                    {
                        dbOper = new OracleHelper(dbtype);
                    }
                    return dbOper;
                    break;
                    break;
                case eDBType.SqlServer:
                    if (dbOper_SqlServer == null)
                    {
                        return dbOper_SqlServer = new SqlServerHelper(dbtype);
                    }
                    return dbOper_SqlServer;
                    break;
                default: //默认是本地库
                    throw new NotImplementedException("未实现的数据库类型读取:" + dbtype.ToString());

                    break;
            }

            return null;
        }

        /// <summary>
        /// 在自定义数据库连接中根据数据库类型获取数据库连接
        /// <example >调用示例
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(AccessConnectionType.Custom,AccessConnectionType.Oralce,"yltl_192.1680.170","valtagedb_c","","valtagedb_c","123456")
        /// </code>
        /// 返回：IDataBaseOper 
        /// </example>
        /// 创建者：wls 20090902
        /// </summary>
        /// <param name="at">数据库的连接方式</param>
        /// <param name="at2">自定义数据库的数据库类型</param>
        /// <param name="ServerName">(net)服务器名称,或 IP + “，端口号”</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="UserIdName">用户</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static IDataBaseOper GetDataBaseOper(eDBType at, eDBType at2, string ServerName, string DataBaseName, string UserIdName, string Password)
        {

            switch (at)
            {
                case eDBType.Access:
                case eDBType.Oracle:
                case eDBType.SqlServer:
                    break;
                default: //默认是本地库
                    break;
            }
            return null;
        }


        /// <summary>
        /// 在自定义数据库连接中根据数据库类型获取数据库连接
        /// <example >调用示例
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(AccessConnectionType.Custom,AccessConnectionType.Oralce,"yltl_192.1680.170","valtagedb_c","","valtagedb_c","123456")
        /// </code>
        /// 返回：IDataBaseOper 
        /// </example>
        /// 创建者：wls 20090902
        /// </summary>
        /// <param name="flag">数据库的连接方式,标示符号 </param>
        /// <param name="at">数据库的连接方式</param>
        /// <param name="at2">自定义数据库的数据库类型</param>
        /// <param name="ServerName">(net)服务器名称</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="UserIdName">用户</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static IDataBaseOper GetDataBaseOper(string flag, eDBType at, eDBType at2, string ServerName, string DataBaseName, string UserIdName, string Password)
        {
            if (dbOper_Custom_Hashtable == null)
            {
                dbOper_Custom_Hashtable = new Hashtable();
            }
            switch (at)
            {
                case eDBType.Access:
                case eDBType.Oracle:
                case eDBType.SqlServer:
                case eDBType.Sybase:
                    break;
                default: //默认是本地库
                    break;
            }
            return null;
        }

        /// <summary>
        /// 根据 isNew 来判断，自定义连接是否需要开启一个新的连接，
        /// </summary>
        /// <param name="isNew">是否是新的连接</param>
        /// <param name="at">数据库连接类型</param>
        /// <param name="ServerName">服务名 或是 DNS服务名 ODBC连接时</param>
        /// <param name="DataBaseName">数据库名称 ODBC连接时 为空 “”</param>
        /// <param name="UserIdName">用户</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static IDataBaseOper GetDataBaseOper(bool isNew, eDBType at, string ServerName, string DataBaseName, string UserIdName, string Password)
        {
            switch (at)
            {
                case eDBType.Access:
                    if (isNew)
                    {
                        dbOper_Custom = new AccessHelper(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper == null)
                        {
                            dbOper_Custom = new AccessHelper(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }
                    return dbOper_Custom;
                    break;
                case eDBType.Sybase:
                    if (isNew)
                    {
                        dbOper_Custom = new SybaseHelperByODBC(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper_Custom == null)
                        {
                            dbOper_Custom = new SybaseHelperByODBC(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }
                    return dbOper_Custom;
                    break;

                case eDBType.Oracle:
                    if (isNew)
                    {
                        dbOper_Custom = new OracleHelper(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper_Custom == null)
                        {
                            dbOper_Custom = new OracleHelper(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }

                    return dbOper_Custom;
                    break;
                case eDBType.SqlServer:
                    if (isNew)
                    {
                        dbOper_Custom = new SqlServerHelper(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper_Custom == null)
                        {
                            dbOper_Custom = new SqlServerHelper(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }

                    return dbOper_Custom;
                    break;
                default: //默认是本地库
                    break;
            }

            return null;
        }


        /// <summary>
        /// 根据 isNew 来判断，自定义连接是否需要开启一个新的连接，
        /// </summary>
        /// <param name="isNew">是否是新的连接</param>
        /// <param name="at">数据库连接类型</param>
        /// <param name="ServerName">服务名 或是 DNS服务名 ODBC连接时</param>
        /// <param name="DataBaseName">数据库名称 ODBC连接时 为空 “”</param>
        /// <param name="UserIdName">用户</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static IDataBaseOper GetDataBaseOper_BySybaseOleDB(bool isNew, eDBType at, string ServerName, string DataBaseName, string UserIdName, string Password)
        {
            switch (at)
            {
                case eDBType.Access:
                    if (isNew)
                    {
                        dbOper_Custom = new AccessHelper(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper == null)
                        {
                            dbOper_Custom = new AccessHelper(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }
                    return dbOper_Custom;
                    break;
                case eDBType.Sybase:
                    if (isNew)
                    {
                        dbOper_Custom = new SybaseHelper(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper_Custom == null)
                        {
                            dbOper_Custom = new SybaseHelper(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }
                    return dbOper_Custom;
                    break;

                case eDBType.Oracle:
                    if (isNew)
                    {
                        dbOper_Custom = new OracleHelper(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper_Custom == null)
                        {
                            dbOper_Custom = new OracleHelper(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }

                    return dbOper_Custom;
                    break;
                case eDBType.SqlServer:
                    if (isNew)
                    {
                        dbOper_Custom = new SqlServerHelper(at, ServerName, DataBaseName, UserIdName, Password);
                    }
                    else
                    {
                        if (dbOper_Custom == null)
                        {
                            dbOper_Custom = new SqlServerHelper(at, ServerName, DataBaseName, UserIdName, Password);
                        }
                    }

                    return dbOper_Custom;
                    break;
                default: //默认是本地库
                    break;
            }

            return null;
        }


        #endregion 获取数据库连接

        /// <summary>
        /// 销毁 IES中 所有的连接
        /// </summary>
        public static void CloseAllCacheIESConn()
        {
            if (dbOper_Custom_Hashtable != null)
            {
                foreach (DictionaryEntry obj in dbOper_Custom_Hashtable)
                {
                    IDataBaseOper idb = (IDataBaseOper)obj.Value;
                    idb.ConnClose();
                    idb = null;
                }
            }

            dbOper_Custom_Hashtable = null;


            if (dbOper_Custom != null)
            {
                try
                {
                    dbOper_Custom.ConnClose();
                    dbOper_Custom = null;
                }
                catch
                {
                    ;
                }
            }

        }

    }

}
