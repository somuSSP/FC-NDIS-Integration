using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.RestAPIModels.Customer
{


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Attributes
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class RecordType
    {
        public Attributes attributes { get; set; }
        public string Name { get; set; }
    }

    public class Record
    {
        public Attributes attributes { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string OtherStreet { get; set; }
        public string OtherCity { get; set; }
        public string OtherState { get; set; }
        public string OtherPostalCode { get; set; }
        public RecordType RecordType { get; set; }
        public string Enrite_Care_Auto_Number__c { get; set; }
        public string enrtcr__Status__c { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class Root
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public string nextRecordsUrl { get; set; }
        public List<Record> records { get; set; }
    }

}
