using FC_NDIS.ActionInterface;
using FC_NDIS.ApplicationIntegartionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.Extensions.Configuration;
using FC_NDIS.Models;
using FC_NDIS.DBAccess;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using FC_NDIS.JsonModels;
using NLog;
using FC_NDIS.JsonModels.Resource;
using FC_NDIS.APIModels.FCModel;

namespace FC_NDIS.Action
{
    public class FleetCompleteAction : IFleetComplete
    {
        public string URL = "";
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IntegrationAppSettings _integrationAppSettings;
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public FleetCompleteAction(IntegrationAppSettings integrationAppSettings)
        {
            this._integrationAppSettings = integrationAppSettings;
        }

        public FCResponseTokenandUserId GetAccessToken(string URL)
        {
            FCResponseTokenandUserId sfdResponse = new FCResponseTokenandUserId();
            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);
            client.Timeout = -1;
            IRestResponse response = client.Execute(request);
            FCResponse FCResponse = new FCResponse();
            FCResponseInfo obj = JsonConvert.DeserializeObject<FCResponseInfo>(response.Content);
            sfdResponse.Token = obj.Token;
            sfdResponse.UserId = obj.UserID;
            return sfdResponse;
        }

        public void IntegrateAsset(string ClientID, string UserID, string Token)
        {
            logger.Info("Scheduled Fleet complete asset job triggered");
            var client = new RestClient(_integrationAppSettings.AssetURL);
            client.Timeout = -1;
            var request = RestRequestMapping((int)Method.GET, ClientID, UserID, Token);
            IRestResponse response = client.Execute(request);
            AssetResp ASResp = new AssetResp();
            ASResp = JsonConvert.DeserializeObject<AssetResp>(response.Content);
            List<Vehicle> vehicles = new List<Vehicle>();
            if (ASResp.Errors == null)
            {
                foreach (var vsRes in ASResp.Data)
                {
                    try
                    {
                        Vehicle vh = new Vehicle();
                        vh.AssetId = vsRes.ID;
                        vh.Registration = vsRes.LicensePlate;
                        vh.Make = vsRes.Make;
                        vh.Model = vsRes.Model;
                        string GuidID = vsRes.ID;
                        var CatType = AssetType(GuidID, ClientID, UserID, Token);
                        if (CatType != 0)
                            vh.Category = CatType;
                        if (vsRes.AssetType != null)
                        {
                            vh.Type = (vsRes.AssetType.ToString().Contains("Fleet")) ? 1 : 2;
                            vh.Active = (vsRes.AssetType.ToString().Contains("Replaced")) ? false : true;
                        }
                        else
                        {
                            vh.Active = true;
                            vh.Type = 1;
                        }
                        vh.Description = vsRes.Description;
                        vh.Availability = true;
                        vh.CreatedDate = DateTime.Now;
                        vh.ModifiedDate = DateTime.Now;
                        if (vsRes.IsDeleted != null && vsRes.IsDeleted != true)
                            vehicles.Add(vh);
                    }
                    catch (Exception ex)
                    {
                        logger.Info(ex.ToString() + ", Ids :" + vsRes.ID.ToString() + ", vsRes.LicensePlate:" + vsRes.LicensePlate.ToString());
                    }
                }
                DBAction dba = new DBAction(_integrationAppSettings);
                dba.IntegrateAssetsintoDB(vehicles);
            }
        }

        public int AssetType(string GUID, string ClientID, string UserID, string Token)
        {
            int result = 0;
            var client = new RestClient("https://hosted.fleetcomplete.com.au/Integration/v8_5_0/GPS/Asset/" + GUID);
            client.Timeout = -1;
            var request = RestRequestMapping((int)Method.GET, ClientID, UserID, Token);


            IRestResponse response = client.Execute(request);
            Root vehicleInformation = JsonConvert.DeserializeObject<Root>(response.Content);
            if (vehicleInformation != null)
            {
                if (vehicleInformation.Data.AssetType.Description == "Modified Vehicle")
                {
                    result = 1;
                }
                else
                {
                    result = 2;
                }
            }
            else
            {
                result = 1;
            }

            return result;
        }

