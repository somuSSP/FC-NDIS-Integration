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
            var request = new RestRequest(Method.GET);
            request.AddHeader("ClientID", ClientID);
            request.AddHeader("UserID", UserID);
            request.AddHeader("Token", Token);
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
                    catch(Exception ex)
                    {
                        logger.Info(ex.ToString()+", Ids :"+ vsRes.ID.ToString() + ", vsRes.LicensePlate:"+ vsRes.LicensePlate.ToString());
                    }
                }
                DBAction dba = new DBAction(_integrationAppSettings);
                dba.IntegrateAssetsintoDB(vehicles);
            }
        }

        public int AssetType(string GUID, string ClientID, string UserID, string Token)
        {
            int result = 0;
            var client = new RestClient( "https://hosted.fleetcomplete.com.au/Integration/v8_5_0/GPS/Asset/" + GUID);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("ClientID", ClientID);
            request.AddHeader("UserID", UserID);
            request.AddHeader("Token", Token);
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

        public bool PostResource(string ClientID, string UserID, string Token,int ResourceId)
        {
            bool result = false;
            try
            {
                DBAction dba = new DBAction(_integrationAppSettings);
                logger.Info("Scheduled Fleet complete Post Method - Resource");
                var client = new RestClient(_integrationAppSettings.ResourcePost);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("ClientID", ClientID);
                request.AddHeader("UserID", UserID);
                request.AddHeader("Token", Token);
                var drivers = dba.GetDriverInformation(ResourceId);
                Resource Resource = new Resource();
                Resource.Description= drivers.EmployeeCode +" "+(drivers.PreferedName!=null? drivers.PreferedName: drivers.FirstName)+""+drivers.LastName;
                //Need to Map the Model and convert to Json Format
                var json = JsonConvert.SerializeObject(Resource);
                var body = json;
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool PutResource(string ClientID, string UserID, string Token,int ResourceId)
        {
            bool result = false;
            try
            {
                logger.Info("Scheduled Fleet complete Put Method for Resource");
                var client = new RestClient(_integrationAppSettings.ResourcePut.Replace("{id}", ResourceId.ToString()).ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("ClientID", ClientID);
                request.AddHeader("UserID", UserID);
                request.AddHeader("Token", Token);
                Resource Resource = new Resource();

                //Need to Map the Model and convert to Json Format
                var json = JsonConvert.SerializeObject(Resource);
                var body = json;
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
