using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "CaptureType", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class CaptureType:MBase, IComparable<CaptureType>
    {
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "IconCls")]
        public string IconCls { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Color")]
        public string Color { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Type")]
        public int Type { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Remark")]
        public string Remark { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Sort")]
        public int Sort { get; set; }
        
        public int CompareTo(CaptureType other)
        {
            if (other.Sort > Sort) return -1;
            if (other.Sort < Sort) return 1;
            return 0;
        }
    }
}
