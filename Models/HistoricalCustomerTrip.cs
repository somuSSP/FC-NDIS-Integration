using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class HistoricalCustomerTrip
    {
        public int CustomerTripId { get; set; }
        public int? TripId { get; set; }
        public int? CustomerId { get; set; }
        public bool? Shared { get; set; }
        public float? StartKm { get; set; }
        public float? EndKm { get; set; }
        public string StartGps { get; set; }
        public string EndGps { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public float? CustomerKm { get; set; }
        public int? SharedTripId { get; set; }
        public int? TripStatus { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsCancelled { get; set; }
        public string EndAddress { get; set; }
        public string StartAddress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? SharedKm { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool? Billable { get; set; }
        public bool? ConvertedToBillable { get; set; }
        public string CustomerTripDescription { get; set; }
        public float? SharedStartKm { get; set; }
        public float? SharedEndKm { get; set; }
        public string ChangedHistory { get; set; }
        public bool? OnHold { get; set; }
        public float? PauseKm { get; set; }
        public float? ResumeKm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual HistoricalDriverTrip Trip { get; set; }
    }
}
