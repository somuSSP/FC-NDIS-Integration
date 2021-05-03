using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CostCentresBak
    {
        public string CostCentre { get; set; }
        public string Description { get; set; }
        public bool? ISdeleted { get; set; }
        public int CostCentreId { get; set; }
        public string BusinessUnit { get; set; }
        public string ConnXcc { get; set; }
    }
}
