using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace sharedObjects
{

    [DataContract]
    public class SysInfo
    {
        public SysInfo()
        {
            SystemName = System.Environment.MachineName;
            myDrives = new List<Drive>();
        }
        [DataMember]
        public List<Drive> myDrives { get; set; }
        [DataMember]
        public string SystemName { get; set; }

        [DataMember]
        public string ipAddress { get; set; }

        [DataMember]
        public BenchmarkSettings benchmarkSettings { get; set;  }
    }

    [DataContract]
    public class Drive
    {
        [DataMember]
        public string caption { get; set; }
        [DataMember]
        public string physicalLoc { get; set; }
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public string partitions { get; set; }
        [DataMember]
        public string size { get; set; }
    }
    [DataContract]
    public class BenchmarkSettings
    {
        [DataMember]
        public int time { get; set; }
        [DataMember]
        public string selecedDriveLoc { get; set; }
        [DataMember]
        public int packetSize { get; set; }
    }
}
