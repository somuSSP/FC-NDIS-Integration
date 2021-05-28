using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class TripCoordinatesArchive
    {
        public int Id { get; set; }
        public int? TripId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsProcess { get; set; }
        public float? Accuracy { get; set; }
        public string Description { get; set; }
    }
}
