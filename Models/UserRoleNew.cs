using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class UserRoleNew
    {
        public int UserRoleId { get; set; }
        public int? UserId { get; set; }
        public int? RoleGroupId { get; set; }
        public int? PageNameId { get; set; }
        public int? AccessLevelId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual RoleAccessLevel AccessLevel { get; set; }
        public virtual GridList PageName { get; set; }
        public virtual RoleGroup RoleGroup { get; set; }
    }
}
