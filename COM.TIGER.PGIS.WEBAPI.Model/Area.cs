using System;
using System.Collections.Generic;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 辖区信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "area", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Area : MBase, IComparable<Area>
    {
        private int? _pid = 0;//上级辖区标识
        private decimal _range = 0.0m;//辖区面积
        private int _companyTypeCode = 0;//数据归属单位类别代码
        private List<Area> _childs = null;//区内辖区信息
        private List<AreaRange> _ranges = new List<AreaRange>();//区域范围信息

        /// <summary>
        /// 辖区标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 上级辖区标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "PID")]
        public int? PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        /// <summary>
        /// 辖区名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 新代码
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "NewCode")]
        public string NewCode { get; set; }

        /// <summary>
        /// 旧代码
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "OldCode")]
        public string OldCode { get; set; }

        /// <summary>
        /// 数据归属单位类型代码
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "BelongToID")]
        public int BelongToID
        {
            get { return _companyTypeCode; }
            set { _companyTypeCode = value; }
        }

        /// <summary>
        /// 数据归属单位类型
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "CompanyType")]
        public BelongTo CompanyType { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// 区域范围信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Ranges")]
        public AreaRange[] Ranges 
        {
            get 
            {
                var arr = new AreaRange[_ranges.Count];
                _ranges.CopyTo(arr);
                return arr;
            }
        }

        /// <summary>
        /// 区内辖区
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ChildAreas")]
        public Area[] ChildAreas
        {
            get 
            {
                _childs = _childs ?? new List<Area>();
                var arr = new Area[_childs.Count];
                _childs.CopyTo(arr);
                return arr;
            }
        }

        /// <summary>
        /// 添加辖区到子辖区项尾部,返回添加的索引值
        /// <para>如果已经存在辖区信息,返回辖区的索引值</para>
        /// </summary>
        /// <param name="a">需要添加的辖区信息</param>
        /// <returns></returns>
        public int Add(Area a)
        {
            _childs = _childs ?? new List<Area>();
            var index = _childs.FindIndex(t => t.ID == a.ID);
            if (index < 0)
            {
                _childs.Add(a);
                return _childs.Count;
            }
            return index;
        }

        /// <summary>
        /// 批量添加辖区信息到子辖区尾部
        /// <para>该方法会检验辖区的ID,并保存不存在的ID对应的辖区值到子辖区中</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(params Area[] items)
        {
            if (items.Length == 0) return;
            _childs = _childs ?? new List<Area>();
            var its = items.Where(t => !_childs.Exists(x => x.ID == t.ID));
            _childs.AddRange(its);
        }

        /// <summary>
        /// 批量添加辖区信息到子辖区尾部
        /// <para>该方法会检验辖区是否是当前辖区的子辖区，并保存子辖区中ID不存在的辖区信息</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(List<Area> items)
        {
            if (items == null) return;
            _childs = _childs ?? new List<Area>();
            var its = items.Where(t => !(_childs.Exists(x => x.ID == t.ID)) && (t.PID == this.ID)).ToList();
            var count = its.Count;
            for (var i = 0; i < count; i++)
            {
                var it = its[i];
                it.AddRange(items);
                _childs.Add(it);
            }
        }

        /// <summary>
        /// 添加区域范围信息
        /// </summary>
        /// <param name="items"></param>
        public void AddRanges(params AreaRange[] items)
        {
            if (items.Length > 0)
            {
                _ranges.AddRange(items);
            }
        }

        /// <summary>
        /// 排序比较
        /// <para>默认按照主键标识大小比较</para>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Area other)
        {
            if (ID > other.ID) return 1;
            if (ID < other.ID) return -1;
            return 0;
        }
    }
}
