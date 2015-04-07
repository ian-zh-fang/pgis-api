using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Common.Attributes
{
    /// <summary>
    /// 后缀信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Interface| AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class SuffixAttribute : Attribute
    {
        private string _name = string.Empty;

        /// <summary>
        /// 后缀值
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public SuffixAttribute() { }

        public SuffixAttribute(string name) 
        {
            _name = name;
        }
    }
}
