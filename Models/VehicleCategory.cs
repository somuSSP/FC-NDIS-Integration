using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class VehicleCategory
    {
        public VehicleCategory()
        {
            Trips = new HashSet<Trip>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int VehicleCategoryId { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
