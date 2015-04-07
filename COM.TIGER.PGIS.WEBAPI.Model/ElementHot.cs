
/*****************************************************
*   Author: Ian.Fun
*   File: ElementHot.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// 地图热区信息
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "ElementHot", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class ElementHot : MBase_Map
    {
        private int _MEH_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_ID")]
        public int MEH_ID
        {
            get{ return _MEH_ID;}
            set{ _MEH_ID = value;}
        }
        private int _MEH_PID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_PID")]
        public int MEH_PID
        {
            get{ return _MEH_PID;}
            set{ _MEH_PID = value;}
        }
        private int _MEH_MOI_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_MOI_ID")]
        public int MEH_MOI_ID
        {
            get{ return _MEH_MOI_ID;}
            set{ _MEH_MOI_ID = value;}
        }
        private string _MEH_CenterX;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_CenterX")]
        public string MEH_CenterX
        {
            get{ return _MEH_CenterX;}
            set{ _MEH_CenterX = value;}
        }
        private string _MEH_CenterY;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_CenterY")]
        public string MEH_CenterY
        {
            get{ return _MEH_CenterY;}
            set{ _MEH_CenterY = value;}
        }
        private string _MEH_Croods;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_Croods")]
        public string MEH_Croods
        {
            get{ return _MEH_Croods;}
            set{ _MEH_Croods = value;}
        }
        private string _MEH_HotLevel = "0,1";
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_HotLevel")]
        public string MEH_HotLevel
        {
            get{ return _MEH_HotLevel;}
            set{ _MEH_HotLevel = value;}
        }
        private int _MEH_IsLabel = 1;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_IsLabel")]
        public int MEH_IsLabel
        {
            get{ return _MEH_IsLabel;}
            set{ _MEH_IsLabel = value;}
        }
        private int _MEH_HotFlag =1;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_HotFlag")]
        public int MEH_HotFlag
        {
            get{ return _MEH_HotFlag;}
            set{ _MEH_HotFlag = value;}
        }
        private DateTime _MEH_CreateDate = DateTime.Now;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_CreateDate")]
        public DateTime MEH_CreateDate
        {
            get{ return _MEH_CreateDate;}
            set{ _MEH_CreateDate = value;}
        }
        private DateTime _MEH_UpdateDate = DateTime.Now;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_UpdateDate")]
        public DateTime MEH_UpdateDate
        {
            get{ return _MEH_UpdateDate;}
            set{ _MEH_UpdateDate = value;}
        }
        private int _MEH_IsLock;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MEH_IsLock")]
        public int MEH_IsLock
        {
            get{ return _MEH_IsLock;}
            set{ _MEH_IsLock = value;}
        }
        private double _Area;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Area")]
        public double Area
        {
            get{ return _Area;}
            set{ _Area = value;}
        }
        private int _flag;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "flag")]
        public int flag
        {
            get{ return _flag;}
            set{ _flag = value;}
        }
    }
}

