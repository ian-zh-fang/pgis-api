
/*****************************************************
*   Author: Ian.Fun
*   File: Street.cs
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
    [System.Runtime.Serialization.DataContract(Name = "Street", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Street :MBase
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
        private string _Alias;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Alias")]
        public string Alias
        {
            get{ return _Alias;}
            set{ _Alias = value;}
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
        private int _PositionID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PositionID")]
        public int PositionID
        {
            get{ return _PositionID;}
            set{ _PositionID = value;}
        }
        private string _Position;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Position")]
        public string Position
        {
            get{ return _Position;}
            set{ _Position = value;}
        }
        private int _LeftNumTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LeftNumTypeID")]
        public int LeftNumTypeID
        {
            get{ return _LeftNumTypeID;}
            set{ _LeftNumTypeID = value;}
        }
        private string _LeftNumTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LeftNumTypeName")]
        public string LeftNumTypeName
        {
            get{ return _LeftNumTypeName;}
            set{ _LeftNumTypeName = value;}
        }
        private int _LeftStartNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LeftStartNum")]
        public int LeftStartNum
        {
            get{ return _LeftStartNum;}
            set{ _LeftStartNum = value;}
        }
        private int _LeftEndNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LeftEndNum")]
        public int LeftEndNum
        {
            get{ return _LeftEndNum;}
            set{ _LeftEndNum = value;}
        }
        private int _RightStartNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RightStartNum")]
        public int RightStartNum
        {
            get{ return _RightStartNum;}
            set{ _RightStartNum = value;}
        }
        private int _RightEndNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RightEndNum")]
        public int RightEndNum
        {
            get{ return _RightEndNum;}
            set{ _RightEndNum = value;}
        }
        private double _X;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "X")]
        public double X
        {
            get{ return _X;}
            set{ _X = value;}
        }
        private double _Y;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Y")]
        public double Y
        {
            get{ return _Y;}
            set{ _Y = value;}
        }
        private decimal _Lng;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Lng")]
        public decimal Lng
        {
            get{ return _Lng;}
            set{ _Lng = value;}
        }
        private decimal _Lat;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Lat")]
        public decimal Lat
        {
            get{ return _Lat;}
            set{ _Lat = value;}
        }
        private int _GisGrid;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "GisGrid")]
        public int GisGrid
        {
            get{ return _GisGrid;}
            set{ _GisGrid = value;}
        }
        private int _AdminID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AdminID")]
        public int AdminID
        {
            get{ return _AdminID;}
            set{ _AdminID = value;}
        }
        private string _AdminName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AdminName")]
        public string AdminName
        {
            get{ return _AdminName;}
            set{ _AdminName = value;}
        }
        private int _LivingState;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LivingState")]
        public int LivingState
        {
            get{ return _LivingState;}
            set{ _LivingState = value;}
        }
        private DateTime _EndTime;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "EndTime")]
        public DateTime EndTime
        {
            get{ return _EndTime;}
            set{ _EndTime = value;}
        }
        private string _SourceFrom;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "SourceFrom")]
        public string SourceFrom
        {
            get{ return _SourceFrom;}
            set{ _SourceFrom = value;}
        }

        private System.Collections.Generic.List<Model.StreetNum> _items = new System.Collections.Generic.List<StreetNum>();
        /// <summary>
        /// 当前街巷的所有门牌号信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Numbers")]
        public Model.StreetNum[] Numbers
        {
            get
            {
                var arr = new Model.StreetNum[_items.Count];
                _items.CopyTo(arr);
                return arr;
            }
        }

        /// <summary>
        /// 添加街巷门牌号信息，如果含有不存在的门牌号
        /// </summary>
        /// <param name="items">门牌号信息</param>
        public void Add(params Model.StreetNum[] items)
        {
            _items.AddRange(items.Where(t => !_items.Exists(x => x.ID == t.ID)));
        }
    }
}

