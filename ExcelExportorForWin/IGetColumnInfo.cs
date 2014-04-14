using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelExportorForWin
{
    /// <summary>
    /// 获取列元素的接口
    /// </summary>
    public interface IGetColumnInfo
    {
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// 获取列元素信息集合
        /// </summary>
        /// <returns></returns>
        List<ColumnElement> GetColumnElements();
    }
}
