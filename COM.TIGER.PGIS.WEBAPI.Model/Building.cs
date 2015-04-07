
/*****************************************************
*   Author: Ian.Fun
*   File: Building.cs
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
    [System.Runtime.Serialization.DataContract(Name = "Building", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Building : MBase
    {
        private int _Building_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Building_ID")]
        public int Building_ID
        {
            get { return _Building_ID; }
            set { _Building_ID = value; }
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
        private int _FloorsCount;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "FloorsCount")]
        public int FloorsCount
        {
            get { return _FloorsCount; }
            set { _FloorsCount = value; }
        }
        private int _RoomsCount;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomsCount")]
        public int RoomsCount
        {
            get { return _RoomsCount; }
            set { _RoomsCount = value; }
        }
        private int _AdminID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AdminID")]
        public int AdminID
        {
            get { return _AdminID; }
            set { _AdminID = value; }
        }
        private string _AdminName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AdminName")]
        public string AdminName
        {
            get { return _AdminName; }
            set { _AdminName = value; }
        }
        private int _StreetID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetID")]
        public int StreetID
        {
            get { return _StreetID; }
            set { _StreetID = value; }
        }
        private int _StreetNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetNum")]
        public int StreetNum
        {
            get { return _StreetNum; }
            set { _StreetNum = value; }
        }
        private string _Address;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Address")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private int _RoomStructureID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomStructureID")]
        public int RoomStructureID
        {
            get { return _RoomStructureID; }
            set { _RoomStructureID = value; }
        }
        private string _RoomStructure;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomStructure")]
        public string RoomStructure
        {
            get { return _RoomStructure; }
            set { _RoomStructure = value; }
        }
    }
}

