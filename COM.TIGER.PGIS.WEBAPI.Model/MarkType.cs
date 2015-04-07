using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "MarkType", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class MarkType:MBase, IComparable<MarkType>
    {
        /// <summary>
        /// 类型标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "IconCls")]
        public string IconCls { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Color")]
        public string Color { get; set; }

        /// <summary>
        /// 类型
        /// <para>1：标识单点</para>
        /// <para>2：标识线条</para>
        /// <para>3：标识区域</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Type")]
        public int Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Remark")]
        public string Remark { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Sort")]
        public int Sort { get; set; }

        private List<Model.Mark> _marks = new List<Mark>();
        /// <summary>
        /// 当前类型下的所有标注信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Marks")]
        public Model.Mark[] Marks
        {
            get
            {
                var arr = new Model.Mark[_marks.Count];
                _marks.CopyTo(arr);
                return arr;
            }
        }

        /// <summary>
        /// 批量添加标注
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(List<Mark> items)
        {
            var temp = items.Where(t =>
            {
                if (!_marks.Exists(x => t.ID == x.ID) && t.MarkTypeID == this.ID)
                {
                    return true;
                }
                return false;
            });
            _marks.AddRange(temp);
        }

        public int CompareTo(MarkType other)
        {
            if (other.Sort > Sort) return -1;
            if (other.Sort < Sort) return 1;
            return 0;
        }
    }
}
