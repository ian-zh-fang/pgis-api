
/*****************************************************
*   Author: Ian.Fun
*   File: PatrolRecord.cs
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
    [System.Runtime.Serialization.DataContract(Name = "PatrolRecord", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class PatrolRecord :MBase
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
        private int _PatrolID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PatrolID")]
        public int PatrolID
        {
            get{ return _PatrolID;}
            set{ _PatrolID = value;}
        }
        private string _Patrol;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Patrol")]
        public string Patrol
        {
            get{ return _Patrol;}
            set{ _Patrol = value;}
        }
        private DateTime _CurrentTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime CurrentTime
        {
            get{ return _CurrentTime;}
            set { _CurrentTime = value; _currenttimestr = value.ToString("yyyy-MM-dd HH:mm"); }
        }
        private string _Remark;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Remark")]
        public string Remark
        {
            get{ return _Remark;}
            set{ _Remark = value;}
        }
        private int _PatrolMonitorID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PatrolMonitorID")]
        public int PatrolMonitorID
        {
            get { return _PatrolMonitorID; }
            set { _PatrolMonitorID = value; }
        }
        
        public string _currenttimestr;
        /// <summary>
        /// 当前时间的字符串.格式 yyyy-MM-dd HH:mm
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "CurrentTime")]
        public string CurrentTimeStr
        {
            get { return _currenttimestr; }
            set { _currenttimestr = value; }
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "DeviceID")]
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Device")]
        public MonitorDevice Device { get; set; }

        /// <summary>
        /// 线路
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Track")]
        public PatrolTrack Track { get; set; }

        /// <summary>
        /// 巡逻人员
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Officer")]
        public Officer Officer { get; set; }
    }
}

