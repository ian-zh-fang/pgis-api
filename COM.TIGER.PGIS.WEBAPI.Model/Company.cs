
/*****************************************************
*   Author: Ian.Fun
*   File: Company.cs
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
    [System.Runtime.Serialization.DataContract(Name = "Company", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Company : MBase
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
        private int _TypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TypeID")]
        public int TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }
        private string _TypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TypeName")]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        private int _GenreID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "GenreID")]
        public int GenreID
        {
            get { return _GenreID; }
            set { _GenreID = value; }
        }
        private string _GenreName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "GenreName")]
        public string GenreName
        {
            get { return _GenreName; }
            set { _GenreName = value; }
        }
        private string _Name;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _AddressID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AddressID")]
        public int AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        private int _TradeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TradeID")]
        public int TradeID
        {
            get { return _TradeID; }
            set { _TradeID = value; }
        }
        private string _TradeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TradeName")]
        public string TradeName
        {
            get { return _TradeName; }
            set { _TradeName = value; }
        }
        private decimal _Capital;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Capital")]
        public decimal Capital
        {
            get { return _Capital; }
            set { _Capital = value; }
        }
        private string _Corporation;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Corporation")]
        public string Corporation
        {
            get { return _Corporation; }
            set { _Corporation = value; }
        }
        private double _Square;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Square")]
        public double Square
        {
            get { return _Square; }
            set { _Square = value; }
        }
        private DateTime _StartTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set 
            {
                _StartTime = value;
                StartTimeStr = value.ToString("yyyy-MM-dd");
            }
        }

        [System.Runtime.Serialization.DataMember(Name = "StartTimeStr")]
        public string StartTimeStr { get; set; }
        
        private string _Tel;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Tel")]
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }
        private string _LicenceNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LicenceNum")]
        public string LicenceNum
        {
            get { return _LicenceNum; }
            set { _LicenceNum = value; }
        }
        private DateTime _LicenceStartTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime LicenceStartTime
        {
            get { return _LicenceStartTime; }
            set 
            { 
                _LicenceStartTime = value;
                LicenceStartTimeStr = value.ToString("yyyy-MM-dd");
            }
        }

        [System.Runtime.Serialization.DataMember(Name = "LicenceStartTimeStr")]
        public string LicenceStartTimeStr { get; set; }

        private DateTime _LicenceEndTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime LicenceEndTime
        {
            get { return _LicenceEndTime; }
            set 
            { 
                _LicenceEndTime = value;
                LicenceEndTimeStr = value.ToString("yyyy-MM-dd");
            }
        }

        [System.Runtime.Serialization.DataMember(Name = "LicenceEndTimeStr")]
        public string LicenceEndTimeStr { get; set; }

        private string _MainFrame;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MainFrame")]
        public string MainFrame
        {
            get { return _MainFrame; }
            set { _MainFrame = value; }
        }
        private string _Concurrently;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Concurrently")]
        public string Concurrently
        {
            get { return _Concurrently; }
            set { _Concurrently = value; }
        }
        private int _MigrantWorks;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MigrantWorks")]
        public int MigrantWorks
        {
            get { return _MigrantWorks; }
            set { _MigrantWorks = value; }
        }
        private int _FireRating;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "FireRating")]
        public int FireRating
        {
            get { return _FireRating; }
            set { _FireRating = value; }
        }
        private int _OrganID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OrganID")]
        public int OrganID
        {
            get { return _OrganID; }
            set { _OrganID = value; }
        }
        private string _OrganName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OrganName")]
        public string OrganName
        {
            get { return _OrganName; }
            set { _OrganName = value; }
        }
        private string _RoomID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "RoomID")]
        public string RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }

        /// <summary>
        /// 详细地址描述
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Addr")]
        public string Addr { get; set; }

        private Model.Address _address;        
        /// <summary>
        /// 详细地址信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Address")]
        public Address Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                if (value != null) Addr = value.Content;
            }
        }
    }
}

