using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public interface IInsert : IQueryExecute
    {
        /// <summary>
        /// 功能类型，此处应为Insert模式
        /// </summary>
        string OrderType { get; }

        /// <summary>
        /// 传入新增信息目标数据表，表名称为类型T名称的复数形式
        /// </summary>
        IInsert Into<T>() where T : new();

        /// <summary>
        /// 传入新增信息目标数据表名称
        /// </summary>
        IInsert Into(string tableName);

        /// <summary>
        /// 传入新增信息的字段名称，需要和传入的值长度一致
        /// </summary>
        IInsert Table(params string[] fieldCollection);

        /// <summary>
        /// 传入新增信息的字段值，需要和传入的字段名称长度一致
        /// </summary>
        IInsert Values(params object[] valueCollection);

        /// <summary>
        /// 传入新增信息指定字段的值
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        IInsert InsertValue(string fieldName, string value);
    }
}
