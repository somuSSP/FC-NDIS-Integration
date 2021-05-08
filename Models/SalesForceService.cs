using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class SalesForceService
    {
        public string ServiceId { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public int? UnitOfMeasure { get; set; }
        public int SalesForceServiceId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
