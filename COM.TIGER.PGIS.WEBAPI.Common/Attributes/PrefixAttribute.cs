using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Common.Attributes
{
    /// <summary>
    /// 前缀信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrefixAttribute : Attribute
    {
        private string _name = string.Empty;

        /// <summary>
        /// 前缀值
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public PrefixAttribute() { }

        public PrefixAttribute(string name) 
        {
            _name = name;
        }
    }
}
