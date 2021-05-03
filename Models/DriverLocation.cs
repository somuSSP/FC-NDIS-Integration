using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class DriverLocation
    {
        public int LocationId { get; set; }
        public int? TripId { get; set; }
        public string DriverId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
