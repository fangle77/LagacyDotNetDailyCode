using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace yltl.DBUtility
{
    /// <summary>
    /// 数据库结构的接口
    /// </summary>
    public interface IDBSchema : IOperationEvent
    {
        /// <summary>
        /// 返回数据库中用户的所有的表
        /// </summary>
        /// <returns></returns>
        DataTable GetTables();

        /// <summary>
        /// 返回某个表的所有列信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable GetColumns(string tableName);

        /// <summary>
        /// 返回用户的视图信息
        /// </summary>
        /// <returns></returns>
        DataTable GetViews();
    }
}
