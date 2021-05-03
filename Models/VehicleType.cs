using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Trips = new HashSet<Trip>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int VehicleTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
