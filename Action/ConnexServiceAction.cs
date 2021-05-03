using ConnxServiceReference;
using FC_NDIS.ActionInterface;
using FC_NDIS.DBAccess;
using FC_NDIS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.Action
{
    public class ConnexServiceAction : IConnex
    {
                
        private readonly IntegrationAppSettings _integrationAppSettings;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        public ConnexServiceAction(IntegrationAppSettings integrationAppSettings)
        {
            this._integrationAppSettings = integrationAppSettings;
        }       
        public bool validator(string EnvName, string EnvAPI, int stagingSourceID)
        {          
            ConnXServiceClient CSC = new ConnXServiceClient();
            CSC.ValidateCaller(EnvName, EnvAPI, ref stagingSourceID);
            if (stagingSourceID == 0)
                return false;
            else
                return true;
        }

        public bool IntegrateDriverDetails(string userName, string Password)
        {           
            ConnXServiceClient CSC = new ConnXServiceClient();
            CSC.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
            var emppersonalDetails = CSC.GetEmployeePersonalDetail(userName, Password, null, string.Empty, string.Empty, string.Empty, null);
            var empEmploymentDetail = CSC.GetEmployeeEmploymentDetail(userName, Password, null, string.Empty, string.Empty, string.Empty, null, string.Empty);
            DBAction dba = new DBAction(_integrationAppSettings);
            List<Driver> Drivers = new List<Driver>();
            if (emppersonalDetails.Result.ErrorCode == 0 && empEmploymentDetail.Result.ErrorCode == 0)
            {
                for (int i = 0; i < emppersonalDetails.Items.Length; i++)
                {                    
                        var empEmploymentDetailNew = empEmploymentDetail.Items.FirstOrDefault(k => k.EmployeeCode == emppersonalDetails.Items[i].EmployeeCode);
                        Driver dr = new Driver();
                        dr.EmployeeCode = emppersonalDetails.Items[i].EmployeeCode ?? "";
                        dr.SalesForceUserId = "";//It was came from SFDC
                        dr.FirstName = emppersonalDetails.Items[i].GivenName ?? "";
                        dr.LastName = emppersonalDetails.Items[i].Surname ?? "";
                        dr.PreferedName = emppersonalDetails.Items[i].PreferredName;
                        dr.Username = emppersonalDetails.Items[i].EmailWork;
                        dr.Disabled = false;
                        dr.IsTerminated = emppersonalDetails.Items[i].IsTerminated == null ? false : Convert.ToBoolean(emppersonalDetails.Items[i].IsTerminated);                        
                        dr.Type = 1;
                        if (empEmploymentDetailNew != null)
                        {
                            if (!string.IsNullOrEmpty(empEmploymentDetailNew.CostAccountCodeDefault))
                            {
                                var costcenter = dba.GetCostcenterId(empEmploymentDetailNew.CostAccountCodeDefault);
                                dr.CostCenter = (int?)costcenter;
                            }
                            dr.JobDescription = empEmploymentDetailNew.JobDescription;
                            dr.Department = empEmploymentDetailNew.PrimaryDepartmentCode;
                            dr.ManagerName = empEmploymentDetailNew.PrimaryDepartmentManagerGivenName + " " + empEmploymentDetailNew.PrimaryDepartmentManagerSurname;
                            dr.Password = "ZZ9XcjvG47MrH2neZ2h5WzwwFVJ7hciId1Tpbi4FTuU";
                            dr.IsPortalUser = false;    
                            dr.Otp = null;
                        }                      
                         Drivers.Add(dr);                     
                   
                }
            }
            if (Drivers.Count > 0)
            {             
                dba.IntegrateOpertorintoDB(Drivers.Where(k => k.Username != "").ToList());
                return true;
            }
            else {           
                return false; }
        }
    }
}
