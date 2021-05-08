using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CostCentre
    {
        public CostCentre()
        {
            BillingLines = new HashSet<BillingLine>();
            ConnxCcs = new HashSet<ConnxCc>();
            Drivers = new HashSet<Driver>();
            TripTypes = new HashSet<TripType>();
            Trips = new HashSet<Trip>();
        }

        public string CostCentre1 { get; set; }
        public string Description { get; set; }
        public bool? ISdeleted { get; set; }
        public int CostCentreId { get; set; }
        public string BusinessUnit { get; set; }
        public string ConnXcc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<BillingLine> BillingLines { get; set; }
        public virtual ICollection<ConnxCc> ConnxCcs { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<TripType> TripTypes { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
