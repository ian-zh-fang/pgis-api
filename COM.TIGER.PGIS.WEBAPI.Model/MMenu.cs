using System;
using System.Collections.Generic;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 菜单
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "menu", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Menu : MBase, IComparable<Menu>
    {
        private int _menu_id;

        /// <summary>
        /// 菜单id
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Id")]
        public int ID
        {
            get { return _menu_id; }
            set { _menu_id = value; }
        }
        private int? _menu_pid = 0;

        /// <summary>
        /// 父级菜单id
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "PId")]
        public int? PID
        {
            get { return _menu_pid; }
            set { _menu_pid = value; }
        }
        private string _menu_text;

        /// <summary>
        /// 标题
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Text")]
        public string Text
        {
            get { return _menu_text; }
            set { _menu_text = value; }
        }
        private string _menu_key;

        /// <summary>
        /// key
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Code")]
        public string Code
        {
            get { return _menu_key; }
            set { _menu_key = value; }
        }
        private int _menu_disabled = 1;

        /// <summary>
        /// 显示标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Disabled")]
        public int Disabled
        {
            get { return _menu_disabled; }
            set { _menu_disabled = value; }
        }
        private int _menu_checked = 0;

        /// <summary>
        /// 复选框
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Checked")]
        public int Checked
        {
            get { return _menu_checked; }
            set { _menu_checked = value; }
        }
        private string _menu_iconcls;

        /// <summary>
        /// 图标
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Iconcls")]
        public string Iconcls
        {
            get { return _menu_iconcls; }
            set { _menu_iconcls = value; }
        }
        private string _menu_hander;

        /// <summary>
        /// javascript处理方法名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Handler")]
        public string Handler
        {
            get { return _menu_hander; }
            set { _menu_hander = value; }
        }
        private string _menu_description;

        /// <summary>
        /// 描述
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Description")]
        public string Description
        {
            get { return _menu_description; }
            set { _menu_description = value; }
        }
        private int _menu_sort = 0;

        /// <summary>
        /// 排序规则
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Sort")]
        public int Sort
        {
            get { return _menu_sort; }
            set { _menu_sort = value; }
        }

        private List<Menu> _childs = new List<Menu>();
        /// <summary>
        /// 子菜单信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ChildMenus")]
        public Menu[] ChildMenus
        {
            get
            {
                _childs = _childs ?? new List<Menu>();
                var arr = new Menu[_childs.Count];
                _childs.CopyTo(arr);
                return arr;
            }
        }

        /// <summary>
        /// 添加子菜单,返回菜单索引值
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int Add(Menu m)
        {
            var index = _childs.FindIndex(t => t.ID == m.ID);
            if (index < 0)
            {
                _childs.Add(m);
                return _childs.Count;
            }
            return index;
        }

        /// <summary>
        /// 批量添加子菜单
        /// <para>该方法会检查子菜单项是否已经存在,保存不存的子菜单项</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(params Menu[] items)
        {
            if (items.Length == 0) return;
            var its = items.Where(t => !_childs.Exists(x => x.ID == t.ID));
            _childs.AddRange(its);
        }

        /// <summary>
        /// 批量添加子菜单
        /// <para>该方法会检查子菜单是否是当前菜单的子菜单,保存不存在的子菜单项</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(List<Menu> items)
        {
            var its = items.Where(t => !(_childs.Exists(x => x.ID == t.ID)) && (t.PID == this.ID)).ToList();
            var c = its.Count;
            for (var i = 0; i < c; i++)
            {
                var it = its[i];
                it.AddRange(items);
                _childs.Add(it);
            }
        }

        public int CompareTo(Menu other)
        {
            if (this.ID > other.ID) return 1;
            if (this.ID < other.ID) return -1;
            return 0;
        }
    }
}
