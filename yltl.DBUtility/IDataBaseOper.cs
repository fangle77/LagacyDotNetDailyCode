using System;
using System.Data.Common;
using System.Collections.Generic;

namespace yltl.DBUtility
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDataBaseOper
    {
        /// <summary>
        /// 启动事务处理
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 提交事务处理
        /// </summary>
        void Commit();
        /// <summary>
        /// 关闭连接
        /// </summary>
        void ConnClose();
        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// 数据库名称或用户名
        /// </summary>
        string DataBaseName { get; set; }
        /// <summary>
        /// 执行简单的sql语句，返回受影响的行数
        /// </summary>
        /// <param name="sqlText">需要被执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNoneQuery(string sqlText);
        /// <summary>
        /// 执行带参数的sql语句，返回受影响的行数
        /// </summary>
        /// <param name="sqlText">需要被执行的sql语句</param>
        /// <param name="cmdParms">sql语句的参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNoneQuery(string sqlText, params System.Data.Common.DbParameter[] cmdParms);
        /// <summary>
        /// 执行存储过程，无返回值
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数</param>
        /// <param name="blTrans">是否启用事务处理</param>
        void ExecuteProc(string procName, System.Data.Common.DbParameter[] prams, bool blTrans);
        /// <summary>
        /// 执行存储过程，返回DbDataReader
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数</param>
        /// <param name="blTrans">是否启用事务处理</param>
        /// <returns>返回DbDataReader</returns>
        System.Data.Common.DbDataReader ExecuteProcRetDataReader(string procName, System.Data.Common.DbParameter[] prams, bool blTrans);
        /// <summary>
        /// 执行存储过程，返回 DataSet
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数</param>
        /// <param name="blTrans">是否启用事务处理</param>
        /// <returns>返回 DataSet</returns>
        System.Data.DataSet ExecuteProcRetDataSet(string procName, System.Data.Common.DbParameter[] prams, bool blTrans);
        /// <summary>
        /// 执行存储过程，返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">参数</param>
        /// <param name="blTrans">是否启用事务处理</param>
        /// <returns>返回 DataTable</returns>
        System.Data.DataTable ExecuteProcRetDataTable(string procName, System.Data.Common.DbParameter[] prams, bool blTrans);
        /// <summary>
        /// 执行带参数的sql语句，返回 DbDataReader
        /// </summary>
        /// <param name="sqlText">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>返回 DbDataReader</returns>
        System.Data.Common.DbDataReader ExecuteReader(string sqlText, params System.Data.Common.DbParameter[] cmdParms);
        /// <summary>
        /// 执行sql语句返回首行首列的值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回首行首列的值 string </returns>
        string ExecuteScalar(string sql);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <returns></returns>
        object ExecuteSingle(string SQLString);
        /// <summary>
        /// 执行带参数的sql语句
        /// </summary>
        /// <param name="sqlText">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        object ExecuteSingle(string sqlText, params System.Data.Common.DbParameter[] cmdParms);
        /// <summary>
        /// 执行简单的sql语句，返回受影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql(string sql);
        /// <summary>
        /// 事务方式执行 多条的sql语句
        /// </summary>
        /// <param name="sqlTextList">多条的sql语句集</param>
        void ExecuteSqlTran(System.Collections.Hashtable sqlTextList);
        /// <summary>
        /// 事务方式执行 多条的sql语句
        /// </summary>
        /// <param name="sqlList">多条的sql语句集</param>
        /// <returns>执行成功还是失败</returns>
        bool ExecuteSqlTran(IList<string> sqlList);
        /// <summary>
        /// 执行简单的sql语句，返回数据适配器 DbDataAdapter
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回数据适配器 DbDataAdapter</returns>
        System.Data.Common.DbDataAdapter GetDataAdapter(string sql);
        /// <summary>
        /// 执行简单的sql语句，返回 DbDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回 DbDataReader</returns>
        System.Data.Common.DbDataReader GetDataReader(string sql);
        /// <summary>
        /// 执行简单的sql，返回 DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回 DataSet</returns>
        System.Data.DataSet GetDataSet(string sql);
        /// <summary>
        /// 执行简单的sql，返回 DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回 DataTable</returns>
        System.Data.DataTable GetDataTable(string sql);
        /// <summary>
        /// 连接判断
        /// </summary>
        /// <returns></returns>
        bool IsConnection();
        /// <summary>
        /// 是否存在的判断
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>存在true：不存在：false</returns>
        bool IsExist(string sql);
        /// <summary>
        /// 获取或设置密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 执行带参数的sql语句，返回 DataSet
        /// </summary>
        /// <param name="sqlText">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>返回 DataSet</returns>
        System.Data.DataSet Query(string sqlText, params System.Data.Common.DbParameter[] cmdParms);
        /// <summary>
        /// 执行简单的sql语句，返回 DataTable
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <returns>返回 DataTable</returns>
        System.Data.DataTable Query(string SQLString);
        /// <summary>
        /// 执行带参数的sql语句，返回 DataTable
        /// </summary>
        /// <param name="sqlText">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>返回 DataTable</returns>
        System.Data.DataTable QueryTable(string sqlText, params System.Data.Common.DbParameter[] cmdParms);

        /// <summary>
        /// 分页函数的调用
        /// </summary>
        /// <param name="pageIndex">页位置</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="uniqueCols"></param>
        /// <param name="countSQL"></param>
        /// <param name="showString"></param>
        /// <param name="queryString">查询语句</param>
        /// <param name="whereString">条件</param>
        /// <param name="orderString">排序</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        System.Data.DataTable QueryTablePagging(int pageIndex, int pageSize, string uniqueCols, string countSQL, string showString,
                    string queryString, string whereString, string orderString,
            out int pageCount, ref int recordCount);

        /// <summary>
        /// 事务的回滚
        /// </summary>
        void RollBack();
        /// <summary>
        /// 获取或设置 服务器
        /// </summary>
        string ServerName { get; set; }
        /// <summary>
        /// 获取或设置 用户ID
        /// </summary>
        string UserIddName { get; set; }
        /// <summary>
        /// 获取或设置 用户ID
        /// </summary>
        bool IsMulitThread { get; set; }

        //DbParameter MakeInParam(string ParamName, DbType DbType, int Size, object Value);
        //DbParameter MakeOutParam(string ParamName, DbType DbType, int Size);
        //DbParameter MakeParam(string ParamName, DbType DbType, Int32 Size, ParameterDirection Direction, object Value);
    }
}
