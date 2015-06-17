
/*****************************************************
*   Author: Ian.Fun
*   File: Employee.cs
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
    [System.Runtime.Serialization.DataContract(Name = "Employee", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Employee :MBase
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
        private int _OrganID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OrganID")]
        public int OrganID
        {
            get{ return _OrganID;}
            set{ _OrganID = value;}
        }
        private int _OrganTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OrganTypeID")]
        public int OrganTypeID
        {
            get{ return _OrganTypeID;}
            set{ _OrganTypeID = value;}
        }
        private string _OrganTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "OrganTypeName")]
        public string OrganTypeName
        {
            get{ return _OrganTypeName;}
            set{ _OrganTypeName = value;}
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
        private int _CardTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CardTypeID")]
        public int CardTypeID
        {
            get{ return _CardTypeID;}
            set{ _CardTypeID = value;}
        }
        private string _CardTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CardTypeName")]
        public string CardTypeName
        {
            get{ return _CardTypeName;}
            set{ _CardTypeName = value;}
        }
        private string _IdentityCardNum;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "IdentityCardNum")]
        public string IdentityCardNum
        {
            get{ return _IdentityCardNum;}
            set{ _IdentityCardNum = value;}
        }
        private int _GenderID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "GenderID")]
        public int GenderID
        {
            get{ return _GenderID;}
            set{ _GenderID = value;}
        }
        private string _GenderDesc;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "GenderDesc")]
        public string GenderDesc
        {
            get{ return _GenderDesc;}
            set{ _GenderDesc = value;}
        }
        private int _ProvinceID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "ProvinceID")]
        public int ProvinceID
        {
            get{ return _ProvinceID;}
            set{ _ProvinceID = value;}
        }
        private string _ProvinceName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "ProvinceName")]
        public string ProvinceName
        {
            get{ return _ProvinceName;}
            set{ _ProvinceName = value;}
        }
        private int _CityID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CityID")]
        public int CityID
        {
            get{ return _CityID;}
            set{ _CityID = value;}
        }
        private string _CityName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "CityName")]
        public string CityName
        {
            get{ return _CityName;}
            set{ _CityName = value;}
        }
        private string _Address;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Address")]
        public string Address
        {
            get{ return _Address;}
            set{ _Address = value;}
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
        private int _Seniority;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Seniority")]
        public int Seniority
        {
            get{ return _Seniority;}
            set{ _Seniority = value;}
        }
        private string _Func;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Func")]
        public string Func
        {
            get{ return _Func;}
            set{ _Func = value;}
        }
        private int _JobTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "JobTypeID")]
        public int JobTypeID
        {
            get{ return _JobTypeID;}
            set{ _JobTypeID = value;}
        }
        private string _JobTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "JobTypeName")]
        public string JobTypeName
        {
            get{ return _JobTypeName;}
            set{ _JobTypeName = value;}
        }
        private DateTime _EntryTime = DateTime.Now;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "EntryTime")]
        public DateTime EntryTime
        {
            get{ return _EntryTime;}
            set{ _EntryTime = value;}
        }
        private int _IsInService;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "IsInService")]
        public int IsInService
        {
            get{ return _IsInService;}
            set{ _IsInService = value;}
        }
        private DateTime _QuitTime = DateTime.Now;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "QuitTime")]
        public DateTime QuitTime
        {
            get{ return _QuitTime;}
            set{ _QuitTime = value;}
        }
    }

    [System.Runtime.Serialization.DataContract(Name = "CompanyEmployee", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class CompanyEmployee : Employee
    {
        [System.Runtime.Serialization.DataMember(Name = "Company")]
        public Model.Company Company { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "CompanyName")]
        public string CompanyName
        {
            get { return Company == null ? null : Company.Name; }
            private set
            {
                if (Company != null)
                    Company.Name = value;
            }
        }
    }
}

