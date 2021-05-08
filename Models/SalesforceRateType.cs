using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class SalesforceRateType
    {
        public SalesforceRateType()
        {
            SalesforceRates = new HashSet<SalesforceRate>();
        }

        public int SalesforceRateTypeId { get; set; }
        public string RateType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<SalesforceRate> SalesforceRates { get; set; }
    }
}
