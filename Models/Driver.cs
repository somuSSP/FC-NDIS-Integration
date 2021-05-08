using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class Driver
    {
        public Driver()
        {
            BillingLines = new HashSet<BillingLine>();
            Trips = new HashSet<Trip>();
            Vehicles = new HashSet<Vehicle>();
        }

        public string EmployeeCode { get; set; }
        public string SalesForceUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CostCenter { get; set; }
        public string Username { get; set; }
        public bool? Disabled { get; set; }
        public int Type { get; set; }
        public string JobDescription { get; set; }
        public string Department { get; set; }
        public string ManagerName { get; set; }
        public string Password { get; set; }
        public bool? IsPortalUser { get; set; }
        public int? JobNumber { get; set; }
        public string PreferedName { get; set; }
        public int DriverId { get; set; }
        public int? Otp { get; set; }
        public bool? IsTerminated { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CostCentre CostCenterNavigation { get; set; }
        public virtual ICollection<BillingLine> BillingLines { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
