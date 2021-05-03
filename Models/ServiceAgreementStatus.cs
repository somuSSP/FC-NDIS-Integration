using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class ServiceAgreementStatus
    {
        public ServiceAgreementStatus()
        {
            CustomerServiceLines = new HashSet<CustomerServiceLine>();
        }

        public int SastatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<CustomerServiceLine> CustomerServiceLines { get; set; }
    }
}
