using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.JsonModels
{
    

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Datum
    {
        public string ID { get; set; }
        public string DriverName { get; set; }
        public string ResourceCode { get; set; }
        public int WorkStatus { get; set; }
        public string AssignedBranchID { get; set; }
        public string BranchDescription { get; set; }
        public bool IsShared { get; set; }
        public bool IsDeleted { get; set; }
        public object LicenseExpiryDate { get; set; }
        public object InsuranceExpiryDate { get; set; }
        public bool IsStrongBox { get; set; }
        public string ResourceTypeID { get; set; }
        public bool IsActive { get; set; }
        public int ResourceCategory { get; set; }
        public string ResourceTypeDescription { get; set; }
        public string Company { get; set; }
        public bool HasDevice { get; set; }
        public string KodiakNumber { get; set; }
        public double DailyExpectedRevenue { get; set; }
        public double GuaranteeAmount { get; set; }
        public int SettlementFrequency { get; set; }
        public string WorkScheduleID { get; set; }
        public DateTime LastUpdatedTimeStamp { get; set; }
        public int ActiveWork { get; set; }
        public object Assets { get; set; }
    }

    public class OutputResource
    {
        public List<Datum> Data { get; set; }
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }


}
