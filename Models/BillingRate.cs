using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class BillingRate
    {
        public int RateId { get; set; }
        public int VehicleCategory { get; set; }
        public float Rate { get; set; }
        public int UnitOfMeasure { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
