using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FC_NDIS.JsonModels.Resource
{
   
    public class Branch
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

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
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class License
    {
        public bool IsMale { get; set; }
        public string Class { get; set; }
        public string ProvinceCode { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
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

    public class Pictures
    {
        public string Picture { get; set; }
    }

    public class HomeBase
    {
        public string Description { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
        public string ProvinceCode { get; set; }
        public string CountryCode { get; set; }
        public string AddressInfo { get; set; }
        public string Code { get; set; }
    }

    public class Asset
    {
        public DateTime LastUpdatedTimeStamp { get; set; }
        public string GpsLocation { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class ResourceType
    {
        public int ResourceCount { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Definition
    {
        public string Description { get; set; }
        public int Type { get; set; }
    }

    public class CustomField
    {
        public string Value { get; set; }
        public int CustomFieldID { get; set; }
        public Definition Definition { get; set; }
    }

    public class HOS
    {
        public string ID { get; set; }
        public int DutyStatusType { get; set; }
        public int AvailableTimeInMinutes { get; set; }
        public int DriveTimeInMinutes { get; set; }
        public int OnDutyTimeInMinutes { get; set; }
        public int CycleOnDutyTimeInMinutes { get; set; }
        public int DailyResetTimeRemainingInMinutes { get; set; }
        public int CycleResetTimeRemainingInMinutes { get; set; }
        public DateTime LastUpdatedTimeStamp { get; set; }
    }

    public class WorkSchedules
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

    public class Route
    {
        public string RouteId { get; set; }
        public int Status { get; set; }
        public DateTime EstimatedStartTimeStamp { get; set; }
        public DateTime EstimatedCompletedTimeStamp { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedTimeStamp { get; set; }
        public bool IsIrregularRoute { get; set; }
        public string RouteResourcePayID { get; set; }
    }

    public class LastAccountingSettlement
    {
        public string AccountingSettlementID { get; set; }
        public int Amount { get; set; }
    }

    public class SettlementOptions
    {
        public string ID { get; set; }
        public int GuaranteeAmount { get; set; }
        public int DailyExpectedRevenue { get; set; }
        public int SettlementFrequency { get; set; }
        public int SettlementDeliveryMethod { get; set; }
        public string SettlementDeliveryRecipientEmail { get; set; }
        public DateTime LastSettlementPeriodEndDate { get; set; }
        public DateTime NextSettlementPeriodEndDate { get; set; }
        public string LastAccountingSettlementID { get; set; }
        public string SettlementReportID { get; set; }
        public LastAccountingSettlement LastAccountingSettlement { get; set; }
    }

    public class Task
    {
        public string TaskId { get; set; }
        public string ServiceOfferingID { get; set; }
        public string ServiceTypeID { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdatedTimeStamp { get; set; }
    }

    public class TaskLeg2
    {
    }

    public class TaskPayList
    {
        public string TaskID { get; set; }
        public string TaskLegID { get; set; }
        public string AccountingSettlementID { get; set; }
        public TaskLeg TaskLeg { get; set; }
    }

    public class TaskLeg
    {
        public string TaskLegId { get; set; }
        public string TaskId { get; set; }
        public int Status { get; set; }
        public string AssignedResourceId { get; set; }
        public Task Task { get; set; }
        public List<TaskPayList> TaskPayList { get; set; }
    }

    public class DeductionAssignment
    {
        public string ID { get; set; }
        public string ResourceID { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Details
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
        public Guid AssignedCrewID { get; set; }
        public Guid WorkScheduleID { get; set; }
        public Guid HomeBaseID { get; set; }
        public string ResourceTypeID { get; set; }
        public Pictures Picture { get; set; }
        public HomeBase HomeBase { get; set; }
        public List<Asset> Assets { get; set; }
        public ResourceType ResourceType { get; set; }
        public List<CustomField> CustomFields { get; set; }
        public HOS HOS { get; set; }
        public WorkSchedules WorkSchedule { get; set; }
        public List<Route> Routes { get; set; }
        public SettlementOptions SettlementOptions { get; set; }
        public List<TaskLeg> TaskLegs { get; set; }
        public List<DeductionAssignment> DeductionAssignments { get; set; }
    }

    public class Parent
    {
    }

    public class EquipmentType
    {
        public string Code { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class VehicleType
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Certification
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Resource
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsSettlementOnly { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsShared { get; set; }
        public DateTime LastUpdatedTimeStamp { get; set; }
        public string AssignedBranchID { get; set; }
        public Branch Branch { get; set; }
        public Details Details { get; set; }
        public List<Parent> Parents { get; set; }
        public List<EquipmentType> EquipmentTypes { get; set; }
        public List<VehicleType> VehicleTypes { get; set; }
        public List<Certification> Certifications { get; set; }
    }


}
