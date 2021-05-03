using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.ApplicationIntegartionModels
{
    //Fleet complete Asset
    public class Position
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public int? Speed { get; set; }
        public int? SpeedLimit { get; set; }
        public int? SpeedLimitMI { get; set; }
        public Decimal? Direction { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
    public class Datum
    {
        public string DeviceID { get; set; }
        public int? Status { get; set; }
        public string LicensePlate { get; set; }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Manufacturer { get; set; }
        public bool? HasMDT { get; set; }
        public bool? IsCrashDetected { get; set; }
        public bool? IsSatellite { get; set; }
        public DateTime? LastUpdatedTimeStamp { get; set; }
        public bool? IsVisible { get; set; }
        public bool? AlternateDDSConfigured { get; set; }
        public bool? IsDeleted { get; set; }
        public Position Position { get; set; }
        public object Branch { get; set; }
        public object HomeBase { get; set; }
        public object AssetType { get; set; }
        public object WorkSchedule { get; set; }
        public object Resource { get; set; }
        public object Advanced { get; set; }
        public object Sensors { get; set; }
        public object CustomFields { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }
    class AssetResp
    {
        public List<Datum> Data { get; set; }
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }
}
