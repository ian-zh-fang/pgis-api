using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "Maptheme", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Maptheme
    {
        /// <summary>
        /// 大楼 标识符
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_MOI_ID")]
        public int MEH_MOI_ID { get; set; }

        /// <summary>
        /// 重心坐标 横坐标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_CenterX")]
        public string MEH_CenterX { get; set; }

        /// <summary>
        /// 中心坐标 纵坐标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_CenterY")]
        public string MEH_CenterY { get; set; }

        /// <summary>
        /// 总 记录数
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Record")]
        public int Record { get; set; }
    }
}
