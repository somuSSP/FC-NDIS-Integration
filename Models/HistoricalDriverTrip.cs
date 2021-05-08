using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class HistoricalDriverTrip
    {
        public HistoricalDriverTrip()
        {
            HistoricalCustomerTrips = new HashSet<HistoricalCustomerTrip>();
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<HistoricalCustomerTrip> HistoricalCustomerTrips { get; set; }
    }
}
