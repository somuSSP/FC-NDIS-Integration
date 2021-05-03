using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class DriverPauseResumeTrip
    {
        public int? TripId { get; set; }
        public bool? IsPaused { get; set; }
        public DateTime? Time { get; set; }
        public string Gps { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
