using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class BillingLinesNew
    {
        public int BillingId { get; set; }
        public int? TripId { get; set; }
        public string ServiceAgreementId { get; set; }
        public string ServiceAgreementItemId { get; set; }
        public float? Rate { get; set; }
        public int? UnitOfMeasure { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateTransferred { get; set; }
        public bool? Approved { get; set; }
        public int? CustomerTripId { get; set; }
        public bool? SentToSalesForce { get; set; }
        public float? Cost { get; set; }
        public float? BlendedRate { get; set; }
        public int? UserId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool? Validated { get; set; }
        public DateTime? ValidatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ChangedHistory { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Billable { get; set; }
        public string LumaryId { get; set; }
        public int? SalesforceRatesId { get; set; }
        public string SiteGlcode { get; set; }
        public bool? AllowNegotiation { get; set; }
        public bool? SentToSalesForceStatus { get; set; }
        public string SentToSalesForceDescription { get; set; }

        public virtual BillingCustomerTrip CustomerTrip { get; set; }
        public virtual SalesforceRate SalesforceRates { get; set; }
        public virtual BillingDriverTrip Trip { get; set; }
    }
}
