using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 预案文件数据
    /// </summary>
    public class PlanFile:MBase
    {
        /// <summary>
        /// 标识符
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 文档标识
        /// </summary>
        public int FileID { get; set; }

        /// <summary>
        /// 预案标识
        /// </summary>
        public int PlanID { get; set; }
    }
}
