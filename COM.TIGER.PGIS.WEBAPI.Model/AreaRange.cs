using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "arearange", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class AreaRange:MBase
    {
        /// <summary>
        /// 范围标识符
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 当前范围范围座标组
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Range")]
        public string Range { get; set; }

        /// <summary>
        /// 当前范围中心原点座标横坐标值
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "X")]
        public double X { get; set; }

        /// <summary>
        /// 当前范围中心原点座标横坐标值
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Y")]
        public double Y { get; set; }

        /// <summary>
        /// 当前范围在地图中显示的颜色
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Color")]
        public string Color { get; set; }

        /// <summary>
        /// 当前范围隶属于哪一个区域
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "AreaID")]
        public int AreaID { get; set; }
    }
}
