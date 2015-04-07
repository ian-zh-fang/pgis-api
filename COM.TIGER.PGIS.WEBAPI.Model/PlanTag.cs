using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 预案在地图上的标注信息，
    /// <para>每一个预案都是有多个标注信息组成的</para>
    /// </summary>
    public class PlanTag:MBase
    {
        /// <summary>
        /// 标识符
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 标注标识
        /// </summary>
        public int TagID { get; set; }

        /// <summary>
        /// 预案标识
        /// </summary>
        public int PlanID { get; set; }
    }
}
