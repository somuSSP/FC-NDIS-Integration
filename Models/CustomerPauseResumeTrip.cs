using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CustomerPauseResumeTrip
    {
        public int? CustomerTripId { get; set; }
        public bool? IsPaused { get; set; }
        public DateTime? Time { get; set; }
        public string Gps { get; set; }

        public virtual CustomerTrip CustomerTrip { get; set; }
    }
}
