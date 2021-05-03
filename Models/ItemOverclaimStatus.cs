using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class ItemOverclaimStatus
    {
        public ItemOverclaimStatus()
        {
            CustomerServiceLines = new HashSet<CustomerServiceLine>();
        }

        public int ItemOverclaimStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<CustomerServiceLine> CustomerServiceLines { get; set; }
    }
}
