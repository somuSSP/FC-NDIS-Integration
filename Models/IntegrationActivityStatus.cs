using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class IntegrationActivityStatus
    {
        public IntegrationActivityStatus()
        {
            IntegrationActivities = new HashSet<IntegrationActivity>();
        }

        public int IntegrationActivityStatusId { get; set; }
        public string IntegrationActivityStatus1 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<IntegrationActivity> IntegrationActivities { get; set; }
    }
}
