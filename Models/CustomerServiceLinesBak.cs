using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CustomerServiceLinesBak
    {
        public int? ServiceAgreementCustomerId { get; set; }
        public string ServiceAgreementId { get; set; }
        public string ServiceAgreementName { get; set; }
        public DateTime ServiceAgreementEndDate { get; set; }
        public int? ServiceAgreementStatus { get; set; }
        public string ServiceAgreementFundingManagement { get; set; }
        public string ServiceAgreementFundingType { get; set; }
        public string ServiceAgreementItemId { get; set; }
        public string ServiceAgreementItemName { get; set; }
        public float SupportCategoryAmount { get; set; }
        public float? SupportCategoryDelivered { get; set; }
        public float? FundsRemaining { get; set; }
        public int? ItemOverclaim { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteServiceProgramId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool? AllowRateNegotiation { get; set; }
        public int CustomerServiceLineId { get; set; }
        public bool? Default { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string TravelServiceId { get; set; }
        public string TransportServiceId { get; set; }
        public string SiteGlcode { get; set; }
        public string CategoryItemId { get; set; }
    }
}
