using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class Customer
    {
        public Customer()
        {
            BillingLines = new HashSet<BillingLine>();
            CustomerServiceLines = new HashSet<CustomerServiceLine>();
            CustomerTrips = new HashSet<CustomerTrip>();
            SharedTrips = new HashSet<SharedTrip>();
        }

        public int CustId { get; set; }
        public string CustomerId { get; set; }
        public string LumaryId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int? Status { get; set; }
        public bool? Active { get; set; }
        public bool? OnHold { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CustomerStatus StatusNavigation { get; set; }
        public virtual ICollection<BillingLine> BillingLines { get; set; }
        public virtual ICollection<CustomerServiceLine> CustomerServiceLines { get; set; }
        public virtual ICollection<CustomerTrip> CustomerTrips { get; set; }
        public virtual ICollection<SharedTrip> SharedTrips { get; set; }
    }
}
