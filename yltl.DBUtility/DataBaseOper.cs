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
    /// ���ݿ�����࣬�޷�ֱ��ʵ������ֻ��ͨ��GetDataBaseOper(AccessType at)��ȡ���ݿ�Ĳ�������
    /// ��֤���ݿ����Ч���Ӻ����ٵ�����
    /// </summary>
    public class DataBaseOper
    {
        private static OracleHelper dbOper;//Ĭ���� oracle �����ݿ�����
        private static OracleHelper dbOper_Gpms; //����gpms�����ݿ�
        private static AccessHelper dbOper_Access; //access ���ݿ������
        private static SybaseHelper dbOper_Sybase; //access ���ݿ������
        private static SqlServerHelper dbOper_SqlServer;//sqlserver �����ݿ�����
        private static IDataBaseOper dbOper_Custom;//�Զ�������ݿ����� ֻ�нӿ��ж���ķ�����
        private static OracleHelper dbOper_Gpms_APP; //����gpms�����ݿ� �а����ȥ�ı��

        //private static IList<IDataBaseOper> dbOper_Custom_list;//�Զ�������ݿ����� ֻ�нӿ��ж���ķ�����
        private static Hashtable dbOper_Custom_Hashtable;//�Զ�������ݿ����� ֻ�нӿ��ж���ķ��� �������ӵ�

        #region  ��ȡ���ݿ�����
        /// <summary>
        ///  �������ݿ����ͻ�ȡ������
        ///  �����ߣ�ycf 
        ///  ����ʱ�䣺 20100304
        /// <example >����ʾ��
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(ConnectionString)
        /// </code>
        /// ���أ�IDataBaseOper 
        /// </example>        /// 
        /// </summary>
        /// <param name="ConnectionString">access���ݿ������ַ���</param>
        /// <returns></returns>
        public static IDataBaseOper GetDataBaseOper_ConnectionString(string ConnectionString)
        {
            dbOper_Access = new AccessHelper(ConnectionString);

            return dbOper_Access;


        }
        /// <summary>
        /// �������ݿ����ͻ�ȡ������
        /// <example >����ʾ��
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(AccessConnectionType.Oralce)
        /// </code>
        /// ���أ�IDataBaseOper 
        /// </example>
        /// �����ߣ�wls 20090902
        /// </summary>
        /// <param name="at">���ݿ�����</param>
        /// <returns>DataBaseOper ���ݲ�����</returns>
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
                default: //Ĭ���Ǳ��ؿ�
                    throw new NotImplementedException("δʵ�ֵ����ݿ����Ͷ�ȡ:" + dbtype.ToString());

                    break;
            }

            return null;
        }

        /// <summary>
        /// ���Զ������ݿ������и������ݿ����ͻ�ȡ���ݿ�����
        /// <example >����ʾ��
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(AccessConnectionType.Custom,AccessConnectionType.Oralce,"yltl_192.1680.170","valtagedb_c","","valtagedb_c","123456")
        /// </code>
        /// ���أ�IDataBaseOper 
        /// </example>
        /// �����ߣ�wls 20090902
        /// </summary>
        /// <param name="at">���ݿ�����ӷ�ʽ</param>
        /// <param name="at2">�Զ������ݿ�����ݿ�����</param>
        /// <param name="ServerName">(net)����������,�� IP + �����˿ںš�</param>
        /// <param name="DataBaseName">���ݿ�����</param>
        /// <param name="UserIdName">�û�</param>
        /// <param name="Password">����</param>
        /// <returns></returns>
        public static IDataBaseOper GetDataBaseOper(eDBType at, eDBType at2, string ServerName, string DataBaseName, string UserIdName, string Password)
        {

            switch (at)
            {
                case eDBType.Access:
                case eDBType.Oracle:
                case eDBType.SqlServer:
                    break;
                default: //Ĭ���Ǳ��ؿ�
                    break;
            }
            return null;
        }


        /// <summary>
        /// ���Զ������ݿ������и������ݿ����ͻ�ȡ���ݿ�����
        /// <example >����ʾ��
        /// <code>
        /// yltl.DBUtility.DataBaseOper.GetDataBaseOper(AccessConnectionType.Custom,AccessConnectionType.Oralce,"yltl_192.1680.170","valtagedb_c","","valtagedb_c","123456")
        /// </code>
        /// ���أ�IDataBaseOper 
        /// </example>
        /// �����ߣ�wls 20090902
        /// </summary>
        /// <param name="flag">���ݿ�����ӷ�ʽ,��ʾ���� </param>
        /// <param name="at">���ݿ�����ӷ�ʽ</param>
        /// <param name="at2">�Զ������ݿ�����ݿ�����</param>
        /// <param name="ServerName">(net)����������</param>
        /// <param name="DataBaseName">���ݿ�����</param>
        /// <param name="UserIdName">�û�</param>
        /// <param name="Password">����</param>
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
                default: //Ĭ���Ǳ��ؿ�
                    break;
            }
            return null;
        }

        /// <summary>
        /// ���� isNew ���жϣ��Զ��������Ƿ���Ҫ����һ���µ����ӣ�
        /// </summary>
        /// <param name="isNew">�Ƿ����µ�����</param>
        /// <param name="at">���ݿ���������</param>
        /// <param name="ServerName">������ ���� DNS������ ODBC����ʱ</param>
        /// <param name="DataBaseName">���ݿ����� ODBC����ʱ Ϊ�� ����</param>
        /// <param name="UserIdName">�û�</param>
        /// <param name="Password">����</param>
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
                default: //Ĭ���Ǳ��ؿ�
                    break;
            }

            return null;
        }


        /// <summary>
        /// ���� isNew ���жϣ��Զ��������Ƿ���Ҫ����һ���µ����ӣ�
        /// </summary>
        /// <param name="isNew">�Ƿ����µ�����</param>
        /// <param name="at">���ݿ���������</param>
        /// <param name="ServerName">������ ���� DNS������ ODBC����ʱ</param>
        /// <param name="DataBaseName">���ݿ����� ODBC����ʱ Ϊ�� ����</param>
        /// <param name="UserIdName">�û�</param>
        /// <param name="Password">����</param>
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
                default: //Ĭ���Ǳ��ؿ�
                    break;
            }

            return null;
        }


        #endregion ��ȡ���ݿ�����

        /// <summary>
        /// ���� IES�� ���е�����
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
