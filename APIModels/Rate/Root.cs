using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.APIModels.Rate
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Attributes
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Record
    {
        public Attributes attributes { get; set; }
        public string Id { get; set; }
        public string enrtcr__Effective_Date__c { get; set; }
        public string enrtcr__End_Date__c { get; set; }
        public string Name { get; set; }
        public string enrtcr__Service__c { get; set; }
        public string enrtcr__Allow_Rate_Negotiation__c { get; set; }
        public double enrtcr__Amount_Ex_GST__c { get; set; }
        public string enrtcr__Quantity_Type__c { get; set; }
    }

    public class Root
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public List<Record> records { get; set; }
    }


}
