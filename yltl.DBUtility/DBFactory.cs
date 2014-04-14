using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace yltl.DBUtility
{
    /// <summary>
    /// 数据库连接操作类的创建工厂
    /// </summary>
    public class DBFactory
    {
        /* connectionString example
 * 
 * private Dictionary<eDBType, string> _connectStringDic = new Dictionary<eDBType, string>()
{
    {eDBType.Access,"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=voltagedb_c.mdb;jet OleDB:Database Password=123456"},
    {eDBType.DB2,""},
    {eDBType.MySql,""},
    {eDBType.Oracle,"Data Source=lan_189;User ID=user_ahdy_shi;Password=user_ahdy_shi"},
    {eDBType.SqlServer,"server=192.168.0.161;database=voltagedb_c;uid=sa;pwd=111111"},
    {eDBType.Sybase,""},
};
 */


        private static Func<string, string> _DecodeMethod = Encrypt.DeCode;
        /// <summary>
        /// 获取或设置解密方法：如果不设置则按照默认方式解密；可以自定义
        /// </summary>
        public static Func<string, string> DecodeMethod
        {
            get { return _DecodeMethod; }
            set { _DecodeMethod = value; }
        }

        private static Func<string, string> _EncodeMethod = Encrypt.EnCode;
        /// <summary>
        /// 获取或设置加密方法：如果不设置则按默认的加密方法；可以自定义
        /// </summary>
        public static Func<string, string> EncodeMethod
        {
            get { return _EncodeMethod; }
            set { _EncodeMethod = value; }
        }

        /// <summary>
        /// 创建Access数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDBOperator CreateAccess(string connectionString)
        {
            return Create(eDBType.Access, connectionString);
        }

        /// <summary>
        /// 创建Access数据库连接
        /// </summary>
        /// <param name="datasource">数据库路径</param>
        /// <param name="password">密码，可为空</param>
        /// <returns></returns>
        public static IDBOperator CreateAccess(string datasource, string password)
        {
            string pwd = "";
            if (!string.IsNullOrEmpty(password))
            {
                pwd = string.Format("jet OleDB:Database Password={0}", password);
            }
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};{1}", datasource, pwd);

            return Create(eDBType.Access, connectionString);
        }

        /// <summary>
        /// 创建Oracle数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDBOperator CreateOracle(string connectionString)
        {
            return Create(eDBType.Oracle, connectionString);
        }

        /// <summary>
        /// 创建Oracle数据库连接
        /// </summary>
        /// <param name="datasource">本地服务名</param>
        /// <param name="password">用户</param>
        /// <param name="user">密码</param>
        /// <returns></returns>
        public static IDBOperator CreateOracle(string datasource, string user, string password)
        {
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2}", datasource, user, password);

            return Create(eDBType.Oracle, connectionString);
        }

        /// <summary>
        /// 创建SqlServer数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDBOperator CreateSqlServer(string connectionString)
        {
            return Create(eDBType.SqlServer, connectionString);
        }

        /// <summary>
        /// 创建SqlServer数据库连接
        /// </summary>
        /// <param name="datasource">数据库路径</param>
        /// <param name="password">密码，可为空</param>
        /// <returns></returns>
        public static IDBOperator CreateSqlServer(string server, string database, string user, string password)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}", server, database, user, password);

            return Create(eDBType.SqlServer, connectionString);
        }

        /// <summary>
        /// 根据数据库类型和连接字符串，创建一个数据库操作接口
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public static IDBOperator Create(eDBType dbtype, string connectionString)
        {
            switch (dbtype)
            {
                case eDBType.Access:
                    return new AccessHelper(connectionString);
                case eDBType.Oracle:
                    return new OracleHelper(connectionString);
                case eDBType.SqlServer:
                    return new SqlServerHelper(connectionString);
                case eDBType.MySql:
                case eDBType.DB2:
                case eDBType.Sybase:
                default:
                    throw new NotImplementedException("未实现的数据库类型" + dbtype);
            }
        }

        /// <summary>
        /// 通过配置文件的配置来创建数据库连接，如果有多个相同的Name，则取第一个
        /// </summary>
        /// <param name="connectionName">对应配置中的Name属性</param>
        /// <returns></returns>
        public static IDBOperator CreateByConfig(string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException("connectionName", "连接名称不能为null，");

            var dic = DBConfigDic;
            foreach (var type in dic.Keys)
            {
                if (dic[type].ContainsKey(connectionName))
                {
                    return Create(type, dic[type][connectionName]);
                }
            }
            throw new Exception("在配置中没有找到连接名称：" + connectionName);
        }

        /// <summary>
        /// 通过配置文件的配置来创建数据库连接，如果有多个同类型的连接，则只取第一个
        /// </summary>
        /// <param name="dbtype">对应配置中的DBType属性</param>
        /// <returns></returns>
        public static IDBOperator CreateByConfig(eDBType dbtype)
        {
            var dic = DBConfigDic;

            if (dic.ContainsKey(dbtype))
            {
                return Create(dbtype, dic[dbtype].First().Value);
            }

            throw new Exception("在配置中没有找到数据库类型：" + dbtype);
        }

        /// <summary>
        /// 通过配置文件的配置来创建数据库连接，可以用来区分同一数据库类型下的多个连接
        /// </summary>
        /// <param name="dbtype">对应配置中的DBType属性</param>
        /// <param name="connectionName">对应配置中的Name属性</param>
        /// <returns></returns>
        public static IDBOperator CreateByConfig(eDBType dbtype, string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException("connectionName", "连接名称不能为null，");

            var dic = DBConfigDic;
            if (dic.ContainsKey(dbtype))
            {
                if (dic[dbtype].ContainsKey(connectionName))
                {
                    return Create(dbtype, dic[dbtype][connectionName]);
                }
            }
            throw new Exception(string.Format("在配置中没有找到数据库类型-名称：{0}-{1}", dbtype, connectionName));
        }

        private static Dictionary<eDBType, Dictionary<string, string>> _dicType_Name_ConnString;
        private static Dictionary<eDBType, Dictionary<string, string>> DBConfigDic
        {
            get
            {
                if (_dicType_Name_ConnString == null)
                {
                    //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    DBConnectionSection section = (DBConnectionSection)System.Configuration.ConfigurationManager.GetSection("DBConnections");

                    if (section == null)
                    {
                        throw new Exception("未找到配置节DBConnections");
                    }

                    var eleColection = section.DBConnections;
                    if (eleColection == null || eleColection.Count == 0)
                    {
                        throw new Exception("未找到配置DBConnections\\DBConnection");
                    }

                    Dictionary<eDBType, Dictionary<string, string>> dicType_Name_ConnString = new Dictionary<eDBType, Dictionary<string, string>>();

                    foreach (DBConnectionElement ele in eleColection)
                    {
                        string connectionString = ele.IsEncrypt ? DecodeMethod(ele.ConnectionString) : ele.ConnectionString;

                        if (dicType_Name_ConnString.ContainsKey(ele.DBType) == false)
                        {
                            dicType_Name_ConnString.Add(ele.DBType, new Dictionary<string, string>()
                                    {
                                        {ele.Name,connectionString}
                                    });
                        }
                        else
                        {
                            var dic = dicType_Name_ConnString[ele.DBType];
                            if (dic.ContainsKey(ele.Name) == false)
                            {
                                dic.Add(ele.Name, connectionString);
                            }
                            dicType_Name_ConnString[ele.DBType] = dic;
                        }
                    }

                    _dicType_Name_ConnString = dicType_Name_ConnString;
                }
                return _dicType_Name_ConnString;
            }
        }
    }
}
