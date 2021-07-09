using FC_NDIS.Action;
using FC_NDIS.ApplicationIntegartionModels;
using FC_NDIS.DBAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FCIntegrationController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IntegrationAppSettings _integrationAppSettings;

        public FCIntegrationController(IntegrationAppSettings integrationAppSettings, ILogger<FCIntegrationController> logger)
        {
            this._integrationAppSettings = integrationAppSettings;
            this._logger = logger;
        }
        [HttpGet("IntegerateAssets")]
        public Response Get()
        {
            Response rs = new Response();
            try
            {
                _logger.LogInformation("IntegerateAssets");

                var url = "https://hosted.fleetcomplete.com.au/Authentication/v9/Authentication.svc/authenticate/user?clientId="+ 46135 + "&userLogin="+ _integrationAppSettings .UserName+ "&userPassword="+ _integrationAppSettings.Password;
               FleetCompleteAction fca = new FleetCompleteAction(_integrationAppSettings);
                _logger.LogInformation("IntegerateAssets Appsettings:"+ _integrationAppSettings.AppConnection.ToString());

                var tokeninfo = fca.GetAccessToken(url);
                _logger.LogInformation("IntegerateAssets Token:" + tokeninfo.Token);
                fca.IntegrateAsset(_integrationAppSettings.ClientID, tokeninfo.UserId, tokeninfo.Token);
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
