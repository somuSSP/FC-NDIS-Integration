using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class GridSetting
    {
        public int GridSettingId { get; set; }
        public int? UserId { get; set; }
        public int? GridId { get; set; }
        public string GridOrderSetting { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual GridList Grid { get; set; }
    }
}
