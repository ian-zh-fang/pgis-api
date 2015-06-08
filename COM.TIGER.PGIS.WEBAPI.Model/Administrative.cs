
/*****************************************************
*   Author: Ian.Fun
*   File: Administrative.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// 
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "Administrative", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Administrative :MBase, IComparable<Administrative>
    {
        private int _ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID
        {
            get{ return _ID;}
            set{ _ID = value;}
        }
        private string _Name;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name
        {
            get{ return _Name;}
            set{ _Name = value;}
        }
        private string _Code;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Code")]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        private int? _PID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PID")]
        public int? PID
        {
            get{ return _PID;}
            set{ _PID = value;}
        }
        private string _FirstLetter;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "FirstLetter")]
        public string FirstLetter
        {
            get{ return _FirstLetter;}
            set{ _FirstLetter = value;}
        }
        private int _AreaID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AreaID")]
        public int AreaID
        {
            get{ return _AreaID;}
            set{ _AreaID = value;}
        }
        private string _AreaName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AreaName")]
        public string AreaName
        {
            get{ return _AreaName;}
            set{ _AreaName = value;}
        }

        /// <summary>
        /// 上一级行政区划
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Parent")]
        public Administrative Parent { get; set; }

        /// <summary>
        /// 当前新政区划隶属辖区信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Area")]
        public Area Area { get; set; }

        /// <summary>
        /// 行政区划全称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "FullName")]
        public string FullName
        {
            get
            {
                if (Parent == null) return Name;
                return string.Format("{0},{1}", Parent.FullName, Name);
            }
            private set { }
        }
        
        private System.Collections.Generic.List<Administrative> _items = new System.Collections.Generic.List<Administrative>();
        /// <summary>
        /// 下一级行政区划信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Items")]
        public Administrative[] Items
        {
            get
            {
                var arr = new Administrative[_items.Count];
                _items.CopyTo(arr);
                return arr;
            }
        }
        
        private System.Collections.Generic.List<Model.Street> _streets = new System.Collections.Generic.List<Street>();
        /// <summary>
        /// 当前行政区划中的街巷信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Streets")]
        public Model.Street[] Streets
        {
            get { return _streets.ToArray(); }
        }

        /// <summary>
        /// 获取指定索引位置的下一级行政区划信息
        /// </summary>
        /// <param name="index">相对位置0处的偏移量</param>
        /// <returns></returns>
        public Administrative this[int index]
        {
            get { return _items[index]; }
        }

        /// <summary>
        /// 获取指定名称的下一级行政区划信息
        /// </summary>
        /// <param name="name">行政区划名称</param>
        /// <returns></returns>
        public Administrative this[string name]
        {
            get { return _items.FirstOrDefault(t => t._Name == name); }
        }

        /// <summary>
        /// 添加下一级行政区划信息，如果当前行政区划不存在
        /// </summary>
        /// <param name="e"></param>
        public void Add(Administrative e)
        {
            if (_items.FirstOrDefault(t => t.ID == e.ID) == null)
                _items.Add(e);
        }

        /// <summary>
        /// 批量添加下一级行政区划信息，如果含有不存在的行政区划信息
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(System.Collections.Generic.List<Model.Administrative> items)
        {
            var arr = items.Where(t => _items.Exists(x => t.ID == x.ID) == false && t.PID == _ID);
            foreach (var item in arr)
            {
                item.AddRange(items);
                _items.Add(item);
            }
        }

        /// <summary>
        /// 添加街巷信息，如果当含有不存在的街巷信息
        /// </summary>
        /// <param name="streets"></param>
        public void Add(params Model.Street[] streets)
        {
            _streets.AddRange(streets.Where(t => !_streets.Exists(x => t.ID == x.ID)));
        }

        /// <summary>
        /// 匹配排序函数
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Administrative other)
        {
            if (_ID > other.ID) return 1;
            if (_ID < other.ID) return -1;
            return 0;
        }
    }
}

