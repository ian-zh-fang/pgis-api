
/*****************************************************
*   Author: Ian.Fun
*   File: PatrolMonitor.cs
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
    [System.Runtime.Serialization.DataContract(Name = "PatrolMonitor", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class PatrolMonitor :MBase
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
        private int _TrackID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TrackID")]
        public int TrackID
        {
            get{ return _TrackID;}
            set{ _TrackID = value;}
        }
        private int _MonitorID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MonitorID")]
        public int MonitorID
        {
            get{ return _MonitorID;}
            set{ _MonitorID = value;}
        }
        private int _Sort;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Sort")]
        public int Sort
        {
            get{ return _Sort;}
            set{ _Sort = value;}
        }
    }
}

