using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class ConnxCcBak
    {
        public int ConnxCcid { get; set; }
        public string ConnxCcvalue { get; set; }
        public int? CostCentreId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
