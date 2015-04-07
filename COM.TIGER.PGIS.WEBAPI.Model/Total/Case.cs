using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 案件统计模型
    /// </summary>
    public class Case
    {
        /// <summary>
        /// 种类标识
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 种类名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 天计数
        /// </summary>
        public int TodayTickCount { get; set; }

        /// <summary>
        /// 周计数
        /// </summary>
        public int ThisWeekTickCount { get; set; }

        /// <summary>
        /// 月度计数
        /// </summary>
        public int ThisMonthTickCount { get; set; }

        /// <summary>
        /// 年度计数
        /// </summary>
        public int ThisYearTickCount { get; set; }
    }
}
