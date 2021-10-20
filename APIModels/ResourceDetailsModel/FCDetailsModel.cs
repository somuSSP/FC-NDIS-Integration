using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.APIModels.ResourceDetailsModel
{
  
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Communication
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Pager { get; set; }
        public string Fax { get; set; }
        public string MobileEmail { get; set; }
        public string MailingAddress { get; set; }
        public int CommunicationMethod { get; set; }
    }

    public class Insurance
    {
        public object IssueDate { get; set; }
        public object ExpiryDate { get; set; }
    }

    public class License
    {
        public bool IsMale { get; set; }
        public string Class { get; set; }
        public string ProvinceCode { get; set; }
        public object IssueDate { get; set; }
        public object ExpiryDate { get; set; }
    }

    public class WorkInfo
    {
        public int WorkStatus { get; set; }
        public bool IsCrewChief { get; set; }
        public DateTime? EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }
        public string DispatchDeviceID { get; set; }
        public string PTTNumber { get; set; }
        public string MobileID { get; set; }
        public string PIN { get; set; }
    }

    public class Picture
    {
        public object picture { get; set; }
    }

    public class ResourceType
    {
        public int ResourceCount { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class WorkSchedule
    {
        public bool IsSundayActive { get; set; }
        public bool IsMondayActive { get; set; }
        public bool IsTuesdayActive { get; set; }
        public bool IsWednesdayActive { get; set; }
        public bool IsThursdayActive { get; set; }
        public bool IsFridayActive { get; set; }
        public bool IsSaturdayActive { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class SettlementOptions
    {
        public string ID { get; set; }
        public double GuaranteeAmount { get; set; }
        public double DailyExpectedRevenue { get; set; }
        public int SettlementFrequency { get; set; }
        public int SettlementDeliveryMethod { get; set; }
        public string SettlementDeliveryRecipientEmail { get; set; }
        public object LastSettlementPeriodEndDate { get; set; }
        public object NextSettlementPeriodEndDate { get; set; }
        public object LastAccountingSettlementID { get; set; }
        public string SettlementReportID { get; set; }
        public object LastAccountingSettlement { get; set; }
    }

    public class Data
    {
        public string LanguagePreference { get; set; }
        public string Company { get; set; }
        public bool IsStrongBox { get; set; }
        public string OutsideCode { get; set; }
        public string InsideCode { get; set; }
        public Communication Communication { get; set; }
        public Insurance Insurance { get; set; }
        public License License { get; set; }
        public WorkInfo WorkInfo { get; set; }
        public object AssignedCrewID { get; set; }
        public string WorkScheduleID { get; set; }
        public object HomeBaseID { get; set; }
        public string ResourceTypeID { get; set; }
        public Picture Picture { get; set; }
        public object HomeBase { get; set; }
        public List<object> Assets { get; set; }
        public ResourceType ResourceType { get; set; }
        public object CustomFields { get; set; }
        public object HOS { get; set; }
        public WorkSchedule WorkSchedule { get; set; }
        public List<object> Routes { get; set; }
        public SettlementOptions SettlementOptions { get; set; }
        public List<object> TaskLegs { get; set; }
        public List<object> DeductionAssignments { get; set; }
    }

    public class FCDetailsModel
    {
        public Data Data { get; set; }
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }


}
