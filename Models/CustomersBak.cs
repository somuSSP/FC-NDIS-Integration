using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CustomersBak
    {
        public int CustId { get; set; }
        public string CustomerId { get; set; }
        public string LumaryId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int? Status { get; set; }
        public bool? Active { get; set; }
        public bool? OnHold { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
