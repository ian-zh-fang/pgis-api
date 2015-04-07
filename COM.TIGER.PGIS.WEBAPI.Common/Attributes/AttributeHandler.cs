using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Common.Attributes
{
    public static class AttributeHandler
    {
        /// <summary>
        /// 获取前缀
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPrefixName(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(PrefixAttribute), true);
            if (attr.Length == 0) return string.Empty;
            return ((PrefixAttribute)attr[0]).Name;
        }

        /// <summary>
        /// 获取后缀
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSuffixName(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(SuffixAttribute), true);
            if (attr.Length == 0) return string.Empty;
            return ((SuffixAttribute)attr[0]).Name;
        }
    }
}
