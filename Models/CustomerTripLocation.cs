using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CustomerTripLocation
    {
        public int LocationId { get; set; }
        public int? CustomerTripId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
