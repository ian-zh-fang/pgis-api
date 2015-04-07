
/*****************************************************
*   Author: Ian.Fun
*   File: OwnerPic.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// Â¥·¿Í¼Æ¬ÐÅÏ¢
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "OwnerPic", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class OwnerPic : MBase_Map
    {
        private int _MOP_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_ID")]
        public int MOP_ID
        {
            get{ return _MOP_ID;}
            set{ _MOP_ID = value;}
        }
        private int _MOP_MOI_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_MOI_ID")]
        public int MOP_MOI_ID
        {
            get{ return _MOP_MOI_ID;}
            set{ _MOP_MOI_ID = value;}
        }
        private string _MOP_ImgName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_ImgName")]
        public string MOP_ImgName
        {
            get{ return _MOP_ImgName;}
            set{ _MOP_ImgName = value;}
        }
        private string _MOP_ImgTitle;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_ImgTitle")]
        public string MOP_ImgTitle
        {
            get{ return _MOP_ImgTitle;}
            set{ _MOP_ImgTitle = value;}
        }
        private string _MOP_ImgRemark;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_ImgRemark")]
        public string MOP_ImgRemark
        {
            get{ return _MOP_ImgRemark;}
            set{ _MOP_ImgRemark = value;}
        }
        private string _MOP_ImgPath;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_ImgPath")]
        public string MOP_ImgPath
        {
            get{ return _MOP_ImgPath;}
            set{ _MOP_ImgPath = value;}
        }
        private int _MOP_Sort;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_Sort")]
        public int MOP_Sort
        {
            get{ return _MOP_Sort;}
            set{ _MOP_Sort = value;}
        }
        private int _MOP_ImgDefault;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "MOP_ImgDefault")]
        public int MOP_ImgDefault
        {
            get{ return _MOP_ImgDefault;}
            set{ _MOP_ImgDefault = value;}
        }
        private DateTime _MOP_Createdate = DateTime.Now;
        ///<summary>
        /// 
        ///</summary>
        //[System.Runtime.Serialization.DataMember(Name = "MOP_Createdate")]
        public DateTime MOP_Createdate
        {
            get{ return _MOP_Createdate;}
            set{ _MOP_Createdate = value;}
        }
        private DateTime _MOP_Updatedate = DateTime.Now;
        ///<summary>
        /// 
        ///</summary>
        //[System.Runtime.Serialization.DataMember(Name = "MOP_Updatedate")]
        public DateTime MOP_Updatedate
        {
            get{ return _MOP_Updatedate;}
            set{ _MOP_Updatedate = value;}
        }
        private string _JID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "JID")]
        public string JID
        {
            get{ return _JID;}
            set{ _JID = value;}
        }
    }
}

