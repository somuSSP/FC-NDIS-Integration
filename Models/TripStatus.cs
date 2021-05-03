using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class TripStatus
    {
        public TripStatus()
        {
            CustomerTrips = new HashSet<CustomerTrip>();
            Trips = new HashSet<Trip>();
        }

        public int TripStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<CustomerTrip> CustomerTrips { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
