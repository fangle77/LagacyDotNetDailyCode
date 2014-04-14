using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yltl.DBUtility
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDBOperator : IDBSchema, IOperationEvent
    {
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        eDBType DBType { get; }

        /// <summary>
        /// 获取或设置连接字符串
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 测试连接，返回是否连接成功，及连接信息。
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool TestConnection(out string msg);

        /// <summary>
        /// 执行sql语句并返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns></returns>
        int ExecuteSql(string sql);

        /// <summary>
        /// 使用参数化方式执行sql命令
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql(string sql, params System.Data.Common.DbParameter[] cmdParms);

        /// <summary>
        /// 执行查询语句，并返回结果集的DataTable
        /// </summary>
        /// <param name="sql">要执行的查询语句</param>
        /// <returns></returns>
        System.Data.DataTable GetDataTable(string sql);

        /// <summary>
        /// 使用参数化方式执行查询
        /// </summary>
        /// <param name="sql">要查询的sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>查询结果</returns>
        System.Data.DataTable GetDataTable(string sql, params System.Data.Common.DbParameter[] cmdParms);

        /// <summary>
        /// 获取DataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        System.Data.Common.DbDataReader GetDataReader(string sql);

        /// <summary>
        /// 执行查询语句，并返回首行首列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        string ExecuteScalar(string sql);
    }
}
