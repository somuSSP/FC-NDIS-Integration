using FC_NDIS.ApplicationIntegartionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnxServiceReference;
using FC_NDIS.Action;

using NLog;
using Microsoft.Extensions.Logging;

namespace FC_NDIS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnXIntegrationController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IntegrationAppSettings _integrationAppSettings;

        public ConnXIntegrationController(IntegrationAppSettings integrationAppSettings, ILogger<ConnXIntegrationController> logger)
        {
            this._integrationAppSettings = integrationAppSettings;
            this._logger = logger;
        }

        [HttpGet("IntegerateDrivers")]
        public Response Get()
        {
            Response rs = new Response();
            try
            {
                _logger.LogInformation("IntegrateDrivers");
                //Validate the Environment 
                ConnexServiceAction csa = new ConnexServiceAction(_integrationAppSettings);
                csa.IntegrateDriverDetails(_integrationAppSettings.ConnexUserName, _integrationAppSettings.ConnexUserPassword);
                rs.ResponseCode = 200;
                rs.Message = "Integrated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 500;
                rs.Message = "Internal Sever error occued";
            }
            return rs;
        }

        [HttpGet("GetResource")]
        public Response IntegerateResource_Get()
        {
            Response rs = new Response();
            try
            {
                _logger.LogInformation("IntegerateResource_Get");
                var url = "https://hosted.fleetcomplete.com.au/Authentication/v9/Authentication.svc/authenticate/user?clientId=" + 46135 + "&userLogin=" + _integrationAppSettings.UserName + "&userPassword=" + _integrationAppSettings.Password;
                FleetCompleteAction fca = new FleetCompleteAction(_integrationAppSettings);
                _logger.LogInformation("IntegerateAssets Appsettings:" + _integrationAppSettings.AppConnection.ToString());

                var tokeninfo = fca.GetAccessToken(url);
                _logger.LogInformation("IntegerateAssets Token:" + tokeninfo.Token);
                if (fca.GetandUpdateResourcetoDB(_integrationAppSettings.ClientID, tokeninfo.UserId, tokeninfo.Token))
                {
                    rs.ResponseCode = 200;
                    rs.Message = "Integrated Successfully";
                }
                else
                {
                    rs.ResponseCode = 500;
                    rs.Message = "Internal Server Error Occured";
                }
                rs.ResponseCode = 200;
                rs.Message = "Integrated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 500;
                rs.Message = "Internal Server Error Occured";
            }
            return rs;
        }

        [HttpPost("PostResource")]
        public Response IntegerateResource_Post(List<int> ResourceIds)
        {
            Response rs = new Response();
            try
            {
                _logger.LogInformation("IntegerateResource_Post");
                var url = "https://hosted.fleetcomplete.com.au/Authentication/v9/Authentication.svc/authenticate/user?clientId=" + 46135 + "&userLogin=" + _integrationAppSettings.UserName + "&userPassword=" + _integrationAppSettings.Password;
                FleetCompleteAction fca = new FleetCompleteAction(_integrationAppSettings);
                _logger.LogInformation("IntegerateAssets Appsettings:" + _integrationAppSettings.AppConnection.ToString());

                var tokeninfo = fca.GetAccessToken(url);
                _logger.LogInformation("IntegerateAssets Token:" + tokeninfo.Token);
                if (fca.PostResource(_integrationAppSettings.ClientID, tokeninfo.UserId, tokeninfo.Token, ResourceIds))
                {
                    rs.ResponseCode = 200;
                    rs.Message = "Integrated Successfully";
                }
                else
                {
                    rs.ResponseCode = 500;
                    rs.Message = "Internal Server Error Occured";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 500;
                rs.Message = "Internal Server Error Occured";
            }
            return rs;
        }

        [HttpPut("PutResource")]
        public Response IntegerateResource_Put(List<int> ResourceIds)
        {
            Response rs = new Response();
            try
            {
                _logger.LogInformation("IntegerateResource_Put");
                var url = "https://hosted.fleetcomplete.com.au/Authentication/v9/Authentication.svc/authenticate/user?clientId=" + 46135 + "&userLogin=" + _integrationAppSettings.UserName + "&userPassword=" + _integrationAppSettings.Password;
                FleetCompleteAction fca = new FleetCompleteAction(_integrationAppSettings);
                _logger.LogInformation("IntegerateAssets Appsettings:" + _integrationAppSettings.AppConnection.ToString());

                var tokeninfo = fca.GetAccessToken(url);
                _logger.LogInformation("IntegerateAssets Token:" + tokeninfo.Token);
                if (fca.PutResource(_integrationAppSettings.ClientID, tokeninfo.UserId, tokeninfo.Token, ResourceIds))
                {
                    rs.ResponseCode = 200;
                    rs.Message = "Integrated Successfully";
                }
                else
                {
                    rs.ResponseCode = 500;
                    rs.Message = "Internal Server Error Occured";
                }
                rs.ResponseCode = 200;
                rs.Message = "Integrated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 500;
                rs.Message = "Internal Server Error Occured";
            }
            return rs;
        }
    }
}
