
/*****************************************************
*   Author: Ian.Fun
*   File: GpsDeviceTrack.cs
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
    [System.Runtime.Serialization.DataContract(Name = "GpsDeviceTrack", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class GpsDeviceTrack :MBase
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
        private string _OfficerNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OfficerNum")]
        public string OfficerNum
        {
            get{ return _OfficerNum;}
            set{ _OfficerNum = value;}
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
        private DateTime _CurrentTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime CurrentTime
        {
            get{ return _CurrentTime;}
            set{ _CurrentTime = value;}
        }

        private string _CurrTime;
        [System.Runtime.Serialization.DataMember(Name = "CurrentTime")]
        public string CurrTime
        {
            get { return _CurrTime = _CurrTime ?? CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { DateTime.TryParse(_CurrTime = value, out _CurrentTime); }
        }

        private GpsDevice _device;
        [System.Runtime.Serialization.DataMember(Name = "Device")]
        public GpsDevice Device
        {
            get { return _device; }
            set { _device = value; }
        }
    }
}

