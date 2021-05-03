using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CustomerStatus
    {
        public CustomerStatus()
        {
            Customers = new HashSet<Customer>();
        }

        public int CustomerStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
