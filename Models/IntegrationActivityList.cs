using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class IntegrationActivityList
    {
        public IntegrationActivityList()
        {
            IntegrationActivities = new HashSet<IntegrationActivity>();
        }

        public int IntegrationActivityNameId { get; set; }
        public string IntegrationActivityName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCompleted { get; set; }

        public virtual ICollection<IntegrationActivity> IntegrationActivities { get; set; }
    }
}
