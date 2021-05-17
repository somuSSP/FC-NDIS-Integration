using FC_NDIS.Action;
using FC_NDIS.ApplicationIntegartionModels;
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

        //[HttpGet("IntegerateCustomerServiceLine")]
        //public Response Get()
        //{
        //    _logger.LogInformation("IntegerateCustomerServiceLine");
        //    Response rs = new Response();
        //    try
        //    {   
        //        SFDCAction sfdca = new SFDCAction(_integrationAppSettings);              
        //        var result = sfdca.IntegerateSfCustServiceLine(_integrationAppSettings.SFDCUserName, _integrationAppSettings.SFDCUserPassword);
        //        rs.ResponseCode = 200;
        //        rs.Message = "Integrated Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        rs.ResponseCode = 200;
        //        rs.Message = "Internal Server Error Occur";
        //    }
        //    return rs;
        //}


        //[HttpGet("IntegerateTransportRate")]
        //public Response TransportRateGet()
        //{
        //    _logger.LogInformation("IntegerateTransportRate");
        //    Response rs = new Response();
        //    try
        //    {
        //        SFDCAction sfdca = new SFDCAction(_integrationAppSettings);
        //        var result = sfdca.IntegerateSfTransportRate(_integrationAppSettings.SFDCUserName, _integrationAppSettings.SFDCUserPassword);
        //        rs.ResponseCode = 200;
        //        rs.Message = "Integrated Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        rs.ResponseCode = 200;
        //        rs.Message = "Internal Server Error Occur";
        //    }
        //    return rs;
        //}

        //[HttpGet("IntegerateTraveltRate")]
        //public Response TravelRateGet()
        //{
        //    _logger.LogInformation("IntegerateTraveltRate");
        //    Response rs = new Response();
        //    try
        //    {
        //        SFDCAction sfdca = new SFDCAction(_integrationAppSettings);
        //        var result = sfdca.IntegerateSfTravelRate(_integrationAppSettings.SFDCUserName, _integrationAppSettings.SFDCUserPassword);
        //        rs.ResponseCode = 200;
        //        rs.Message = "Integrated Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        rs.ResponseCode = 200;
        //        rs.Message = "Internal Server Error Occur";
        //    }
        //    return rs;
        //}


        //[HttpGet("IntegerateCustomer")]
        //public Response IntegerateSfCustomeList()
        //{
        //    _logger.LogInformation("IntegerateCustomer");
        //    Response rs = new Response();
        //    try
        //    {           
        //        SFDCAction sfdca = new SFDCAction(_integrationAppSettings);
        //        var result = sfdca.IntegerateSfCustomeList(_integrationAppSettings.SFDCUserName, _integrationAppSettings.SFDCUserPassword);
        //        rs.ResponseCode = 200;
        //        rs.Message = "Integrated Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        rs.ResponseCode = 200;
        //        rs.Message = "Internal Server Error Occur";
        //    }
        //    return rs;
        //}
        //[HttpGet("IntegerateSFDCIDtoDriver")]
        //public Response IntegerateSfDCIDs()
        //{
        //    _logger.LogInformation("IntegerateSFDCIDtoDriver");
        //    Response rs = new Response();
        //    try
        //    {             
        //        string userlist;              
        //        List<string> UserNames = new List<string>();
        //        SFDCAction sfdca = new SFDCAction(_integrationAppSettings);                
        //        UserNames = sfdca.GetAllDriverInfo_NotMappedSFDC();
        //       // userlist = string.Join(",'", UserNames);
        //        userlist = "'" +  string.Join("','", UserNames.Where(k=>!string.IsNullOrEmpty(k))) + "'";
        //        var result = sfdca.IntegrateSFDCId_OperatortoDB(userlist, _integrationAppSettings.SFDCUserName, _integrationAppSettings.SFDCUserPassword);
        //        if (result)
        //        {
        //            rs.ResponseCode = 200;
        //            rs.Message = "Integrated Successfully";
        //        }
        //        else
        //        {
        //            rs.ResponseCode = 500;
        //            rs.Message = "Internal server error occured";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        rs.ResponseCode = 200;
        //        rs.Message = "Internal Server Error Occur";
        //    }
        //    return rs;
        //}

        //[HttpPost("BillingLinesInfo_Pushed_to_SFDC")]
        //public Response DataPushedtoSFDC()
        //{
        //    Response rs = new Response();
        //    try
        //    {
        //        SFDCAction sfdca = new SFDCAction(_integrationAppSettings);
        //        var results = sfdca.InsertDataintoSFDC(_integrationAppSettings.SFDCUserName, _integrationAppSettings.SFDCUserPassword);                
        //        if (results)
        //        {
        //            rs.ResponseCode = 200;
        //            rs.Message = "Integrated Successfully";
        //        }
        //        else
        //        {
        //            rs.ResponseCode = 500;
        //            rs.Message = "SFDC Response Issue occured";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        rs.ResponseCode = 200;
        //        rs.Message = "Internal Server Error Occur";
        //    }
        //    return rs;
        //}
    }
}
