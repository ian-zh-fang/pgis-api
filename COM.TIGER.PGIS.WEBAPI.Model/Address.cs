
/*****************************************************
*   Author: Ian.Fun
*   File: Address.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// 
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "MAddress", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Address : MBase
    {
        private int _ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Content;
        ///<summary>
        /// 地址正文
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Content")]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private int _AdminID;
        ///<summary>
        /// 行政区划ID
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AdminID")]
        public int AdminID
        {
            get { return _AdminID; }
            set { _AdminID = value; }
        }

        private int _OwnerInfoID;
        ///<summary>
        /// 楼房ID
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OwnerInfoID")]
        public int OwnerInfoID
        {
            get { return _OwnerInfoID; }
            set { _OwnerInfoID = value; }
        }

        private int _StreetID;
        ///<summary>
        /// 街巷ID
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetID")]
        public int StreetID
        {
            get { return _StreetID; }
            set { _StreetID = value; }
        }

        private int _NumID;
        ///<summary>
        /// 街巷号ID
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "NumID")]
        public int NumID
        {
            get { return _NumID; }
            set { _NumID = value; }
        }

        private int _UnitID;
        ///<summary>
        /// 房间隶属楼房单元ID
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "UnitID")]
        public int UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }

        private int _RoomID;
        ///<summary>
        /// 房间ID
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomID")]
        public int RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }

        private Administrative _admin;
        /// <summary>
        /// 新政区划
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Administrative")]
        public Administrative Administrative
        {
            get { return _admin; }
            set { _admin = value; }
        }

        private OwnerInfo _ownerinfo;
        /// <summary>
        /// 楼房
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "OwnerInfo")]
        public OwnerInfo OwnerInfo
        {
            get { return _ownerinfo; }
            set { _ownerinfo = value; }
        }

        private Street _street;
        /// <summary>
        /// 街道
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Street")]
        public Street Street
        {
            get { return _street; }
            set { _street = value; }
        }

        private StreetNum _streetnum;
        /// <summary>
        /// 门牌号
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetNum")]
        public StreetNum StreetNum
        {
            get { return _streetnum; }
            set { _streetnum = value; }
        }

        private Unit _unit;
        /// <summary>
        /// 楼房单元
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Unit")]
        public Unit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private Rooms _room;
        /// <summary>
        /// 房间
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Room")]
        public Rooms Room
        {
            get { return _room; }
            set { _room = value; }
        }
    }
}

