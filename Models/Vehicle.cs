using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Trips = new HashSet<Trip>();
        }

        public string Registration { get; set; }
        public int Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Category { get; set; }
        public bool? Active { get; set; }
        public bool? Availability { get; set; }
        public int? DriverId { get; set; }
        public int VehicleId { get; set; }

        public virtual VehicleCategory CategoryNavigation { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual VehicleType TypeNavigation { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
