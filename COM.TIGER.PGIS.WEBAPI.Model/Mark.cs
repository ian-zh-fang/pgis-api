using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 地图标注信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "mark", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Mark : MBase, IComparable<Mark>
    {
        /// <summary>
        /// 标识符
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 标注的名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 只有在标注为线和面时，才能起作用，标识标注在地图上的一系列坐标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Coordinates")]
        public string Coordinates { get; set; }

        /// <summary>
        /// 标识标注的中心点，以矩形方式计算宽高，如果标注为一个点，那么表示该点的横坐标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "X")]
        public double X { get; set; }

        /// <summary>
        /// 标识标注的中心点，以矩形方式计算宽高，如果标注为一个点，那么表示该点的纵坐标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Y")]
        public double Y { get; set; }

        /// <summary>
        /// 只有在标注为线和面时，才能起作用，标识标注的颜色
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Color")]
        public string Color { get; set; }

        /// <summary>
        /// 当前标注为点时有效，标识当前标注显示在地图上的图标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "IconCls")]
        public string IconCls { get; set; }

        /// <summary>
        /// 地图标注描述信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Description")]
        public string Description { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "MarkTypeID")]
        public int MarkTypeID { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "MarkType")]
        public MarkType MarkType { get; set; }

        public int CompareTo(Mark other)
        {
            if (ID > other.ID) return 1;
            if (ID < other.ID) return -1;
            return 0;
        }
    }
}
