using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COM.TIGER.PGIS.WEBAPI
{
    /// <summary>
    /// 描述API执行结果
    /// </summary>
    /// <typeparam name="T">操作执行结果，必须支持序列化</typeparam>
    [System.Runtime.Serialization.DataContract(Name="ApiResult", Namespace="http://www.tigerhz.com/web/api/apiresult")]
    public sealed class ApiResult<T>
    {
        private string _message = "Init exception";
        private ResultStatus _status = ResultStatus.Exception;
        private T _result = default(T);

        /// <summary>
        /// API执行结果类型描述信息
        /// <para>描述COM.TIGER.PGIS.WEBAPI.ResultStatus枚举值对应的详细信息</para>
        /// <para>如果Status的值为COM.TIGER.PGIS.WEBAPI.ResultStatus.Message，那么该值保存执行的结果值</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Message")]
        public string Message 
        { 
            get { return _message; }
            set { this._message = value; }
        }

        /// <summary>
        /// （枚举值）API执行结果类型
        /// <para>COM.TIGER.PGIS.WEBAPI.ResultStatus枚举值之一</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Status")]
        public ResultStatus Status 
        { 
            get { return _status; }
            set { this._status = value; }
        }

        /// <summary>
        /// API执行结果
        /// <para>如果API执行成功，结果值正确，该值为执行成功返回的结果</para>
        /// <para>如果API执行成功，结果值正确， 并且类型为System.String，该值为null</para>
        /// <para>如果API执行成功，结果错误，该值为结果值类型的默认值</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Result")]
        public T Result 
        { 
            get { return _result; }
            set { this._result = value; }
        }

        /// <summary>
        /// 实例化一个APIResult的新实例
        /// </summary>
        /// <param name="message">API执行结果类型描述信息</param>
        /// <param name="status">API执行结果类型</param>
        /// <param name="result">API执行结果</param>
        /// <returns>APIResult的新实例</returns>
        public static ApiResult<T> Instance(string message, ResultStatus status, T result)
        {
            var ret = new ApiResult<T>();
            ret._message = message;
            ret._result= result;
            ret._status = status;
            return ret;
        }
    }

    /// <summary>
    /// （枚举值）API执行结果类型
    /// </summary>
    public enum ResultStatus : int
    {
        /// <summary>
        /// API执行成功，并得到错误的结果
        /// </summary>
        Failed = -1,
        /// <summary>
        /// API执行失败
        /// </summary>
        Exception = 0,
        /// <summary>
        /// API执行成功，并得到正确的结果
        /// </summary>
        OK = 200,
        /// <summary>
        /// API执行成功，并得到的正确的结果是System.String类型的值
        /// </summary>
        Message,
    }
}