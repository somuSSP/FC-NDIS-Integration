using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.APIModels.CustomerServiceLine
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Attributes
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class EnrtcrSupportContractR
    {
        public Attributes attributes { get; set; }
        public string Name { get; set; }
        public string enrtcr__End_Date__c { get; set; }
        public string enrtcr__Status__c { get; set; }
        public string enrtcr__Funding_Type__c { get; set; }
        public string enrtcr__Funding_Management__c { get; set; }
        public string enrtcr__Client__c { get; set; }
    }

    public class EnrtcrCategoryItemR
    {
        public Attributes attributes { get; set; }
        public double? enrtcr__Support_Category_Amount__c { get; set; }
        public double? enrtcr__Delivered__c { get; set; }
    }

    public class EnrtcrSiteR
    {
        public Attributes attributes { get; set; }
        public string Name { get; set; }
        public string enrtcr__Site_GL_Code__c { get; set; }
    }

    public class EnrtcrServiceR
    {
        public Attributes attributes { get; set; }
        public string Name { get; set; }
        public string enrtcr__Travel_Service__c { get; set; }
        public string enrtcr__Transport_Service__c { get; set; }
    }

    public class Record
    {
        public Attributes attributes { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public double enrtcr__Remaining__c { get; set; }
        public string enrtcr__Item_Overclaim__c { get; set; }
        public string enrtcr__Support_Contract__c { get; set; }
        public EnrtcrSupportContractR enrtcr__Support_Contract__r { get; set; }
        public string enrtcr__Support_Category__c { get; set; }
        public EnrtcrCategoryItemR enrtcr__Category_Item__r { get; set; }
        public string enrtcr__Site__c { get; set; }
        public EnrtcrSiteR enrtcr__Site__r { get; set; }
        public string enrtcr__Service__c { get; set; }
        public EnrtcrServiceR enrtcr__Service__r { get; set; }
        public string enrtcr__Site_Service_Program__c { get; set; }
    }

    public class Root
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public string nextRecordsUrl { get; set; }
        public List<Record> records { get; set; }
    }


}
