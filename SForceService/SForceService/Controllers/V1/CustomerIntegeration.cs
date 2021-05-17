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
    [Route("V1/[controller]")]
    [ApiController]
    public class CustomerIntegeration : ControllerBase
    {

        public class ClsCusIntegRequest
        {
            
            public string LoginUserName { get; set; }

            public string LoginPassword { get; set; }

        }

        public class ClsListofCustomers {
            public string Id { get; set; }

            public string Name { get; set; }
            public string OtherStreet { get; set; }

            public string OtherCity { get; set; }
            public string OtherState { get; set; }
            public string OtherPostalCode { get; set; }

            public string RecordTypeName { get; set; }
        }
       
        public class ClsCustomerListResponse
        {
            public int StatusID { get; set; }
            public string StatusDescription { get; set; }

            public List<ClsListofCustomers> ObjLstCutomers = new List<ClsListofCustomers>();
            
        }
        private static string sessionId = null;
        private static string serverUrl = null;

        [HttpPost]
        [ApiVersion("1.0")]
        //[Route("IntegerateSfCustomeList")]
        public ClsCustomerListResponse IntegerateSfCustomeList(ClsCusIntegRequest ObjRequest)
        {
            ClsCustomerListResponse ObjResponse = new ClsCustomerListResponse();
            List<ClsListofCustomers> LstRes = new List<ClsListofCustomers>();
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
                        // ObjResponse.Response = null;

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
                
                ss1.query(sHeader, null, null, null, @"SELECT Id,Name,OtherStreet,OtherCity,OtherState,OtherPostalCode,LastModifiedDate,RecordType.Name FROM Contact WHERE RecordType.Name = 'Client' 
                        AND (enrtcr__Status__c='Current' OR enrtcr__Status__c='Deceased' OR enrtcr__Status__c='Inactive' and LastModifiedDate <"+DateTime.Now.ToString("MM-dd-yyyy")+" )", out qr);


                sObject[] records = qr.records;
                if (records != null)
                {
                    if (records.Length > 0)
                    {                        
                        for (var i = 0; i <= records.Length - 1; i++)
                        {
                            ClsListofCustomers Res = new ClsListofCustomers();
                            Res.Id = ((SfServiceRef.Contact)qr.records[i]).Id;
                            Res.Name = ((SfServiceRef.Contact)qr.records[i]).Name;
                            Res.OtherStreet = ((SfServiceRef.Contact)qr.records[i]).OtherStreet;
                            Res.OtherCity = ((SfServiceRef.Contact)qr.records[i]).OtherCity;
                            Res.OtherStreet = ((SfServiceRef.Contact)qr.records[i]).OtherState;
                            var modifieddate = ((SfServiceRef.Contact)qr.records[i]).LastModifiedDate;
                            Res.OtherPostalCode = ((SfServiceRef.Contact)qr.records[i]).OtherPostalCode;
                            Res.RecordTypeName = ((SfServiceRef.Contact)qr.records[i]).RecordType.Name;
                            LstRes.Add(Res);
                        }                        
                        
                        ObjResponse.StatusID = 200;
                        ObjResponse.StatusDescription = "Success";
                        ObjResponse.ObjLstCutomers = LstRes;
                    }
                    else
                    {
                        ObjResponse.StatusID = 202;
                        ObjResponse.StatusDescription = "Unable to find client";
                    }
                }
                else
                {
                    ObjResponse.StatusID = 201;
                    ObjResponse.StatusDescription = "User not found";
                }

                //string JS = JsonConvert.SerializeObject(records);
                //Interaction.MsgBox("Success");

                //                ObjResponse.Response = records;
                return ObjResponse;
            }
            catch (Exception ex)
            {
                ObjResponse.StatusID = 500;
                ObjResponse.StatusDescription = ex.Message.ToString();
                //ObjResponse.Response = null;
                return (ObjResponse);
            }

        }
    }
}
