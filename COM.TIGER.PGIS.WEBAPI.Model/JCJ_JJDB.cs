
/*****************************************************
*   Author: Ian.Fun
*   File: JCJ_JJDB.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// 三台合一警报模型
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "JCJ_JJDB", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class JCJ_JJDB :MBase
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
        private string _Num;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Num")]
        public string Num
        {
            get{ return _Num;}
            set{ _Num = value;}
        }
        private int _TypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TypeID")]
        public int TypeID
        {
            get{ return _TypeID;}
            set{ _TypeID = value;}
        }
        private string _TypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TypeName")]
        public string TypeName
        {
            get{ return _TypeName;}
            set{ _TypeName = value;}
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
        private string _AlarmMan;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AlarmMan")]
        public string AlarmMan
        {
            get{ return _AlarmMan;}
            set{ _AlarmMan = value;}
        }
        private string _Location;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Location")]
        public string Location
        {
            get{ return _Location;}
            set{ _Location = value;}
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
        private DateTime _AlarmTime;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AlarmTime")]
        public DateTime AlarmTime
        {
            get{ return _AlarmTime;}
            set{ _AlarmTime = value;}
        }
        private string _AdminName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AdminName")]
        public string AdminName
        {
            get{ return _AdminName;}
            set{ _AdminName = value;}
        }
        private int _AdminID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AdminID")]
        public int AdminID
        {
            get{ return _AdminID;}
            set{ _AdminID = value;}
        }
    }
}

