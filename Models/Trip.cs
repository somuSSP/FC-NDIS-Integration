using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class Trip
    {
        public Trip()
        {
            BillingLines = new HashSet<BillingLine>();
            CustomerTrips = new HashSet<CustomerTrip>();
            SharedTrips = new HashSet<SharedTrip>();
        }

        public int TripId { get; set; }
        public int? TripTypeId { get; set; }
        public string TripDescription { get; set; }
        public int? VehicleId { get; set; }
        public int? CostCenter { get; set; }
        public float? TotalKm { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? TripStatus { get; set; }
        public bool? Shared { get; set; }
        public int? BillingLineId { get; set; }
        public int? DriverId { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartGps { get; set; }
        public string EndGps { get; set; }
        public float? SoloKm { get; set; }
        public bool? IsDeleted { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? VehicleCategory { get; set; }
        public int? VehicleType { get; set; }
        public string SalesForceUserId { get; set; }
        public string JobNumber { get; set; }
        public DateTime? TripCreatedDate { get; set; }
        public bool? ConvertedToBillable { get; set; }
        public bool? OnHold { get; set; }
        public string ChangedHistory { get; set; }
        public string VehicleRegistrationNumber { get; set; }

        public virtual CostCentre CostCenterNavigation { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual TripStatus TripStatusNavigation { get; set; }
        public virtual TripType TripType { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual VehicleCategory VehicleCategoryNavigation { get; set; }
        public virtual VehicleType VehicleTypeNavigation { get; set; }
        public virtual ICollection<BillingLine> BillingLines { get; set; }
        public virtual ICollection<CustomerTrip> CustomerTrips { get; set; }
        public virtual ICollection<SharedTrip> SharedTrips { get; set; }
    }
}
