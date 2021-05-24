using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class RoleGroup
    {
        public RoleGroup()
        {
            UserRoleNews = new HashSet<UserRoleNew>();
        }

        public int RoleGroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<UserRoleNew> UserRoleNews { get; set; }
    }
}
