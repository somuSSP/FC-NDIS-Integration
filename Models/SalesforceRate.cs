using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class SalesforceRate
    {
        public SalesforceRate()
        {
            BillingLinesNews = new HashSet<BillingLinesNew>();
        }

        public int SalesforceRatesId { get; set; }
        public string RateId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RateName { get; set; }
        public string ServiceId { get; set; }
        public string Negotiation { get; set; }
        public float? Rate { get; set; }
        public int? RateType { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string PostalCode { get; set; }

        public virtual SalesforceRateType RateTypeNavigation { get; set; }
        public virtual ICollection<BillingLinesNew> BillingLinesNews { get; set; }
    }
}
