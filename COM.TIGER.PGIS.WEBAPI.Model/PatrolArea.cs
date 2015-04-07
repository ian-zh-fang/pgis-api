using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 巡防区域信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "PatrolArea", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class PatrolArea:MBase, IComparable<PatrolArea>
    {
        private int _id;
        private string _name;
        private string _manager;
        private string _phone;
        private string _remark;
        private string _coordinates;
        private double _centerx;
        private double _centery;
        private string _color;
       
        /// <summary>
        /// 主键
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 巡防区域名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 巡防区域管理员
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Manager")]
        public string Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Phone")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        /// <summary>
        /// 巡防区域备注
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Remark")]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 坐标串
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Coordinates")]
        public string Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }
        /// <summary>
        /// X坐标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Centerx")]
        public double Centerx
        {
            get { return _centerx; }
            set { _centerx = value; }
        }
        /// <summary>
        /// Y坐标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Centery")]
        public double Centery
        {
            get { return _centery; }
            set { _centery = value; }
        }
        /// <summary>
        /// 颜色
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Color")]
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public int CompareTo(PatrolArea other)
        {
            if (Id > other.Id) return 1;
            if (Id < other.Id) return -1;
            return 0;
        }
    }
}
