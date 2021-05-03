using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.JsonModels
{ 


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class VehicleDetails
    {
        [JsonIgnore]
        public bool? IsEngineDisabled { get; set; }
        public double? FuelTankOneCapacity { get; set; }
        public double? FuelTankTwoCapacity { get; set; }
        [JsonIgnore]
        public bool? IsFuelTankCapacityMetric { get; set; }
        public double? FuelEconomyManufacturer { get; set; }
        [JsonIgnore]
        public bool? IsFuelEconomyManufacturerMetric { get; set; }
        public double? FuelEconomy { get; set; }
        [JsonIgnore]
        public bool? IsFuelEconomyMetric { get; set; }
        public string VIN { get; set; }
        public string LicensePlate { get; set; }
        [JsonIgnore]
        public bool? IsOdometerMetric { get; set; }
        public int? Odometer { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int? Year { get; set; }
        public int? OperatingSeconds { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public bool? IsHeavyDuty { get; set; }
    }

    public class ECM
    {
        public object EngineType { get; set; }
        public int? FuelType { get; set; }
        public double? EngineEfficiency { get; set; }
        public int? EngineDisplacement { get; set; }
    }

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
        public double? Direction { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; }
    }

    public class DeviceDataSetting
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Conditions
    {
        public int? AltDDS1Speed { get; set; }
        [JsonIgnore]
        public bool? IsAltDDS1SpeedIncluded { get; set; }
        [JsonIgnore]
        public bool? IsAltDDS1Input2Included { get; set; }
        [JsonIgnore]
        public bool? IsAltDDS1Input3Included { get; set; }
        public object AltDDS1Input2UnitID { get; set; }
        public object AltDDS1Input3UnitID { get; set; }
    }

    public class Settings
    {
        public int? AltDDS1Time { get; set; }
        public int? AltDDS1TimeUnit { get; set; }
    }

    public class AltDDS
    {
        public Conditions Conditions { get; set; }
        public Settings Settings { get; set; }
    }

    public class Satellite
    {
        [JsonIgnore]
        public bool? SFOEnabled { get; set; }
        public int? TransmitTime { get; set; }
        public int? TransmitTimeUnit { get; set; }
        [JsonIgnore]
        public bool? IsTransmitIgnitionOnOffEnabled { get; set; }
        [JsonIgnore]
        public bool? IsTransmitInputStateChangesEnabled { get; set; }
        [JsonIgnore]
        public bool? IsTransmitAlarmsEnabled { get; set; }
    }

    public class Advanced
    {
        public AltDDS AltDDS { get; set; }
        
        public Satellite Satellite { get; set; }
        public object AssetPoiAlert { get; set; }
    }

    public class MDT
    {
        [JsonIgnore]
        public bool? IsMDTEnabled { get; set; }
        public object MDTDeviceType { get; set; }
        public object CannedMessageGroupID { get; set; }
        public object CannedReplyGroupID { get; set; }
        public object MessageGroup { get; set; }
        public object ReplyGroup { get; set; }
    }

    public class WorkSchedule
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Branch
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class HomeBase
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class AssetType
    {
        public string MapImage { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Type
    {
        public int? ID { get; set; }
        public string Description { get; set; }
    }

    public class SensorUnit
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class DefaultStateUnit
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class PartnerUnit
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Sensor
    {
        public string PhysicalSensor { get; set; }
        public Type Type { get; set; }
        public SensorUnit SensorUnit { get; set; }
        public DefaultStateUnit DefaultStateUnit { get; set; }
        public PartnerUnit PartnerUnit { get; set; }
        public double? Value { get; set; }
        public string ValueDescription { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Rule
    {
        public string ID { get; set; }
        public Sensor Sensor { get; set; }
        public object OutputSensor { get; set; }
        public string Description { get; set; }
    }

    public class Sensor2
    {
        public string PhysicalSensor { get; set; }
        public Type Type { get; set; }
        public SensorUnit SensorUnit { get; set; }
        public DefaultStateUnit DefaultStateUnit { get; set; }
        public PartnerUnit PartnerUnit { get; set; }
        public double? Value { get; set; }
        public string ValueDescription { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Data
    {
        public string DeviceID { get; set; }
        [JsonIgnore]
        public bool? IsRecordOnDirectionChange { get; set; }
        [JsonIgnore]
        public DateTime LastUpdatedTimeStamp { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]
        public bool? IsCrashDetected { get; set; }
        public VehicleDetails VehicleDetails { get; set; }
        public ECM ECM { get; set; }
        public Position Position { get; set; }
        public string AssignedBranchID { get; set; }
        public string Zone { get; set; }
        public DeviceDataSetting DeviceDataSetting { get; set; }
        public Advanced Advanced { get; set; }
        public MDT MDT { get; set; }
        [JsonIgnore]
        public bool? AlternateDDSConfigured { get; set; }
        public object RelatedAssetInfo { get; set; }
        public object Run { get; set; }
        public WorkSchedule WorkSchedule { get; set; }
        public Branch Branch { get; set; }
        public HomeBase HomeBase { get; set; }
        public object VehicleType { get; set; }
        public object Resource { get; set; }
        public AssetType AssetType { get; set; }
        public List<Rule> Rules { get; set; }
        public object CustomFields { get; set; }
        public List<Sensor> Sensors { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class Root
    {
        public Data Data { get; set; }
        public int? StatusCode { get; set; }
        public object Errors { get; set; }
    }

}
