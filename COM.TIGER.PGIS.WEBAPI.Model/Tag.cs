using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 预案标注信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "tag", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Tag:MBase,IComparable<Tag>
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
        /// 地图标注类型。
        /// <para>1表示点，2表示线，3表示面</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Type")]
        public int Type { get; set; }

        /// <summary>
        /// 地图标注描述信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Description")]
        public string Description { get; set; }

        public int CompareTo(Tag other)
        {
            if (ID > other.ID) return 1;
            if (ID == other.ID) return 0;
            return -1;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Tag;
            if (other == null) return false;
            return ID == other.ID
                && Name == other.Name
                && Coordinates == other.Coordinates
                && X == other.X
                && Y == other.Y
                && Color == other.Color
                && IconCls == other.IconCls
                && Type == other.Type
                && Description == other.Description;
        }
    }
}
