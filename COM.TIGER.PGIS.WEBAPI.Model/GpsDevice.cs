
/*****************************************************
*   Author: Ian.Fun
*   File: GpsDevice.cs
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
    [System.Runtime.Serialization.DataContract(Name = "GpsDevice", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class GpsDevice :MBase
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
        private string _DeviceID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "DeviceID")]
        public string DeviceID
        {
            get{ return _DeviceID;}
            set{ _DeviceID = value;}
        }
        private string _OfficerID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OfficerID")]
        public string OfficerID
        {
            get{ return _OfficerID;}
            set{ _OfficerID = value;}
        }
        private DateTime _BindTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime BindTime
        {
            get{ return _BindTime;}
            set{ _BindTime = value;}
        }

        private string _BdTime;
        [System.Runtime.Serialization.DataMember(Name = "BindTime")]
        public string BdTime
        {
            get { return _BdTime = _BdTime ?? BindTime.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { DateTime.TryParse(_BdTime = value, out _BindTime); }
        }

        private int _CarID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CarID")]
        public int CarID
        {
            get{ return _CarID;}
            set{ _CarID = value;}
        }
        private string _CarNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CarNum")]
        public string CarNum
        {
            get{ return _CarNum;}
            set{ _CarNum = value;}
        }
    }
}

