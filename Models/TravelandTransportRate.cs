using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class TravelandTransportRate
    {
        public int TravelandTransportId { get; set; }
        public string Name { get; set; }
        public string NogotiationRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ServiceId { get; set; }
        public int? ExGstAmount { get; set; }
        public string QuantityType { get; set; }
        public string Id { get; set; }
    }
}
