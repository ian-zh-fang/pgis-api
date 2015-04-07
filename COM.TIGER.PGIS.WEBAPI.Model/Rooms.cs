
/*****************************************************
*   Author: Ian.Fun
*   File: Rooms.cs
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
    [System.Runtime.Serialization.DataContract(Name = "Rooms", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Rooms : MBase
    {
        private int _Room_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Room_ID")]
        public int Room_ID
        {
            get { return _Room_ID; }
            set { _Room_ID = value; }
        }

        private string _RoomName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomName")]
        public string RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }

        private string _RoomName2;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomName2")]
        public string RoomName2
        {
            get { return _RoomName2; }
            set { _RoomName2 = value; }
        }

        private decimal _RoomArea;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomArea")]
        public decimal RoomArea
        {
            get { return _RoomArea; }
            set { _RoomArea = value; }
        }

        private int _RoomUseID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomUseID")]
        public int RoomUseID
        {
            get { return _RoomUseID; }
            set { _RoomUseID = value; }
        }

        private string _RoomUse;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomUse")]
        public string RoomUse
        {
            get { return _RoomUse; }
            set { _RoomUse = value; }
        }

        private int _RoomAttributeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomAttributeID")]
        public int RoomAttributeID
        {
            get { return _RoomAttributeID; }
            set { _RoomAttributeID = value; }
        }

        private string _RoomAttribute;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomAttribute")]
        public string RoomAttribute
        {
            get { return _RoomAttribute; }
            set { _RoomAttribute = value; }
        }

        private int _UnitID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "UnitID")]
        public int UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
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

        /// <summary>
        /// 建筑信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "BuildingInfo")]        
        public Model.Building BuildingInfo { get; set; }
    }
}

