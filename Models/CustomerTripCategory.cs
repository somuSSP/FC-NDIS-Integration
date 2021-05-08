using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class CustomerTripCategory
    {
        public CustomerTripCategory()
        {
            CustomerTrips = new HashSet<CustomerTrip>();
        }

        public int CustomerTripCategoryId { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<CustomerTrip> CustomerTrips { get; set; }
    }
}
