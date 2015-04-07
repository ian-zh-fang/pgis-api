using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 系统数据处理程序
    /// <para>包含用户信息检验</para>
    /// </summary>
    public class SysHandler : DBase
    {
        private static SysHandler _handler = null;
        /// <summary>
        /// 系统数据处理程序
        /// </summary>
        public static SysHandler Handler
        {
            get { return (_handler = _handler ?? new SysHandler()); }
        }
        
        private SysHandler() { }
    }
}
