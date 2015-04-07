using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public interface ISelect : IQueryExecute
    {
        /// <summary>
        /// 功能类型，此处应为Select模式
        /// </summary>
        string OrderType { get; }

        /// <summary>
        /// 传入需要查询的列名
        /// </summary>
        /// <param name="colunm"></param>
        /// <returns></returns>
        ISelect Columns(params string[] colunm);

        /// <summary>
        /// 追加查询字段
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        ISelect AddColumn(params string[] column);

        /// <summary>
        /// 传入查询数据表名称。表名称为类型T的名称的复数形式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISelect From<T>();

        /// <summary>
        /// 传入查询数据表名称
        /// <param name="tableName">数据表名称</param>
        /// </summary>
        ISelect From(string tableName);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        ISelect Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="whereContext"></param>
        /// <returns></returns>
        ISelect Where(string whereContext);

        /// <summary>
        /// 连接查询数据表
        /// </summary>
        ISelect Join(JoinType joinType, string tableName);

        /// <summary>
        /// 连接查询条件
        /// </summary>
        ISelect On(params string[] joinWhere);

        /// <summary>
        /// Distincte条件判定。调用此方法意味着使用Distincte条件
        /// </summary>
        ISelect Distincted();

        /// <summary>
        /// 调用此方法意味着需要排序查询数据
        /// <param name="orderType">排序方法。OrderType的值之一</param>
        /// <param name="fields">需要用来排序的字段。需要指明具体的表字段</param>
        /// </summary>
        ISelect OrderBy(OrderType orderType, params string[] fields);

        /// <summary>
        /// 调用此方法意味着使用数据分组。
        /// <param name="fields">支持分组的字段。需要指明具体的表字段
        /// <para>触发异常，分组字段需要存在Select查询字段列表中</para>
        /// </param>
        /// </summary>
        ISelect GroupBy(params string[] fields);

        /// <summary>
        /// 此方法支持数据分页查询
        /// <param name="index">页码</param>
        /// <param name="size">条目数</param>
        /// </summary>
        ISelect Page(int index, int size);
    }
}
