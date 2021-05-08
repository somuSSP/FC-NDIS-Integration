using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class TripType
    {
        public TripType()
        {
            BillingLines = new HashSet<BillingLine>();
            Trips = new HashSet<Trip>();
        }

        public int TripTypeId { get; set; }
        public string Description { get; set; }
        public bool? CustomerRequired { get; set; }
        public int VehicleType { get; set; }
        public bool? BillableToSalesForce { get; set; }
        public int CostCenterType { get; set; }
        public int? DefaultCostCenter { get; set; }
        public bool? TripReason { get; set; }
        public bool? ISdeleted { get; set; }
        public bool? Onboard { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CostCentre DefaultCostCenterNavigation { get; set; }
        public virtual ICollection<BillingLine> BillingLines { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
