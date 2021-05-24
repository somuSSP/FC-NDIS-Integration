using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class RoleAccessLevel
    {
        public RoleAccessLevel()
        {
            UserRoleNews = new HashSet<UserRoleNew>();
        }

        public int AccessLevelId { get; set; }
        public string AccessLevelName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<UserRoleNew> UserRoleNews { get; set; }
    }
}
