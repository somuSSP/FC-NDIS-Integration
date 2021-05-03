using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class SharedTrip
    {
        public int? TripId { get; set; }
        public int? CustomerId { get; set; }
        public float? StartKm { get; set; }
        public float? EndKm { get; set; }
        public int? NoOfSharedCustomers { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string StartGps { get; set; }
        public string EndGps { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public int SharedTripId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