        public bool PostResource(string ClientID, string UserID, string Token)
        {
            bool result = false;
            try
            {
                DBAction dba = new DBAction(_integrationAppSettings);
                logger.Info("Scheduled Fleet complete Post Method - Resource");

                var allDrivers = dba.GetAllDriverInformation();
                var NewRecords = allDrivers.Where(k => string.IsNullOrEmpty(k.FCResourceID) && !string.IsNullOrEmpty(k.RFID)).ToList();
                var CreatedRecords = allDrivers.Where(k => !string.IsNullOrEmpty(k.FCResourceID) && !string.IsNullOrEmpty(k.RFID)).ToList();

                if (NewRecords.Count > 0)
                {
                    var client = new RestClient(_integrationAppSettings.ResourcePost);
                    var request = RestRequestMapping((int)Method.POST, ClientID, UserID, Token);
                    client.Timeout = -1;
                    foreach (var drivers in NewRecords) //All Record will push to FC
                    {
                        if (drivers.FCResourceID != null)
                        {
                            client = new RestClient(_integrationAppSettings.ResourcePut.Replace("{id}", drivers.FCResourceID.ToString()).ToString());
                        }

                        var resource = MappingResourceDTO(drivers, 1);
                        try
                        {
                            var json = JsonConvert.SerializeObject(resource);
                            var body = json;
                            request.AddParameter("application/json", body, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            root resourceResponse = JsonConvert.DeserializeObject<root>(response.Content);
                            if (resourceResponse.Errors == null)
                            {
                                result = dba.UpdatedInformation(drivers.DriverId, resourceResponse.Data);
                            }
                            else
                            {
                                logger.Error("FC Insert Record Status:" + resourceResponse.Errors.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.ToString());
                        }
                    }
                }
                if (CreatedRecords.Count > 0) //Update the records
                {
                    try
                    {
                        var request = RestRequestMapping((int)Method.PUT, ClientID, UserID, Token);
                        foreach (var drivers in CreatedRecords)
                        {
                            if (drivers.FCResourceID != null)
                            {
                                var client = new RestClient(_integrationAppSettings.ResourcePut.Replace("{id}", drivers.FCResourceID.ToString()));
                                client.Timeout = -1;
                                var resource = MappingResourceDTO(drivers, 0);
                                var json = JsonConvert.SerializeObject(resource);
                                var body = json;
                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                client = new RestClient(_integrationAppSettings.ResourcePut.Replace("{id}", drivers.FCResourceID.ToString()).ToString());
                                client.Timeout = -1;
                                IRestResponse response = client.Execute(request);
                                root resourceResponse = JsonConvert.DeserializeObject<root>(response.Content);
                                if (resourceResponse != null)
                                {
                                    if (resourceResponse.Errors == null)
                                    {
                                        result = dba.UpdatedInformation(drivers.DriverId, resourceResponse.Data);
                                    }
                                    else
                                    {
                                        logger.Error("FC Update Record Status:" + resourceResponse.Errors.ToString());
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                    }
                }


                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool PutResource(string ClientID, string UserID, string Token, List<int> ResourceIds)
        {
            bool result = false;

            DBAction dba = new DBAction(_integrationAppSettings);
            logger.Info("Scheduled Fleet complete Put Method for Resource");

            var request = RestRequestMapping((int)Method.PUT, ClientID, UserID, Token);
            foreach (var ResourceId in ResourceIds)
            {
                var drivers = dba.GetDriverInformation(ResourceId);
                var resource = MappingResourceDTO(drivers, 0);
                var client = new RestClient(_integrationAppSettings.ResourcePut.Replace("{id}", drivers.FCResourceID.ToString()));
                client.Timeout = -1;
                var json = JsonConvert.SerializeObject(resource);
                var body = json;
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                try
                {
                    IRestResponse response = client.Execute(request);
                    root resourceResponse = JsonConvert.DeserializeObject<root>(response.Content);
                    if (resourceResponse != null)
                    {
                        if (resourceResponse.Errors == null)
                        {
                            result = dba.UpdatedInformation(drivers.DriverId, resourceResponse.Data);
                        }
                        else
                        {
                            logger.Error("FC Update Record Status:" + resourceResponse.Errors.ToString());
                        }
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                    result = false;
                }
            }
            return result;
        }

        public bool GetandUpdateResourcetoDB(string ClientID, string UserID, string Token)
        {
            bool result = false;

            DBAction dba = new DBAction(_integrationAppSettings);
            logger.Info("Scheduled Fleet complete Put Method for Resource");
            var client = new RestClient(_integrationAppSettings.ResourcePost + "?top=1000");
            client.Timeout = -1;
            var request = RestRequestMapping((int)Method.GET, ClientID, UserID, Token);
            FCResourceModel resourcechildResponse = new FCResourceModel();
            Dictionary<string, string> FCList = new Dictionary<string, string>();
            Dictionary<string, string> FCOriginalList = new Dictionary<string, string>();
            try
            {
                IRestResponse response = client.Execute(request);
                OutputResource resourceResponse = JsonConvert.DeserializeObject<OutputResource>(response.Content);

                if (resourceResponse.Errors == null)
                {
                    foreach (var resourceInp in resourceResponse.Data)
                    {
                        var EmployeeCode = resourceInp.DriverName;
                        string[] EmployeeCodes = EmployeeCode.Split(null);
                        var idstring = EmployeeCodes[0].Replace("[", "").Replace("]", "").Trim();
                        if (!FCList.ContainsKey(idstring))
                            FCList.Add(idstring, resourceInp.ID);
                    }
                }

                foreach (var k in FCList)
                {
                    var url = _integrationAppSettings.ResourceGetDetails.Replace("{id}", k.Value);
                    var clientchild = new RestClient(url);
                    clientchild.Timeout = -1;
                    var request1 = RestRequestMapping((int)Method.GET, ClientID, UserID, Token);
                    IRestResponse responseChild = clientchild.Execute(request1);
                    resourcechildResponse = JsonConvert.DeserializeObject<FCResourceModel>(responseChild.Content);
                    if (resourcechildResponse != null)
                    {
                        if (!FCOriginalList.Keys.Contains(k.Value)) FCOriginalList.Add(k.Value, resourcechildResponse.Data.WorkInfo.MobileID);
                    }
                }

                result = dba.UpdatedFCInformationintoDB(FCList, FCOriginalList);
                result = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = false;
            }

            return result;

        }
        public Resource MappingResourceDTO(Driver drivers, int Methodtype)
        {
            Resource resource = new Resource();

            if (!string.IsNullOrEmpty(drivers.FCResourceID))
                if (drivers.FCResourceID != null)
                    resource.ID = Guid.Parse(drivers.FCResourceID);
            resource.Description = "[" + drivers.EmployeeCode + "] " + (drivers.PreferedName != null ? drivers.PreferedName : drivers.FirstName) + " " + drivers.LastName;
            resource.Description = resource.Description + " FC Test";
            resource.Code = "";
            resource.IsActive = true;
            resource.IsSettlementOnly = false;
            resource.IsSuspended = false;
            resource.IsShared = true;

            resource.AssignedBranchID = "01b8eba6-64be-48f6-841e-ef1988bfc9f7";
            resource.Branch = new JsonModels.Resource.Branch();
            resource.Branch.ID = "01b8eba6-64be-48f6-841e-ef1988bfc9f7";
            resource.Branch.Description = "Ability Centre";

            resource.Details = new Details();
            resource.Details.LanguagePreference = "";
            resource.Details.Company = "";
            resource.Details.IsStrongBox = true;
            resource.Details.OutsideCode = "";
            resource.Details.InsideCode = "";
            if (Methodtype == 1)
            {
                resource.Details.Communication = new Communication
                {
                    Email = drivers.Username,
                    Phone = "",
                    Mobile = "",
                    Pager = "",
                    Fax = "",
                    MobileEmail = drivers.Username,
                    MailingAddress = "",
                    CommunicationMethod = 0
                };
                resource.Details.WorkInfo = new WorkInfo()
                {
                    WorkStatus = 0,
                    IsCrewChief = true,
                    EmploymentStartDate = DateTime.Now,
                    EmploymentEndDate = DateTime.Now,
                    DispatchDeviceID = "",
                    PTTNumber = "",
                    MobileID = drivers.RFID,
                    PIN = ""
                };
            }
            //Insurance null
            resource.Details.License = new License
            {
                IsMale = true,
                Class = "",
                ProvinceCode = "",
                IssueDate = DateTime.Now,
                ExpiryDate = DateTime.Now
            };



            resource.Details.ResourceTypeID = "10b48e80-6a5c-48c7-9604-5b5a8376be87";// Need to cross check
            resource.Details.Assets = new List<Asset>();
            resource.Details.ResourceType = new ResourceType()
            {
                ID = "10b48e80-6a5c-48c7-9604-5b5a8376be87",
                Description = "",
                ResourceCount = 1
            };
            resource.Details.CustomFields = new List<CustomField>();
            //HOS is null
            resource.Details.WorkSchedule = new WorkSchedules()
            {
                IsSundayActive = true,
                IsMondayActive = true,
                IsTuesdayActive = true,
                IsWednesdayActive = true,
                IsThursdayActive = true,
                IsFridayActive = true,
                IsSaturdayActive = true,
                ID = Guid.NewGuid().ToString(),
                Description = ""
            };

            resource.Details.License = new License();
            // resource.Details.License.IssueDate = DateTime.Now.AddYears(10);
            // resource.Details.License.ExpiryDate = DateTime.Now.AddDays(10);
            resource.Details.License.IsMale = true;
            resource.Details.License.Class = "";
            resource.Details.License.ProvinceCode = "";

            return resource;

        }

        public RestRequest RestRequestMapping(int Type, string ClientID, string UserID, string Token)
        {
            var request = new RestRequest(((Method)Type));
            request.AddHeader("ClientID", ClientID);
            request.AddHeader("UserID", UserID);
            request.AddHeader("Token", Token);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddHeader("Content-Type", "application/json");
            return request;
        }
    }
}
