using FC_NDIS.Action;
using FC_NDIS.ApplicationIntegartionModels;
using FC_NDIS.DBAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FC_NDIS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SFDCIntegrationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IntegrationAppSettings _integrationAppSettings;
        public SFDCIntegrationController(IntegrationAppSettings integrationAppSettings, ILogger<ConnXIntegrationController> logger)
        {
            this._integrationAppSettings = integrationAppSettings;
            this._logger = logger;
        }
        [HttpGet("IntegerateCustomerServiceLine")]
        public Response Get()
        {
            _logger.LogInformation("IntegerateCustomerServiceLine");

            Response rs = new Response();
            try
            {
                SFDCRestAPIAccess sfdca = new SFDCRestAPIAccess(_integrationAppSettings);
                var result = sfdca.IntegerateSfCustServiceLine();
                rs.ResponseCode = 200;
                rs.Message = "Integrated Successfully";
            }
            catch (Exception ex)
            {              
               _logger.LogError(ex.ToString());
                rs.ResponseCode = 200;
                rs.Message = "Internal Server Error Occur";
            }
            return rs;
        }


        [HttpGet("IntegerateTransportRate")]
        public Response TransportRateGet()
        {
            _logger.LogInformation("IntegerateTransportRate");
            Response rs = new Response();
            try
            {
                SFDCRestAPIAccess sfdca = new SFDCRestAPIAccess(_integrationAppSettings);
                var result = sfdca.IntegerateSfTransportRate();
                rs.ResponseCode = 200;
                rs.Message = "Integrated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 200;
                rs.Message = "Internal Server Error Occur";
            }
            return rs;
        }

        [HttpGet("IntegerateTraveltRate")]
        public Response TravelRateGet()
        {
            _logger.LogInformation("IntegerateTraveltRate");
            Response rs = new Response();
            try
            {
                SFDCRestAPIAccess sfdca = new SFDCRestAPIAccess(_integrationAppSettings);
                var result = sfdca.IntegerateSfTravelRate();
                rs.ResponseCode = 200;
                rs.Message = "Integrated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 200;
                rs.Message = "Internal Server Error Occur";
            }
            return rs;
        }


        [HttpGet("IntegerateCustomer")]
        public Response IntegerateSfCustomeList()
        {
            _logger.LogInformation("IntegerateCustomer");
            Response rs = new Response();
            try
            {
                SFDCRestAPIAccess sfdca = new SFDCRestAPIAccess(_integrationAppSettings);
                var result = sfdca.IntegerateSfCustomeList();
                rs.ResponseCode = 200;
                rs.Message = "Integrated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 200;
                rs.Message = "Internal Server Error Occur";
            }
            return rs;
        }
        [HttpGet("IntegerateSFDCIDtoDriver")]
        public Response IntegerateSfDCIDs()
        {
            _logger.LogInformation("IntegerateSFDCIDtoDriver");
            var result = false;
            Response rs = new Response();
            try
            {
                string userlist;
                List<string> UserNames = new List<string>();
                SFDCRestAPIAccess sfdca = new SFDCRestAPIAccess(_integrationAppSettings);
                UserNames = sfdca.GetAllDriverInfo_NotMappedSFDC();
                for (int i = 0; i < UserNames.Count; i = i + 100)
                {
                    var items = UserNames.Skip(i).Take(100);
                    userlist = "'" + string.Join("','", items.Where(k => !string.IsNullOrEmpty(k))) + "'";
                    result = sfdca.IntegrateSFDCId_OperatortoDB(userlist);
                    _logger.LogError("SFDC ID updated to driver From "+i.ToString()+" To:"+(i+ items.Count()).ToString());
                }                
                
                if (result)
                {
                    rs.ResponseCode = 200;
                    rs.Message = "Integrated Successfully";
                }
                else
                {
                    rs.ResponseCode = 500;
                    rs.Message = "Internal server error occured";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 200;
                rs.Message = "Internal Server Error Occur";
            }
            return rs;
        }

        [HttpPost("BillingLinesInfo_Pushed_to_SFDC")]
        public Response DataPushedtoSFDC()
        {
            Response rs = new Response();
            try
            {
                SFDCRestAPIAccess sfdca = new SFDCRestAPIAccess(_integrationAppSettings);
                var results = sfdca.InsertDataintoSFDC();               
                if (results)
                {
                    rs.ResponseCode = 200;
                    rs.Message = "Integrated Successfully";
                }
                else
                {
                    rs.ResponseCode = 500;
                    rs.Message = "SFDC Response Issue occured";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 200;
                rs.Message = "Internal Server Error Occur";
            }
            return rs;
        }
        [HttpPost("BillingLinesInfo_Pushed_to_SFDC_FromPortal")]
        public Response DataPushedtoSFDC(List<int> BillingIds)
        {
            Response rs = new Response();
            try
            {
                SFDCRestAPIAccess sfdca = new SFDCRestAPIAccess(_integrationAppSettings);
                var results = sfdca.InsertDataintoSFDCFromPortal(BillingIds);
               
                if (results)
                {
                    rs.ResponseCode = 200;
                    rs.Message = "Integrated Successfully";
                }
                else
                {
                    rs.ResponseCode = 500;
                    rs.Message = "SFDC Response Issue occured";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                rs.ResponseCode = 200;
                rs.Message = "Internal Server Error Occur";
            }
            return rs;
        }
    }
}
