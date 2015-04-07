
/*****************************************************
*   Author: Ian.Fun
*   File: PatrolTrack.cs
*   Version: 1.0.0.0
*   Description: 
******************************************************
*/

using System;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    ///<summary>
    /// 
    ///</summary>
    [System.Runtime.Serialization.DataContract(Name = "PatrolTrack", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class PatrolTrack :MBase
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
        private DateTime? _SettedTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime? SettedTime
        {
            get{ return _SettedTime;}
            set{ _SettedTime = value;}
        }
        private DateTime? _UpdatedTime;
        ///<summary>
        /// 
        ///</summary>
        public DateTime? UpdatedTime
        {
            get{ return _UpdatedTime;}
            set{ _UpdatedTime = value;}
        }

        private string _SettingTime;
        [System.Runtime.Serialization.DataMember(Name = "SettedTime")]
        public string SettingTime
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_SettingTime))
                    return _SettingTime;

                _SettingTime = _SettedTime == null ? null : _SettedTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                return _SettingTime;
            }
            set {
                _SettingTime = value;
                _SettedTime = string.IsNullOrWhiteSpace(value) ? null : (DateTime?)DateTime.Parse(value);
            }
        }

        private string _UpdattingTime;
        [System.Runtime.Serialization.DataMember(Name = "UpdatedTime")]
        public string UpdattingTime
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_UpdattingTime))
                    return _UpdattingTime;

                _UpdattingTime = _UpdatedTime == null ? null : _UpdatedTime.Value.ToString("yyyy-MM-dd HH:mm:ss:fff");
                return _UpdattingTime;
            }
            set
            {
                _UpdattingTime = value;
                _UpdatedTime = string.IsNullOrWhiteSpace(value) ? null : (DateTime?)DateTime.Parse(value);
            }
        }

        private System.Collections.Generic.List<Model.MonitorDeviceEx> _devices = new System.Collections.Generic.List<MonitorDeviceEx>();
        [System.Runtime.Serialization.DataMember(Name = "Devices")]
        public Model.MonitorDeviceEx[] Devices
        {
            get 
            {
                var arr = new Model.MonitorDeviceEx[_devices.Count];
                _devices.CopyTo(arr);
                return arr;
            }
            set { Add(value); }
        }

        public void Add(System.Collections.Generic.IEnumerable<Model.MonitorDeviceEx> items)
        {
            if (items == null)
                return;

            var list = items.Where(t => !_devices.Exists(x => x.ID == t.ID) && t.TrackID == ID);
            _devices.AddRange(list);
        }
    }
}

