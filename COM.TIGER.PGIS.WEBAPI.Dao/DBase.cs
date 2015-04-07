using System;   
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using DEntLib = Microsoft.Practices.EnterpriseLibrary.Data;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 数据库操作基类
    /// <para>提供基础数据哭操作程序</para>
    /// </summary>
    public class DBase:Base
    {
        /// <summary>
        /// 时间范围值，取指定时间的 +- 范围内的数据，单位是分钟
        /// </summary>
        protected double TimeRange
        {
            get
            {
                double val = 0.00d;
                double.TryParse(System.Configuration.ConfigurationManager.AppSettings["timerange"], out val);
                return val;
            }
        }

        public DBase() { }

        protected COM.TIGER.PGIS.WEBAPI.IDao.IInsert InsertHandler
        {
            get
            {
                return new Insert().InsertHandler;
            }
        }

        protected COM.TIGER.PGIS.WEBAPI.IDao.IDelete DeleteHandler
        {
            get
            {
                return new Delete().DeleteHandler;
            }
        }

        protected COM.TIGER.PGIS.WEBAPI.IDao.IUpdate UpdateHandler
        {
            get
            {
                return new Update().UpdateHandler;
            }
        }

        protected COM.TIGER.PGIS.WEBAPI.IDao.ISelect SelectHandler
        {
            get
            {
                return new Select().SelectHandler;
            }
        }

        /// <summary>
        /// 从数据库中阅读数据并转换成实体数据
        /// </summary>
        /// <typeparam name="T">原型</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected List<T> ExecuteList<T>(System.Data.IDataReader reader) where T : new()
        {
            var result = new List<T>();
            var type = typeof(T);
            var properties = type.GetProperties();
            while (reader.Read())
            {
                var t = new T();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var property = properties.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                    if (!reader.IsDBNull(i) && property != null && property.CanWrite)
                    {
                        var obj = reader.GetValue(i);
                        property.SetValue(t, obj, null);
                    }
                }
                result.Add(t);
            }
            reader.Close();
            reader.Dispose();
            return result;
        }

        /// <summary>
        /// 将Datatable类型数据转换成实体数据
        /// </summary>
        /// <typeparam name="T">原型</typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected List<T> ExecuteList<T>(System.Data.DataTable dt) where T : new()
        {
            var result = new List<T>();
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                var t = new T();
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    var name = dt.Columns[i].ColumnName;
                    var property = properties.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                    var obj = row.ItemArray[i];
                    if (property != null && property.CanWrite && obj != null)
                    {
                        property.SetValue(t, obj, null);
                    }
                }
                result.Add(t);
            }
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 从数据库中阅读数据并转换成实体数据，得到首条实体信息之后，停止阅读。
        /// <para>如果没有查询得到数据，返回原型的默认值</para>
        /// </summary>
        /// <typeparam name="T">原型</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected T ExecuteEntity<T>(System.Data.IDataReader reader) where T : new()
        {
            if (!reader.Read()) return default(T);
            var t = new T();
            var type = typeof(T);
            var properties = type.GetProperties();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                var property = properties.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                if (!reader.IsDBNull(i) && property != null && property.CanWrite)
                {
                    var obj = reader.GetValue(i);
                    property.SetValue(t, obj, null);
                }
            }
            reader.Close();
            reader.Dispose();
            return t;
        }

        /// <summary>
        /// 将Datatable类型数据转换成实体数据，得到首条实体信息之后，停止转换。
        /// <para>如果没有查询得到数据，返回原型的默认值</para>
        /// </summary>
        /// <typeparam name="T">原型</typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected T ExecuteEntity<T>(System.Data.DataTable dt) where T : new()
        {
            if(dt.Rows.Count == 0) return default(T);
            var t = new T();
            var type = typeof(T);
            var properties = type.GetProperties();
            var row = dt.Rows[0];
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                var name = dt.Columns[i].ColumnName;
                var property = properties.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                var obj = row.ItemArray[i];
                if (property != null && property.CanWrite && obj != null)
                {
                    property.SetValue(t, obj, null);
                }
            }
            dt.Dispose();
            return t;
        }

        /// <summary>
        /// 获取分页数据
        /// <para>支持单一排序方式</para>
        /// </summary>
        /// <typeparam name="T">实体原型</typeparam>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页数据数量</param>
        /// <param name="expression">查询条件</param>
        /// <param name="orderType">排序方式</param>
        /// <param name="records">当前查询条件下总条目数</param>
        /// <param name="sortFields">当前排序条件下排序字段</param>
        /// <returns></returns>
        protected List<T> Paging<T>(int index, int size,
            System.Linq.Expressions.Expression<Func<T, bool>> expression,
            OrderType orderType, out int records, params string[] sortFields)
            where T : new()
        {
            //计算总条目数
            records = GetCount<T>(expression);
            //获取当前页码数据
            if (records == 0) return new List<T>();
            //无条件
            var query = SelectHandler.From<T>().OrderBy(orderType, sortFields).Page(index, size);
            if (expression == null)
                return ExecuteList<T>(query.Execute().ExecuteDataReader());
            //有条件
            var handler = query.IQueryWhere.Where<T>(expression).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<T>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 获取当前查询条件下总条目数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected int GetCount<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            var query = SelectHandler.Columns("Count(0) as c").From<T>();
            //无条件
            if (expression == null)
                return (int)(query.Execute().ExecuteSaclar());
            //有条件
            var handler = query.IQueryWhere.Where<T>(expression).Where<IDao.ISelect>(query).Execute();
            return (int)(handler.ExecuteSaclar());
        }

        /// <summary>
        /// 查询指定条件的实体
        /// <para>该方法不适合多重查询语句</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        protected T GetEntity<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T:new()
        {
            var query = SelectHandler.From<T>().Where<T>(expression);
            return ExecuteEntity<T>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 查询指定条件的实体集合
        /// <para>该方法不适合多重查询语句</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        protected List<T> GetEntities<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : new()
        {
            var query = SelectHandler.From<T>();
            var handler = query.IQueryWhere
                .Where<T>(expression)
                .Where<IDao.ISelect>(query)
                .Execute();

            return ExecuteList<T>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 移除指定条件的数据
        /// <para>该方法不适合多重查询语句</para> 
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        protected int DeleteOperate<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : new()
        {
            var query = DeleteHandler.From<T>();
            var handler = query.IQueryWhere.Where<T>(expression).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /*****************************************************************
         *  批量处理分页，通过JOIN方式
         * ***************************************************************
         */

        /// <summary>
        /// 获取指定查询的总记录数
        /// </summary>
        /// <param name="query">当前查询</param>
        /// <returns></returns>
        protected int GetCount(IDao.ISelect query)
        {
            query = SelectHandler.Columns("count(0)").From(string.Format("({0}) as temp", query.CommandText));
            var rst = Convert.ToInt32(query.Execute().ExecuteSaclar());
            return rst;
        }

        /// <summary>
        /// 分页查询数据，并获取当前页码的数据
        /// </summary>
        /// <param name="query">指定的查询条件表达式</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询的受影响记录数</param>
        /// <returns></returns>
        protected List<T> Paging<T>(IDao.ISelect query, int index, int size, out int records) where T : new()
        {
            records = GetCount(query);
            query = SelectHandler.Columns("temp.*").From(string.Format("({0}) as temp", query.CommandText))
                .Where(string.Format("temp.rownum > {0} and temp.rownum <= {1}", (index - 1) * size, index * size));
            return ExecuteList<T>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取分页查询表达式
        /// </summary>
        /// <param name="tableName">排序数据字段所在数据表名称</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="columns">查询的字段组，以“，”分隔</param>
        /// <returns>eg.select $tablename.*, ROW_NUMBER() OVER ( order by $tablename.$field desc ) AS rownum from  $tablename</returns>
        protected IDao.ISelect GetPageQuery(string sortTableName, string sortField, params string[] columns)
        {
            if (columns.Length == 0)
            {
                columns = new string[] { string.Format("{0}.*", sortTableName) };
            }
            var arr = new List<string>();
            arr.AddRange(columns);
            arr.Add(string.Format("ROW_NUMBER() OVER ( order by {0}.{1} desc ) AS rownum", sortTableName, sortField));
            var query = SelectHandler.Columns(arr.ToArray()).From(sortTableName);
            return query;
        }

        /// <summary>
        /// 匹配地址
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="query"></param>
        protected void MatchAddress(string pattern, ref IDao.ISelect query)
        {
            var patterns = pattern.ToCharArray();
            for (var i = 0; i < patterns.Length; i++)
            {
                var str = string.Format("{0}", patterns[i]);
                if (string.IsNullOrWhiteSpace(str))
                    continue;

                query = query.Where<Model.Address>(t => t.Content.Like(str));
            }
        }
        
        /// <summary>
        /// 从指定的座标组中获取矩形左上角座标和右下角座标
        /// </summary>
        /// <param name="coords">指定的座标组</param>
        /// <param name="x1">左上角座标横坐标</param>
        /// <param name="y1">左上角座标纵坐标</param>
        /// <param name="x2">右下角座标横坐标</param>
        /// <param name="y2">右下角座标纵坐标</param>
        protected void GetXY(double[] coords, out double x1, out double y1, out double x2, out double y2)
        {
            var xs = coords.Where((t, index) => index % 2 == 0);
            var ys = coords.Where((t, index) => index % 2 != 0);

            x1 = xs.Min();
            x2 = xs.Max();
            y1 = ys.Min();
            y2 = ys.Max();
        }

        /// <summary>
        /// 转换指定的座标为平面直角座标
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        protected WEBAPI.Common.GDI.Point[] GetPoints(double[] coords)
        {
            var arr = new List<WEBAPI.Common.GDI.Point>();
            for (var i = 0; i < coords.Length - 1; i += 2)
            {
                var point = new WEBAPI.Common.GDI.Point() { X = coords[i], Y = coords[i + 1] };
                arr.Add(point);
            }
            return arr.ToArray();
        }

        /// <summary>
        /// 获取地址的详细信息
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="insertFlag"></param>
        /// <returns></returns>
        protected override Model.Address GetAddressInfo(string addr, bool insertFlag = true)
        {
            if (string.IsNullOrWhiteSpace(addr))
                return null;

            var query = SelectHandler.From<Model.Address>().Where<Model.Address>(t => t.Content == addr);
            var e = ExecuteEntity<Model.Address>(query.Execute().ExecuteDataReader());

            if (e != null)
                return e;

            if (!insertFlag)
                return null;

            AddressHandler.Handler.InsertEntity(new Model.Address() { Content = addr });
            return GetAddressInfo(addr, false);
        }
    }

    public abstract class Base
    {
        public virtual int InsertEntity(object obj)
        {
            return -1;
        }

        public virtual int UpdateEntity(object obj)
        {
            return -1;
        }

        public virtual int DeleteEntities(params string[] ids)
        {
            return -1;
        }

        /// <summary>
        /// 获取指定实体类型的类型名称
        /// </summary>
        /// <typeparam name="T">需要调用的实体类型</typeparam>
        /// <returns></returns>
        public virtual string GetTableName<T>()
        {
            var type = typeof(T);
            var tablename = string.Format("{0}{1}{2}",
                Common.Attributes.AttributeHandler.GetPrefixName(type),
                type.Name,
                Common.Attributes.AttributeHandler.GetSuffixName(type));
            return tablename;
        }

        /// <summary>
        /// 校验地址信息
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        protected virtual Model.Address GetAddressInfo(string addr, bool insertFlag = true)
        {
            return null;
        }
    }

    /// <summary>
    /// 人员性质
    /// </summary>
    public enum LiveProperty:byte
    { 
        /// <summary>
        /// 无
        /// </summary>
        NONE = 0x00,
        /// <summary>
        /// 常住人员
        /// </summary>
        CZ,
        /// <summary>
        /// 暂住人员
        /// </summary>
        ZZ,
        /// <summary>
        /// 境外人员
        /// </summary>
        JW,
        /// <summary>
        /// 重点人员
        /// </summary>
        ZD,
    }

    /// <summary>
    /// 用于处理T-SQL语句中的函数处理
    /// <para>包括但不限制like,in内置函数</para>
    /// </summary>
    public static class T_SQL_Helper
    {
        public static bool Like(this string str, string val)
        {
            return true;
        }

        public static bool In(this object obj, params string[] items)
        {
            return true;
        }

        public static bool NotLike(this string str, string val)
        {
            return true;
        }

        public static bool NotIn(this object obj, params string[] items)
        {
            return true;
        }
    }

    public static class IEnumerable_Helper
    {
        /// <summary>
        /// 用于排除相同的对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }

            }

        }
    }

    //自定义异常类
    public class NoCompleteException : Exception
    { 
        
    }
}
