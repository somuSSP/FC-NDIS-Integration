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
                if (csa.validator(_integrationAppSettings.ConnexUserName, _integrationAppSettings.ConnexUserPassword, 0))
                {
                    csa.IntegrateDriverDetails(_integrationAppSettings.ConnexUserName, _integrationAppSettings.ConnexUserPassword);
                    rs.ResponseCode = 200;
                    rs.Message = "Integrated Successfully";
                }
                else
                {
                    rs.ResponseCode = 500;
                    rs.Message = "provide valid credential";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 500;
                rs.Message = "Internal Sever error occued";              
            }
            return rs;
        }
    }
}
