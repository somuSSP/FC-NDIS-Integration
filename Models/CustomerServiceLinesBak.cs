using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CustomerServiceLinesBak
    {
        public string ServiceAgreementCustomerId { get; set; }
        public string ServiceAgreementId { get; set; }
        public string ServiceAgreementName { get; set; }
        public DateTime ServiceAgreementEndDate { get; set; }
        public string ServiceAgreementStatus { get; set; }
        public string ServiceAgreementFundingManagement { get; set; }
        public string ServiceAgreementFundingType { get; set; }
        public string ServiceAgreementItemId { get; set; }
        public string ServiceAgreementItemName { get; set; }
        public float SupportCategoryAmount { get; set; }
        public string SupportCategoryDelivered { get; set; }
        public float? FundsRemaining { get; set; }
        public bool? ItemOverclaim { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteServiceProgramId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string RateId { get; set; }
        public string RateName { get; set; }
        public string RateType { get; set; }
        public float? RateAmount { get; set; }
        public bool? AllowRateNegotiation { get; set; }
    }
}
