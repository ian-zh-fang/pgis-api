
/*****************************************************
*   Author: Ian.Fun
*   File: PassCar.cs
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
    [System.Runtime.Serialization.DataContract(Name = "PassCar", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class PassCar :MBase
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
        private int _PassID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PassID")]
        public int PassID
        {
            get{ return _PassID;}
            set{ _PassID = value;}
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
        private string _CapturePic;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CapturePic")]
        public string CapturePic
        {
            get{ return _CapturePic;}
            set{ _CapturePic = value;}
        }
        private DateTime _CaptureTime;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CaptureTime")]
        public DateTime CaptureTime
        {
            get{ return _CaptureTime;}
            set{ _CaptureTime = value;}
        }
        private int _IsPeccancy;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "IsPeccancy")]
        public int IsPeccancy
        {
            get{ return _IsPeccancy;}
            set{ _IsPeccancy = value;}
        }
        private int _PeccancyTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PeccancyTypeID")]
        public int PeccancyTypeID
        {
            get{ return _PeccancyTypeID;}
            set{ _PeccancyTypeID = value;}
        }
        private string _PeccancyTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PeccancyTypeName")]
        public string PeccancyTypeName
        {
            get{ return _PeccancyTypeName;}
            set{ _PeccancyTypeName = value;}
        }
    }
}

