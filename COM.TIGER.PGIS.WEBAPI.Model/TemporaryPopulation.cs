
/*****************************************************
*   Author: Ian.Fun
*   File: TemporaryPopulation.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// 暂住人员
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "TemporaryPopulation", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class TemporaryPopulation :MBase
    {
        private int _TP_ID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TP_ID")]
        public int TP_ID
        {
            get{ return _TP_ID;}
            set{ _TP_ID = value;}
        }

        private string _LandlordName;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LandlordName")]
        public string LandlordName
        {
            get{ return _LandlordName;}
            set{ _LandlordName = value;}
        }

        private string _LandlordCard;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LandlordCard")]
        public string LandlordCard
        {
            get{ return _LandlordCard;}
            set{ _LandlordCard = value;}
        }

        private string _LandlordPhone;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "LandlordPhone")]
        public string LandlordPhone
        {
            get{ return _LandlordPhone;}
            set{ _LandlordPhone = value;}
        }

        private DateTime _TP_Date;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TP_Date")]
        public DateTime TP_Date
        {
            get{ return _TP_Date;}
            set{ _TP_Date = value;}
        }

        private int _TP_ReasonID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TP_ReasonID")]
        public int TP_ReasonID
        {
            get{ return _TP_ReasonID;}
            set{ _TP_ReasonID = value;}
        }

        private string _TP_Reason;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "TP_Reason")]
        public string TP_Reason
        {
            get{ return _TP_Reason;}
            set{ _TP_Reason = value;}
        }

        private string _ResidenceNo;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "ResidenceNo")]
        public string ResidenceNo
        {
            get{ return _ResidenceNo;}
            set{ _ResidenceNo = value;}
        }

        private int _PoID;
        ///<summary>
        /// 
        ///</summary>
        [System.Runtime.Serialization.DataMember(Name = "PoID")]
        public int PoID
        {
            get{ return _PoID;}
            set{ _PoID = value;}
        }

        private string _addr;
        [System.Runtime.Serialization.DataMember(Name = "Addr")]
        public string Addr
        {
            get { return _addr; }
            set { _addr = value; }
        }

        /********************************************************************
        *   TemporaryPopulation extention
        *********************************************************************
        */

        /// <summary>
        /// 基础信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name="BasicInfo")]
        public PopulationBasicInfo BasicInfo { get; set; }

        /// <summary>
        /// 暂住事由
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name="Reasons")]
        public Param Reasons { get; set; }
    }
}

