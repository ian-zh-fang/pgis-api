using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    public class CountBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Records { get; set; }
    }

    /// <summary>
    /// 统计数据
    /// </summary>
    public class CountPopulation : CountBase
    {
        public int LiveTypeID { get; set; }
        public string LiveType { get; set; }
    }

    /// <summary>
    /// 单位数据统计
    /// </summary>
    public class CountCompany : CountBase
    {

    }

    /// <summary>
    /// 酒店，宾馆，旅店数据统计
    /// </summary>
    public class CountHotel : CountBase
    {

    }

    /// <summary>
    /// 案件数据统计
    /// </summary>
    public class CountCase : CountBase
    {
        /// <summary>
        /// 案件模型。1-标识一键报警；2-标识三台合一报警
        /// </summary>
        public int Mod { get; set; }
    }

    /// <summary>
    /// 监控数据统计
    /// </summary>
    public class CountMonitor : CountBase
    {

    }
}
