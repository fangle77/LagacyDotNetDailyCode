using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yltl.DBUtility
{
    /// <summary>
    /// 操作事件接口
    /// </summary>
    public interface IOperationEvent
    {
        /// <summary>
        /// 写日志的事件
        /// </summary>
        event Action<string> LogHandler;

        /// <summary>
        /// 在抛出异常时触发
        /// </summary>
        event Action<Exception> AfterExceptionThrow;
    }
}
