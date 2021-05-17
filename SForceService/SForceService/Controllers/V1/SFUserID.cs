using Microsoft.AspNetCore.Mvc;
using System;
using SfServiceRef;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace SForceService.Controllers
{
    [Route("V1/[controller]")]
    [ApiController]
    public class SFUserID : ControllerBase
    {
        public class ClsRequest
        {
            public List<string> UserList { get; set; }
            public string LoginUserName { get; set; }
            public string LoginPassword { get; set; }

        }
        public class ClsResponse
        {
            public int StatusID { get; set; }
            public string StatusDescription { get; set; }
            public List<ClsUsersList> ObjUserList { get; set; }
        }

        public class ClsUsersList
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string UserRoleID { get; set; }
            public bool IsActive { get; set; }
        }
        private static string sessionId = null;
        private static string serverUrl = null;

        [HttpPost]
        [ApiVersion("1.0")]
        //[Route("GetEmployeeSFID")]
        public ClsResponse GetEmployeeSFID(ClsRequest ObjRequest)
        {
            ClsResponse ObjResponse = new ClsResponse();
            List<ClsUsersList> ObjList = new List<ClsUsersList>();
            string userlist;
            try
            {
                //ObjRequest.LoginUserName = "aidan.holmes@tccp.com.au";
                //ObjRequest.LoginPassword = "@P1_password" + "6ohQKoPPG99OP0u8SalTFy8E";
                userlist = "'" + String.Join("','", ObjRequest.UserList) + "'";
                SoapClient ss =new SoapClient();
                LoginResult lr=new LoginResult();
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
                    // Limit to display only 100 records 
                    //ss1.query(sHeader, null, null, null, @"SELECT Id,Name,OtherStreet,OtherCity,OtherState,OtherPostalCode,RecordType.Name FROM Contact WHERE RecordType.Name = 'Client' 
                    //        AND enrtcr__Status__c = 'Current'", out qr);
                ss1.query(sHeader, null, null, null, @"Select Id,EmployeeNumber,UserRoleID,IsActive,Username,CompanyName From User WHERE Username IN(" + userlist + ")", out qr);

                sObject[] records = qr.records;
                if (records != null)
                {
                    if (records.Length > 0)
                    {
                        for (int i = 0; i <= records.Length - 1; i++)
                        {
                            ClsUsersList Obj = new ClsUsersList();
                            Obj.Id= ((SfServiceRef.User)qr.records[i]).Id;
                            Obj.IsActive = (bool)((SfServiceRef.User)qr.records[i]).IsActive;
                            Obj.UserName = ((SfServiceRef.User)qr.records[i]).Username;
                            Obj.UserRoleID = ((SfServiceRef.User)qr.records[i]).UserRoleId;
                            ObjList.Add(Obj);
                        }
                        
                        
                        ObjResponse.StatusID = 200;
                        ObjResponse.StatusDescription = "Success";
                        ObjResponse.ObjUserList = ObjList;
                    }
                    else
                    {
                        ObjResponse.StatusID = 202;
                        ObjResponse.StatusDescription = "Unable to find Id";

                    }

                }
                else {
                    
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
