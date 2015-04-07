using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    /// <summary>
    /// API基类控制器
    /// <para>其他控制器都必须继承于API基类控制器</para>
    /// </summary>
    [ControllerAuthentizationFilter]
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// 获取COM.TIGER.PGIS.WEBAPI.ApiResult协议类型的结果
        /// </summary>
        /// <typeparam name="T">操作执行结果，必须支持序列化</typeparam>
        /// <param name="message">API执行结果类型描述信息</param>
        /// <param name="status">API执行结果类型</param>
        /// <param name="result">API执行结果</param>
        /// <returns>COM.TIGER.PGIS.WEBAPI.ApiResult协议类型的一个新实例</returns>
        protected virtual ApiResult<T> Result<T>(string message, ResultStatus status, T result)
        {
            return ApiResult<T>.Instance(message, status, result);
        }

        /// <summary>
        /// 获取COM.TIGER.PGIS.WEBAPI.ApiResult协议类型的结果
        /// <para>封装正确的结果，并返回</para>
        /// </summary>
        /// <typeparam name="T">操作执行结果，必须支持序列化</typeparam>
        /// <param name="result">API执行结果</param>
        /// <returns></returns>
        protected ApiResult<T> ResultOk<T>(T result)
        {
            return Result<T>("OK", ResultStatus.OK, result);
        }

        /// <summary>
        /// 获取COM.TIGER.PGIS.WEBAPI.ApiResult协议类型的结果
        /// <para>封装错误的结果，并返回</para>
        /// </summary>
        /// <typeparam name="T">操作执行结果，必须支持序列化</typeparam>
        /// <param name="message">错误信息</param>
        /// <returns></returns>
        protected ApiResult<T> ResultFaild<T>(string message)
        {
            return Result<T>(message, ResultStatus.Failed, default(T));
        }

        /// <summary>
        /// 获取COM.TIGER.PGIS.WEBAPI.ApiResult协议类型的结果
        /// <para>封装异常的结果，并返回</para>
        /// </summary>
        /// <typeparam name="T">操作执行结果，必须支持序列化</typeparam>
        /// <param name="message">异常信息</param>
        /// <returns></returns>
        protected ApiResult<T> ResultException<T>(string message)
        {
            return Result<T>(message, ResultStatus.Exception, default(T));
        }

        /// <summary>
        /// 获取查询参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetQueryParamsCollection<T>() where T : new()
        {
            return GetQueryParamsCollection<T>(new T());
        }

        /// <summary>
        /// 获取查询参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        protected T GetQueryParamsCollection<T>(T t) where T : new()
        {
            var args = Request.GetQueryNameValuePairs();
            if (args.Count() == 0) return default(T);

            var properties = t.GetType().GetProperties();
            foreach (var kp in args)
            {
                var p = properties.FirstOrDefault(x => x.Name.Trim().ToLower() == kp.Key.Trim().ToLower());
                if (kp.Value != null && p.CanWrite)
                {
                    var o = ParseObject(p.PropertyType, kp.Value);
                    p.SetValue(t, o, null);
                }
            }
            return t;
        }

        /// <summary>
        /// 调用Parse(string)方法将string数据类型转换成其他数据类型
        /// <para>如果需要转换的类型为string类型，将直接返回原数据</para>
        /// <para>如果需要转换的类型为非string类型，将调用Parse(string)方法转换数据，并返回新类型的数据</para>
        /// <para>如果需要转换的类型，或者继承类型中不存在Parse(string)方法，将抛出异常System.ArgumentNullException</para>
        /// </summary>
        /// <param name="t">需要转换的类型</param>
        /// <param name="value">需要转换的值</param>
        /// <returns></returns>
        private object ParseObject(Type tp, string value)
        {
            if (tp == typeof(string)) return value;

            //判断类型是否可以为空。
            //  如果可以为空那么返回不可以为空的Type，否者返回原Type
            if (tp.IsGenericType && (tp.GetGenericTypeDefinition() == typeof(Nullable) || tp.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                tp = tp.GetGenericArguments()[0];
            }
            //转换数据
            //  调用Parse方法直接转换
            var m = tp.GetMethod("Parse", new Type[] { typeof(string) });
            if (m == null) throw new ArgumentNullException("getmethod", "Parse(string)方法不存在，无法转换数据");

            var o = m.Invoke(null, new object[] { value });
            return o;
        }

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="t">当前数据</param>
        /// <param name="records">条目数</param>
        /// <returns></returns>
        protected ApiResult<object> ResultPaging(object t, int records)
        {
            return ResultOk<object>(new { Data = t, TotalRecords = records });
        }

        protected ApiResult<PagingModel<T>> ResultPagingEx<T>(List<T> t, int records)
        {
            PagingModel<T> m = new PagingModel<T>();
            m.Data = t;
            m.TotalRecords = records;

            return ResultOk<PagingModel<T>>(m);
        }

        protected void GetXY(double[] coords, out double x1, out double y1, out double x2, out double y2)
        {
            var xs = coords.Where((t, index) => index % 2 == 0);
            var ys = coords.Where((t, index) => index % 2 != 0);

            x1 = xs.Min();
            x2 = xs.Max();
            y1 = ys.Min();
            y2 = ys.Max();
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }
    }

    [System.Runtime.Serialization.DataContract(Name = "PagingModel")]
    public class PagingModel<T>
    {

        [System.Runtime.Serialization.DataMember(Name = "Data")]
        public List<T> Data { get; set; }


        [System.Runtime.Serialization.DataMember(Name = "TotalRecords")]
        public int TotalRecords { get; set; }
    }

    public class PopulationKX:PagingModel<Model.PopulationBasicInfo>
    {

        [System.Runtime.Serialization.DataMember(Name = "CZCount")]
        public int CZCount { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "ZZCount")]
        public int ZZCount { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "JWCount")]
        public int JWCount { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "ZDCount")]
        public int ZDCount { get; set; }
    }

    public static class LinqEx_Help
    {

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
}
