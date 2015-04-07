
/*****************************************************
*   Author: Ian.Fun
*   File: StreetNum.cs
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
    [System.Runtime.Serialization.DataContract(Name = "StreetNum", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class StreetNum :MBase
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

        private int _StreetID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetID")]
        public int StreetID
        {
            get{ return _StreetID;}
            set{ _StreetID = value;}
        }

        private string _StreetName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetName")]
        public string StreetName
        {
            get{ return _StreetName;}
            set{ _StreetName = value;}
        }

        private int _StreetTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetTypeID")]
        public int StreetTypeID
        {
            get{ return _StreetTypeID;}
            set{ _StreetTypeID = value;}
        }

        private string _StreetTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "StreetTypeName")]
        public string StreetTypeName
        {
            get{ return _StreetTypeName;}
            set{ _StreetTypeName = value;}
        }

        private double _X = 0.00d;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "X")]
        public double X
        {
            get{ return _X;}
            set{ _X = value;}
        }

        private double _Y = 0.00d;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Y")]
        public double Y
        {
            get{ return _Y;}
            set{ _Y = value;}
        }

        private double _CX = 0.00d;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CX")]
        public double CX
        {
            get{ return _CX;}
            set{ _CX = value;}
        }

        private double _CY = 0.00d;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CY")]
        public double CY
        {
            get{ return _CY;}
            set{ _CY = value;}
        }

        private int _LivingState;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LivingState")]
        public int LivingState
        {
            get{ return _LivingState;}
            set{ _LivingState = value;}
        }

        private DateTime _EndTime;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "EndTime")]
        public DateTime EndTime
        {
            get{ return _EndTime;}
            set{ _EndTime = value;}
        }

        private string _SourceFrom;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "SourceFrom")]
        public string SourceFrom
        {
            get{ return _SourceFrom;}
            set{ _SourceFrom = value;}
        }
    }
}

