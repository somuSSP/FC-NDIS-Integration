using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SfServiceRef;
using Newtonsoft.Json;

namespace SForceService.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CusServiceLineIntegeration : ControllerBase
    {

        public class ClsCusServiceLineIntegRequest
        {
            public string LoginUserName { get; set; }
            public string LoginPassword { get; set; }
        }

        public class ClsListofCustServiceLine
        {
            public string Id { get; set; }

            public string Name { get; set; }
            public double? enrtcr__remaining__c { get; set; }
            public string enrtcr__Item_Overclaim__c { get; set; }
            public string enrtcr__Support_Contract__c { get; set; }
            public string enrtcr__Support_Contract__r_Name { get; set; }
            public DateTime? enrtcr__Support_Contract__r_enrtcr__End_Date__c { get; set; }
            public string enrtcr__Support_Contract__r_enrtcr__Status__c { get; set; }
            public string enrtcr__Support_Contract__r_enrtcr__Funding_Type__c { get; set; }
            public string enrtcr__Support_Contract__r_enrtcr__Funding_Management__c { get; set; }
            public string enrtcr__Support_Contract__r_enrtcr__Client__c { get; set; }
            public double? enrtcr__Category_Item__r_enrtcr__Support_Category_Amount__c { get; set; }
            public double? enrtcr__Category_Item__r_enrtcr__Delivered__c { get; set; }
            public string enrtcr__Site__c { get; set; }
            public string enrtcr__Site__r_Name { get; set; }
            public string enrtcr__Site__r_enrtcr__Site_GL_Code__c { get; set; }
            public string enrtcr__Service__c { get; set; }
            public string enrtcr__Service__r_Name { get; set; }
            public string enrtcr__Site_Service_Program__c { get; set; }
            public string enrtcr__Rate__c { get; set; }
            public string enrtcr__Rate__r_Name { get; set; }
            public double? enrtcr__Rate__r_enrtcr__Amount_Ex_GST__c { get; set; }
            public string enrtcr__Rate__r_enrtcr__Quantity_Type__c { get; set; }
            public string enrtcr__Rate__r_enrtcr__Allow_Rate_Negotiation__c { get; set; }

        }

        public class ClsCustomerServiceLineResponse
        {
            public int StatusID { get; set; }
            public string StatusDescription { get; set; }

            public List<ClsListofCustServiceLine> ObjLstCutomers = new List<ClsListofCustServiceLine>();

        }
        private static string sessionId = null;
        private static string serverUrl = null;

        [HttpPost]
        [ApiVersion("1.0")]
        //[Route("IntegerateSfCustomeList")]
        public ClsCustomerServiceLineResponse IntegerateSfCustServiceLine(ClsCusServiceLineIntegRequest ObjRequest)
        {
            ClsCustomerServiceLineResponse ObjResponse = new ClsCustomerServiceLineResponse();
            List<ClsListofCustServiceLine> LstRes = new List<ClsListofCustServiceLine>();
            try
            {
                //ObjRequest.LoginUserName = "aidan.holmes@tccp.com.au";
                //ObjRequest.LoginPassword = "@P1_password" + "6ohQKoPPG99OP0u8SalTFy8E";
                SoapClient ss = new SoapClient();
                LoginResult lr = new LoginResult();
                LoginScopeHeader LH = new LoginScopeHeader();

                if (sessionId == null | sessionId == "")
                {
                    // Login Call

                    lr = ss.login(null, ObjRequest.LoginUserName, ObjRequest.LoginPassword);
                    if (lr.passwordExpired)
                    {
                        ObjResponse.StatusID = 300;
                        ObjResponse.StatusDescription = "Password Expired";
                        return ObjResponse;
                    }
                    sessionId = lr.sessionId.ToString().Trim();
                    serverUrl = lr.serverUrl.ToString().Trim();
                }

                // Store SessionId in SessionHeader; We will need while making query() call
                SessionHeader sHeader = new SessionHeader();
                sHeader.sessionId = sessionId;

                // Variable to store query results
                QueryResult qr = new QueryResult();
                SoapClient ss1 = new SoapClient();
                ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);

               var results= ss1.queryAsync(sHeader, null, null, null, @"SELECT Id
                                                    ,Name
                                                    ,enrtcr__Remaining__c
                                                    ,enrtcr__Item_Overclaim__c
                                                    ,enrtcr__Support_Contract__c
                                                    ,enrtcr__Support_Contract__r.Name
                                                    ,enrtcr__Support_Contract__r.enrtcr__End_Date__c
                                                    ,enrtcr__Support_Contract__r.enrtcr__Status__c
                                                    ,enrtcr__Support_Contract__r.enrtcr__Funding_Type__c
                                                    ,enrtcr__Support_Contract__r.enrtcr__Funding_Management__c
                                                    ,enrtcr__Support_Contract__r.enrtcr__Client__c
                                                    ,enrtcr__Category_Item__r.enrtcr__Support_Category_Amount__c
                                                    ,enrtcr__Category_Item__r.enrtcr__Delivered__c
                                                    ,enrtcr__Site__c
                                                    ,enrtcr__Site__r.Name
                                                    ,enrtcr__Site__r.enrtcr__Site_GL_Code__c
                                                    ,enrtcr__Service__c
                                                    ,enrtcr__Service__r.Name
                                                    ,enrtcr__Site_Service_Program__c
                                                    ,enrtcr__Rate__c
                                                    ,enrtcr__Rate__r.Name
                                                    ,enrtcr__Rate__r.enrtcr__Amount_Ex_GST__c
                                                    ,enrtcr__Rate__r.enrtcr__Quantity_Type__c
                                                    ,enrtcr__Rate__r.enrtcr__Allow_Rate_Negotiation__c
                                                    FROM enrtcr__Support_Contract_Item__c 
                                                   WHERE (
                                                           (enrtcr__Service__c = '057F000006J0p3QADQ' AND enrtcr__Rate__r.enrtcr__Quantity_Type__c = 'Annual')
                                                            OR (enrtcr__Service__c = 'a0h5P0000005n8GQAQ' AND enrtcr__Rate__r.enrtcr__Quantity_Type__c = 'Each')
                                                            OR (enrtcr__Service__c = 'a0h5P0000004L35QAE' AND enrtcr__Rate__r.enrtcr__Quantity_Type__c = 'Hour')
                                                          ) ");


                sObject[] records = results.Result.result.records;
                if (records != null)
                {
                    if (records.Length > 0)
                    {
                        for (var i = 0; i <= records.Length - 1; i++)
                        {
                            ClsListofCustServiceLine Res = new ClsListofCustServiceLine();
                            Res.Id = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).Id;
                            Res.Name = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).Name;
                            Res.enrtcr__remaining__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Remaining__c;
                            Res.enrtcr__Item_Overclaim__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Item_Overclaim__c;
                            Res.enrtcr__Support_Contract__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__c;
                            Res.enrtcr__Support_Contract__r_Name = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.Name;
                            Res.enrtcr__Support_Contract__r_enrtcr__End_Date__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__End_Date__c;
                            Res.enrtcr__Support_Contract__r_enrtcr__Status__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c;
                            Res.enrtcr__Support_Contract__r_enrtcr__Funding_Type__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Funding_Type__c;
                            Res.enrtcr__Support_Contract__r_enrtcr__Funding_Management__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Funding_Management__c;
                            Res.enrtcr__Support_Contract__r_enrtcr__Client__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Client__c;
                            Res.enrtcr__Category_Item__r_enrtcr__Support_Category_Amount__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Category_Item__r.enrtcr__Support_Category_Amount__c;
                            Res.enrtcr__Category_Item__r_enrtcr__Delivered__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Category_Item__r.enrtcr__Delivered__c;
                            Res.enrtcr__Site__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site__c;
                            Res.enrtcr__Site__r_Name = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site__r.Name;
                            Res.enrtcr__Site__r_enrtcr__Site_GL_Code__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site__r.enrtcr__Site_GL_Code__c;
                            Res.enrtcr__Service__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__c;
                            Res.enrtcr__Service__r_Name = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__r.Name;
                            Res.enrtcr__Site_Service_Program__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site_Service_Program__c;
                            Res.enrtcr__Rate__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__c;
                            Res.enrtcr__Rate__r_Name = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.Name;
                            Res.enrtcr__Rate__r_enrtcr__Amount_Ex_GST__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Amount_Ex_GST__c;
                            Res.enrtcr__Rate__r_enrtcr__Quantity_Type__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Quantity_Type__c;
                            Res.enrtcr__Rate__r_enrtcr__Allow_Rate_Negotiation__c = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Allow_Rate_Negotiation__c;
                            LstRes.Add(Res);
                        }
                        ObjResponse.StatusID = 200;
                        ObjResponse.StatusDescription = "Success";
                        ObjResponse.ObjLstCutomers = LstRes;
                    }
                    else
                    {
                        ObjResponse.StatusID = 202;
                        ObjResponse.StatusDescription = "Query executed successfully with no records returned";

                    }

                }
                else
                {

                    ObjResponse.StatusID = 201;
                    ObjResponse.StatusDescription = "Query executed successfully with null result";

                }

                return ObjResponse;
            }
            catch (Exception ex)
            {
                ObjResponse.StatusID = 500;
                ObjResponse.StatusDescription = ex.Message.ToString();
                return (ObjResponse);
            }

        }

    }
}
