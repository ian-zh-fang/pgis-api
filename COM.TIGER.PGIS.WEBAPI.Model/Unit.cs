
/*****************************************************
*   Author: Ian.Fun
*   File: Unit.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;
using System.Linq;
using System.Collections.Generic;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// 
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "Unit", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Unit : MBase, IComparable<Unit>
    {
        private int _Unit_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Unit_ID")]
        public int Unit_ID
        {
            get { return _Unit_ID; }
            set { _Unit_ID = value; }
        }

        private string _UnitName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "UnitName")]
        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }

        private int _OwnerInfoID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OwnerInfoID")]
        public int OwnerInfoID
        {
            get { return _OwnerInfoID; }
            set { _OwnerInfoID = value; }
        }

        private int _Sort;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Sort")]
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }

        private List<Model.Rooms> _rooms = new List<Rooms>();
        /// <summary>
        /// 当前单元内房间信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Rooms")]
        public Model.Rooms[] Rooms
        {
            get { return _rooms.ToArray(); }
            set { Add(value); }
        }

        /// <summary>
        /// 批量添加单元房间信息，如果含有不存在房间信息
        /// </summary>
        /// <param name="rooms"></param>
        public void Add(params Model.Rooms[] rooms)
        {
            _rooms.AddRange(rooms.Where(t => !_rooms.Exists(x => t.Room_ID == x.Room_ID)));
        }

        public int CompareTo(Unit other)
        {
            if (_Sort > other.Sort) return 1;
            if (_Sort < other.Sort) return -1;
            return 0;
        }
    }

    [System.Runtime.Serialization.DataContract(Name = "unitex", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class UnitEx : Unit
    {

        private string _address;
        [System.Runtime.Serialization.DataMember(Name = "Address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }
}

