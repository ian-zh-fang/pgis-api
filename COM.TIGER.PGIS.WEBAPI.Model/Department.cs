using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "Department", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Department : MBase, IComparable<Department>
    {
        private List<Department> _childs = new List<Department>();

        /// <summary>
        /// 主键标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 上一级部门机构标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "PID")]
        public int? PID { get; set; }

        /// <summary>
        /// 部门机构代码
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// 部门机构名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// 下级部门信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ChildDepartments")]
        public Department[] ChildDepartments
        {
            get
            {
                var arr = new Department[_childs.Count];
                _childs.CopyTo(arr);
                return arr;
            }
        }

        /// <summary>
        /// 添加部门到子部门
        /// <para>返回参数项在子参数项中的索引</para>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public int Add(Department d)
        {
            var index = _childs.FindIndex(t => t.ID == d.ID);
            if (index < 0)
            {
                _childs.Add(d);
                index = _childs.Count;
            }
            return index;
        }

        /// <summary>
        /// 批量添加子部门
        /// <para>该方法会保存子部门中不存在的部门信息</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(params Department[] items)
        {
            if (items.Length == 0) return;
            var its = items.Where(t => !_childs.Exists(x => x.ID == t.ID));
            _childs.AddRange(its);
        }

        /// <summary>
        /// 批量添加子部门
        /// </summary>
        /// <param name="items">该方法首先检验部门是否是当前部门的子部门信息,然后保存子部门中不存在的部门信息</param>
        public void AddRange(List<Department> items)
        {
            if (items == null) return;
            var its = items.Where(t => !(_childs.Exists(x => x.ID == t.ID)) && (t.PID == ID)).ToList();
            var c = its.Count;
            for (var i = 0; i < c; i++)
            {
                var it = its[i];
                it.AddRange(items);
                _childs.Add(it);
            }
        }

        /// <summary>
        /// 默认排序方式
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Department other)
        {
            if (this.ID > other.ID) return 1;
            if (this.ID < other.ID) return -1;
            return 0;
        }
    }
}
