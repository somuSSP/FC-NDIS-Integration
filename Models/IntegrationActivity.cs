using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class IntegrationActivity
    {
        public int IntegrationActivityId { get; set; }
        public int? IntegrationActivityName { get; set; }
        public string Username { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? Status { get; set; }
        public string FailedException { get; set; }
        public bool? IsScheduled { get; set; }

        public virtual IntegrationActivityList IntegrationActivityNameNavigation { get; set; }
        public virtual IntegrationActivityStatus StatusNavigation { get; set; }
    }
}
