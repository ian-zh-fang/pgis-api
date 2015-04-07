
/*****************************************************
*   Author: Ian.Fun
*   File: MonitorDevice.cs
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
    [System.Runtime.Serialization.DataContract(Name = "MonitorDevice", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class MonitorDevice :MBase
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
        private int _AddressID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "AddressID")]
        public int AddressID
        {
            get{ return _AddressID;}
            set{ _AddressID = value;}
        }
        private int _DeviceTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "DeviceTypeID")]
        public int DeviceTypeID
        {
            get{ return _DeviceTypeID;}
            set{ _DeviceTypeID = value;}
        }
        private string _DeviceTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "DeviceTypeName")]
        public string DeviceTypeName
        {
            get{ return _DeviceTypeName;}
            set{ _DeviceTypeName = value;}
        }
        private int _DoTypeID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "DoTypeID")]
        public int DoTypeID
        {
            get{ return _DoTypeID;}
            set{ _DoTypeID = value;}
        }
        private string _DoTypeName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "DoTypeName")]
        public string DoTypeName
        {
            get{ return _DoTypeName;}
            set{ _DoTypeName = value;}
        }
        private string _IP;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "IP")]
        public string IP
        {
            get{ return _IP;}
            set{ _IP = value;}
        }
        private int _Port;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Port")]
        public int Port
        {
            get{ return _Port;}
            set{ _Port = value;}
        }
        private string _Acct;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Acct")]
        public string Acct
        {
            get{ return _Acct;}
            set{ _Acct = value;}
        }
        private string _Pwd;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "Pwd")]
        public string Pwd
        {
            get{ return _Pwd;}
            set{ _Pwd = value;}
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

        //µØÖ·
        [System.Runtime.Serialization.DataMember(Name = "Address")]
        public Model.Address Address { get; set; }

        private int _administrativeid = 0;
        [System.Runtime.Serialization.DataMember(Name = "AdministrativeID")]
        public int AdministrativeID
        {
            get
            {
                if (Address == null)
                    return _administrativeid;

                _administrativeid = Address.AdminID;
                return _administrativeid;
            }
            set { _administrativeid = value; }
        }

        private string _administrativename = null;
        [System.Runtime.Serialization.DataMember(Name = "AdministrativeName")]
        public string AdministrativeName
        {
            get
            {
                if (Address == null)
                    return _administrativename;
                if (Address.Administrative == null)
                    return _administrativename;
                _administrativename = Address.Administrative.Name;
                return _administrativename;
            }
            set { _administrativename = value; }
        }

        private string _addresscontext = null;
        [System.Runtime.Serialization.DataMember(Name = "AddressContext")]
        public string AddressContext
        {
            get
            {
                if (Address == null)
                    return _addresscontext;
                _addresscontext = Address.Content;
                return _addresscontext;
            }
            set { _addresscontext = value; }
        }
    }

    [System.Runtime.Serialization.DataContract(Name = "MonitorDeviceEx", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class MonitorDeviceEx : MonitorDevice
    {
        /// <summary>
        /// Ñ²ÂßÂ·ÏßID
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "TrackID")]
        public int TrackID { get; set; }
    }
}

