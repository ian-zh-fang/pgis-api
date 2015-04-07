using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "capture", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Capture:MBase
    {
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Coordinates")]
        public string Coordinates { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Type")]
        public int Type { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "X")]
        public double X { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Y")]
        public double Y { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Remark")]
        public string Remark { get; set; }
    }
}
