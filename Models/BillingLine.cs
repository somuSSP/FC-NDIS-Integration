using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class BillingLine
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
        public int? DriverId { get; set; }
        public int? CustomerId { get; set; }
        public int? TripTypeId { get; set; }
        public string CustomerTripDescription { get; set; }
        public float? CustomerTripDistance { get; set; }
        public float? CustomerTripSharedDistance { get; set; }
        public TimeSpan? CustomerTripDuration { get; set; }
        public int? CostCentre { get; set; }
        public float? Cost { get; set; }
        public float? BlendedRate { get; set; }
        public int? UserId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string SalesForceUserId { get; set; }
        public bool? Validated { get; set; }
        public DateTime? ValidatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ChangedHistory { get; set; }
        public string VehicleRegistrationNumber { get; set; }

        public virtual CostCentre CostCentreNavigation { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerTrip CustomerTrip { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual TripType TripType { get; set; }
    }
}
