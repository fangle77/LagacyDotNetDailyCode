using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yltl.DBUtility
{
    /// <summary>
    /// 用于构造参数的类
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 创建一个新的参数对象
        /// </summary>
        /// <returns></returns>
        public static Parameter CreateNew()
        {
            return new Parameter();
        }

        Dictionary<string, object> _dicParamNameValue = new Dictionary<string, object>();

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <returns></returns>
        public Parameter Add(string paramName, object paramValue)
        {
            if (_dicParamNameValue.ContainsKey(paramName) == false)
            {
                _dicParamNameValue.Add(paramName, paramValue);
            }
            return this;
        }

        /// <summary>
        /// 添加一个参数集合
        /// </summary>
        /// <param name="paramNameValuePaire"></param>
        /// <returns></returns>
        public Parameter AddRange(Dictionary<string, object> paramNameValuePaire)
        {
            if (paramNameValuePaire == null || paramNameValuePaire.Count == 0) return this;

            foreach (string name in paramNameValuePaire.Keys)
            {
                Add(name, paramNameValuePaire[name]);
            }

            return this;
        }

        /// <summary>
        /// 转换为数据库的对应参数类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public System.Data.Common.DbParameter[] ConvertToDbParameter(eDBType dbType)
        {
            if (_dicParamNameValue == null | _dicParamNameValue.Count == 0) return new System.Data.Common.DbParameter[0];

            System.Data.Common.DbParameter[] pras = new System.Data.Common.DbParameter[_dicParamNameValue.Count];

            switch (dbType)
            {
                case eDBType.Oracle:

                    System.Data.OracleClient.OracleParameter[] pOras =
                        new System.Data.OracleClient.OracleParameter[_dicParamNameValue.Count];

                    int iOra = 0;
                    foreach (string name in _dicParamNameValue.Keys)
                    {
                        pOras[iOra++] = new System.Data.OracleClient.OracleParameter(name, _dicParamNameValue[name]);
                    }
                    return pOras;
                case eDBType.SqlServer:

                    System.Data.SqlClient.SqlParameter[] pSqls =
                        new System.Data.SqlClient.SqlParameter[_dicParamNameValue.Count];

                    int iSql = 0;
                    foreach (string name in _dicParamNameValue.Keys)
                    {
                        pSqls[iSql++] = new System.Data.SqlClient.SqlParameter(name, _dicParamNameValue[name]);
                    }
                    return pSqls;
                case eDBType.Access:
                    System.Data.OleDb.OleDbParameter[] oSqls =
                      new System.Data.OleDb.OleDbParameter[_dicParamNameValue.Count];

                    int iOledb = 0;
                    foreach (string name in _dicParamNameValue.Keys)
                    {
                        oSqls[iOledb++] = new System.Data.OleDb.OleDbParameter(name, _dicParamNameValue[name]);
                    }
                    return oSqls;
                case eDBType.MySql:
                case eDBType.DB2:
                case eDBType.Sybase:
                default:
                    break;
            }

            return new System.Data.Common.DbParameter[0];
        }
    }
}
