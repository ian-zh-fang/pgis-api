
/*****************************************************
*   Author: Ian.Fun
*   File: Hotel.cs
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
    [System.Runtime.Serialization.DataContract(Name = "Hotel", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Hotel :MBase
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
        private int _AddressID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AddressID")]
        public int AddressID
        {
            get{ return _AddressID;}
            set{ _AddressID = value;}
        }
        private string _Tel;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Tel")]
        public string Tel
        {
            get{ return _Tel;}
            set{ _Tel = value;}
        }
        private string _SafetyTel;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "SafetyTel")]
        public string SafetyTel
        {
            get{ return _SafetyTel;}
            set{ _SafetyTel = value;}
        }
        private string _Corporation;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Corporation")]
        public string Corporation
        {
            get{ return _Corporation;}
            set{ _Corporation = value;}
        }
        private string _Official;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Official")]
        public string Official
        {
            get{ return _Official;}
            set{ _Official = value;}
        }
        private string _PoliceOffcial;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PoliceOffcial")]
        public string PoliceOffcial
        {
            get{ return _PoliceOffcial;}
            set{ _PoliceOffcial = value;}
        }
        private int _RoomCount;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomCount")]
        public int RoomCount
        {
            get{ return _RoomCount;}
            set{ _RoomCount = value;}
        }
        private int _BedCount;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "BedCount")]
        public int BedCount
        {
            get{ return _BedCount;}
            set{ _BedCount = value;}
        }
        private int _StarLevel;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StarLevel")]
        public int StarLevel
        {
            get{ return _StarLevel;}
            set{ _StarLevel = value;}
        }
        private int _Disable;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Disable")]
        public int Disable
        {
            get{ return _Disable;}
            set{ _Disable = value;}
        }

        /// <summary>
        /// œÍœ∏µÿ÷∑–≈œ¢ 
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Address")]
        public Model.Address Address { get; set; }
    }
}

