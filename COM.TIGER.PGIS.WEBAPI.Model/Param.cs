using System;
using System.Collections.Generic;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "params", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Param : MBase, IComparable<Param>
    {
        private int? _pid = 0;
        private List<Param> _params = null;

        /// <summary>
        /// 参数项ID
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name="ID")]
        public int ID { get; set; }

        /// <summary>
        /// 父级参数项配置
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name="PID")]
        public int? PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        /// <summary>
        /// 参数项名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name="Name")]
        public string Name { get; set; }

        /// <summary>
        /// 唯一编码
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// 参数启用项
        /// <para>标识参数项启用项,TRUE表示启用当前参数项</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name="Disabled")]
        public int Disabled { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 子参数项信息
        /// <para>获取子参数信息的浅拷贝信息</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name="ChildParams")]
        public Param[] ChildParams
        {
            get 
            {
                //防止外部修改参数，此处返回对象的拷贝信息
                _params = _params ?? new List<Param>();
                var arr = new Param[_params.Count];
                _params.CopyTo(arr);
                return arr;
            }
        }

        /// <summary>
        /// 添加参数项到子参数项
        /// <para>成功，返回参数项在子参数项中的索引</para>
        /// <para>失败，返回-1</para>
        /// </summary>
        /// <param name="p">子参数项</param>
        /// <returns></returns>
        public int Add(Param p)
        {
            _params = _params ?? new List<Param>();
            if (!_params.Exists(t => t.ID == p.ID))
            {
                _params.Add(p);
                return (_params.Count - 1);
            }
            return -1;
        }

        /// <summary>
        /// 批量添加参数项到子参数项中
        /// <para>该方法会检验参数项的ID，并保存ID不存在的参数项信息</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(params Param[] items)
        {
            if (items.Length == 0) return;
            _params = _params ?? new List<Param>();
            //查找没有保存的数据，并添加到子参数项中
            var its = items.Where(t => !(_params.Exists(x => x.ID == t.ID)));
            _params.AddRange(its);
        }

        /// <summary>
        /// 批量添加参数项到子参数项中
        /// <para>该方法会检验参数项是否是当前参数项的子参数项，并保存子参数项中ID不存在的参数项</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(List<Param> items)
        {
            if (items == null) return;
            _params = _params ?? new List<Param>();
            var its = items.Where(t => !(_params.Exists(x => x.ID == t.ID)) && (t.PID == ID)).ToList();
            var c = its.Count;
            for (var i = 0; i < c; i++)
            {
                var it = its[i];
                //递归获取子参数项信息
                it.AddRange(items);
                _params.Add(it);
            }
        }

        /// <summary>
        /// 用于排序比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Param other)
        {
            if (other.ID > this.ID) return -1;
            if (other.ID == this.ID) return 0;
            return 1;
        }
    }
}
