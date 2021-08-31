using ConnxServiceReference;
using FC_NDIS.ActionInterface;
using FC_NDIS.DBAccess;
using FC_NDIS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.Action
{
    public class ConnexServiceAction : IConnex
    {

        private readonly IntegrationAppSettings _integrationAppSettings;      
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public ConnexServiceAction(IntegrationAppSettings integrationAppSettings)
        {
            this._integrationAppSettings = integrationAppSettings;
        }
        public bool validator(string EnvName, string EnvAPI, int stagingSourceID)
        {
            ConnXServiceClient CSC = new ConnXServiceClient();
            ValidateCallerRequest vrq = new ValidateCallerRequest();
            vrq.strPassword = _integrationAppSettings.ConnexUserName;
            vrq.strPassword = _integrationAppSettings.ConnexUserPassword;           
            var RES = CSC.ValidateCallerAsync(vrq);
            if (RES.Result.intStagingSourceID == 0)
                return false;
            else
                return true;
        }

        public bool IntegrateDriverDetails(string userName, string Password)
        {
            logger.Info("Scheduled Connx Driver job triggered");
            ConnXServiceClient CSC = new ConnXServiceClient();
            CSC.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
            var firstDownload =Convert.ToBoolean( _integrationAppSettings.FirstTimeDownload);          
 
            if (firstDownload)
            {
                 var emppersonalDetails = CSC.GetEmployeePersonalDetailAsync(userName, Password, null, string.Empty, string.Empty, string.Empty, null);
                 var  empEmploymentDetailNewList = CSC.GetEmployeeEmploymentDetailAsync(userName, Password, null, string.Empty, null, null, false, null);
                DBAction dba = new DBAction(_integrationAppSettings);
                List<Driver> Drivers = new List<Driver>();
                if (emppersonalDetails.Result.Result.ErrorCode == 0)
                {
                    for (int i = 0; i < emppersonalDetails.Result.Items.Length; i++)
                    {
                        var empEmploymentDetailNew = empEmploymentDetailNewList.Result.Items.FirstOrDefault(k => k.EmployeeCode == emppersonalDetails.Result.Items[i].EmployeeCode);

                        Driver dr = new Driver();
                        dr.EmployeeCode = emppersonalDetails.Result.Items[i].EmployeeCode ?? "";
                        dr.SalesForceUserId = "";
                        dr.FirstName = emppersonalDetails.Result.Items[i].GivenName ?? "";
                        dr.LastName = emppersonalDetails.Result.Items[i].Surname ?? "";
                        dr.PreferedName = emppersonalDetails.Result.Items[i].PreferredName;
                        dr.Username = emppersonalDetails.Result.Items[i].EmailWork;
                        dr.Disabled = false;
                        dr.IsTerminated = emppersonalDetails.Result.Items[i].IsTerminated == null ? false : Convert.ToBoolean(emppersonalDetails.Result.Items[i].IsTerminated);
                        dr.Type = 1;
                        if (empEmploymentDetailNew != null)
                        {
                            if (!string.IsNullOrEmpty(empEmploymentDetailNew.CostAccountCodeDefault))
                            {
                                var costcenter = dba.GetCostcenterId(empEmploymentDetailNew.CostAccountCodeDefault);
                                dr.CostCenter = (int?)costcenter;
                            }
                            dr.JobDescription = empEmploymentDetailNew?.JobDescription ?? "";
                            dr.Department = empEmploymentDetailNew?.PrimaryDepartmentDescription ?? "";

                            dr.ManagerName = empEmploymentDetailNew.PrimaryDepartmentManagerGivenName + " " + empEmploymentDetailNew.PrimaryDepartmentManagerSurname;
                            dr.Password = "ZZ9XcjvG47MrH2neZ2h5WzwwFVJ7hciId1Tpbi4FTuU";
                            dr.IsPortalUser = false;
                            dr.Otp = null;
                        }
                        dr.CreatedDate = DateTime.Now;
                        dr.ModifiedDate = DateTime.Now;
                        if (dr.IsTerminated != true && emppersonalDetails.Result.Items[i].EmployeeCode.StartsWith("DIR") != true)
                            Drivers.Add(dr);

                    }
                }
                if (Drivers.Count > 0)
                {
                    dba.IntegrateOpertorintoDB(Drivers.Where(k => k.Username != "").ToList());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var emppersonalDetails = CSC.GetEmployeePersonalDetailAsync(userName, Password, null, string.Empty, string.Empty, string.Empty, null);
                var empEmploymentDetailNewList = CSC.GetEmployeeEmploymentDetailAsync(userName, Password, null, string.Empty, null, null, null, null);
                DBAction dba = new DBAction(_integrationAppSettings);
                List<Driver> Drivers = new List<Driver>();
                if (emppersonalDetails.Result.Result.ErrorCode == 0)
                {
                    for (int i = 0; i < emppersonalDetails.Result.Items.Length; i++)
                    {
                        var empEmploymentDetailNew = empEmploymentDetailNewList.Result.Items.Where(k => k.EmployeeCode == emppersonalDetails.Result.Items[i].EmployeeCode).FirstOrDefault();

                        Driver dr = new Driver();
                        dr.EmployeeCode = emppersonalDetails.Result.Items[i].EmployeeCode ?? "";
                        dr.SalesForceUserId = "";
                        dr.FirstName = emppersonalDetails.Result.Items[i].GivenName ?? "";
                        dr.LastName = emppersonalDetails.Result.Items[i].Surname ?? "";
                        dr.PreferedName = emppersonalDetails.Result.Items[i].PreferredName;
                        dr.Username = emppersonalDetails.Result.Items[i].EmailWork;
                        dr.Disabled = false;
                        dr.IsTerminated = emppersonalDetails.Result.Items[i].IsTerminated == null ? false : Convert.ToBoolean(emppersonalDetails.Result.Items[i].IsTerminated);
                        dr.Type = 1;
                        if (empEmploymentDetailNew != null)
                        {
                            if (!string.IsNullOrEmpty(empEmploymentDetailNew.CostAccountCodeDefault))
                            {
                                var costcenter = dba.GetCostcenterId(empEmploymentDetailNew.CostAccountCodeDefault);
                                dr.CostCenter = (int?)costcenter;
                            }
                            dr.JobDescription = empEmploymentDetailNew?.JobDescription ?? "";
                            dr.Department = empEmploymentDetailNew?.PrimaryDepartmentDescription ?? "";

                            dr.ManagerName = empEmploymentDetailNew.PrimaryDepartmentManagerGivenName + " " + empEmploymentDetailNew.PrimaryDepartmentManagerSurname;
                            dr.Password = "ZZ9XcjvG47MrH2neZ2h5WzwwFVJ7hciId1Tpbi4FTuU";
                            dr.IsPortalUser = false;
                            dr.Otp = null;
                        }
                        dr.CreatedDate = DateTime.Now;
                        dr.ModifiedDate = DateTime.Now;
                        if (emppersonalDetails.Result.Items[i].EmployeeCode.StartsWith("DIR") != true)
                            Drivers.Add(dr);

                    }
                }
                if (Drivers.Count > 0)
                {
                    dba.IntegrateOpertorintoDB(Drivers.Where(k => k.Username != "").ToList());
                    return true;
                }
                else
                {
                    return false;
                }
            }            
        }
    }
}
