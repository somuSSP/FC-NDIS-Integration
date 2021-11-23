using FC_NDIS.ApplicationIntegartionModels;
using FC_NDIS.JsonModels;
using FC_NDIS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Globalization;
using FC_NDIS.Utility;

namespace FC_NDIS.DBAccess
{
    public class DBAction : IDBAction
    {
        private readonly IntegrationAppSettings _integrationAppSettings;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;       
        public DBAction(IntegrationAppSettings integrationAppSettings)
        {
            this._integrationAppSettings = integrationAppSettings;
        }
        public bool IntegrateAssetsintoDB(List<Vehicle> vehicles)
        {
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                // _logger.LogInformation("Method IntegrateAssetsintoDB");
                foreach (var veh in vehicles)
                {
                    var vehinfo = dbc.Vehicles.FirstOrDefault(k => k.AssetId == veh.AssetId);
                    if (vehinfo == null)
                    {
                        dbc.Vehicles.Add(veh);
                    }
                    else
                    {
                        vehinfo.Registration = veh.Registration;
                        vehinfo.Make = veh.Make;
                        vehinfo.Description = veh.Description;
                        vehinfo.Model = veh.Model;
                        vehinfo.Type = veh.Type;
                        vehinfo.Active = veh.Active;
                        vehinfo.Category = veh.Category;
                        vehinfo.DriverId = veh.DriverId;
                        vehinfo.ModifiedDate = veh.ModifiedDate;
                    }
                    dbc.SaveChanges();
                }
            }
            return true;
        }
        /// <summary>
        /// Customer Line information pushed to Customer Service Line
        /// </summary>
        /// <param name="cslines"></param>
        /// <returns></returns>
        public bool IntegrateCustomerLineinfointoDB(List<CustomerServiceLine> cslines)
        {
            
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                foreach (var csl in cslines)
                {
                    var cslinfo = dbc.CustomerServiceLines.FirstOrDefault(k => k.ServiceAgreementId == csl.ServiceAgreementId && k.ServiceAgreementItemId == csl.ServiceAgreementItemId && k.ServiceAgreementCustomerId == csl.ServiceAgreementCustomerId);
                    if (cslinfo == null)
                    {
                        if (csl.ServiceAgreementStatus == 1 || csl.ServiceAgreementStatus == 7)
                        {
                            dbc.CustomerServiceLines.Add(csl);
                            dbc.SaveChanges();
                        }
                    }
                    else
                    {
                        cslinfo.ServiceAgreementCustomerId = csl.ServiceAgreementCustomerId;
                        cslinfo.ServiceAgreementName = csl.ServiceAgreementName;
                        cslinfo.ServiceAgreementEndDate = csl.ServiceAgreementEndDate;
                        cslinfo.ServiceAgreementStatus = csl.ServiceAgreementStatus;
                        cslinfo.ServiceAgreementFundingManagement = csl.ServiceAgreementFundingManagement;
                        cslinfo.ServiceAgreementFundingType = csl.ServiceAgreementFundingType;
                        cslinfo.ServiceAgreementItemName = csl.ServiceAgreementItemName;
                        cslinfo.SupportCategoryAmount = csl.SupportCategoryAmount;
                        cslinfo.SupportCategoryDelivered = csl.SupportCategoryDelivered;
                        cslinfo.FundsRemaining = csl.FundsRemaining;
                        cslinfo.ItemOverclaim = csl.ItemOverclaim;
                        cslinfo.SiteGlcode = csl.SiteGlcode;
                        cslinfo.CategoryItemId = csl.CategoryItemId;
                        cslinfo.SiteId = csl.SiteId;
                        cslinfo.SiteName = csl.SiteName;
                        cslinfo.SiteServiceProgramId = csl.SiteServiceProgramId;
                        cslinfo.ServiceId = csl.ServiceId;
                        cslinfo.ServiceName = csl.ServiceName;
                        cslinfo.TravelServiceId = csl.TravelServiceId;
                        cslinfo.TransportServiceId = csl.TransportServiceId;
                        cslinfo.AllowRateNegotiation = csl.AllowRateNegotiation;
                        cslinfo.ModifiedDate = DateTime.Now;
                        dbc.SaveChanges();
                    }
                }
            }
            return true;
        }

        public DateTime? Last_CustomerLineIntegrate(int Type)
        {
            DateTime? result = new DateTime();
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                if (Type == 5)
                {
                    var exitingrecords = dbc.ApplicationSettings.FirstOrDefault(k => k.Key == "Last_CustomerLineIntegrate");
                    try
                    {
                        _logger.LogInformation("Last_CustomerLineIntegrate datetime:" + exitingrecords.Value);
                        var obj = Convert.ToDateTime(exitingrecords.Value);
                        _logger.LogInformation("Last_CustomerLineIntegrate Converted datetime:" + obj.ToString());
                        result = obj;
                    }
                    catch (Exception ex)
                    {
                        result = DateTime.Now.AddDays(-5);
                    }
                }
            }
            return result;
        }
        public Boolean ModifiedCustomerServiceLineIntegratedTime()
        {
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                try
                {
                    var exitingrecords = dbc.ApplicationSettings.FirstOrDefault(k => k.Key == "Last_CustomerLineIntegrate");
                    if (exitingrecords != null)
                    { exitingrecords.Value = DateTime.Now.ToString("yyyy-MM-dd"); }
                    else
                    {
                        exitingrecords = new ApplicationSetting();
                        exitingrecords.Key = "Last_CustomerLineIntegrate";
                        exitingrecords.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    dbc.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool IntegrateErrorCustomerLineinfointoDB(List<CustomerServiceLine> cslines)
        {           
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                foreach (var csl in cslines)
                {
                    var cslinfo = dbc.CustomerServiceLinesErrors.FirstOrDefault(k => k.ServiceAgreementId == csl.ServiceAgreementId && k.ServiceAgreementItemId == csl.ServiceAgreementItemId && k.ServiceAgreementCustomerId == csl.ServiceAgreementCustomerId);
                    if (cslinfo == null)
                    {
                        var cslerror = new CustomerServiceLinesError();
                        cslerror.ServiceAgreementCustomerId = csl.ServiceAgreementCustomerId;
                        cslerror.ServiceAgreementId = csl.ServiceAgreementId;
                        cslerror.ServiceAgreementName = csl.ServiceAgreementName;
                        cslerror.ServiceAgreementEndDate = csl.ServiceAgreementEndDate;
                        cslerror.ServiceAgreementStatus = csl.ServiceAgreementStatus;
                        cslerror.ServiceAgreementFundingManagement = csl.ServiceAgreementFundingManagement;
                        cslerror.ServiceAgreementFundingType = csl.ServiceAgreementFundingType;
                        cslerror.ServiceAgreementItemId = csl.ServiceAgreementItemId;
                        cslerror.ServiceAgreementItemName = csl.ServiceAgreementItemName;
                        cslerror.SupportCategoryAmount = csl.SupportCategoryAmount;
                        cslerror.SupportCategoryDelivered = csl.SupportCategoryDelivered;
                        cslerror.FundsRemaining = csl.FundsRemaining;
                        cslerror.ItemOverclaim = csl.ItemOverclaim;
                        cslerror.SiteId = csl.SiteId;
                        cslerror.SiteName = csl.SiteName;
                        cslerror.SiteGlcode = csl.SiteGlcode;
                        cslerror.SiteServiceProgramId = csl.SiteServiceProgramId;
                        cslerror.ServiceId = csl.ServiceId;
                        cslerror.ServiceName = csl.ServiceName;
                        cslerror.TravelServiceId = csl.TravelServiceId;
                        cslerror.TransportServiceId = csl.TransportServiceId;
                        cslerror.CategoryItemId = csl.CategoryItemId;
                        cslerror.Default = csl.Default;
                        dbc.CustomerServiceLinesErrors.Add(cslerror);
                        dbc.SaveChanges();
                    }
                    else
                    {
                        cslinfo.ServiceAgreementCustomerId = csl.ServiceAgreementCustomerId;
                        cslinfo.ServiceAgreementName = csl.ServiceAgreementName;
                        cslinfo.ServiceAgreementEndDate = csl.ServiceAgreementEndDate;
                        cslinfo.ServiceAgreementStatus = csl.ServiceAgreementStatus;
                        cslinfo.ServiceAgreementFundingManagement = csl.ServiceAgreementFundingManagement;
                        cslinfo.ServiceAgreementFundingType = csl.ServiceAgreementFundingType;
                        cslinfo.ServiceAgreementItemName = csl.ServiceAgreementItemName;
                        cslinfo.SupportCategoryAmount = csl.SupportCategoryAmount;
                        cslinfo.SupportCategoryDelivered = csl.SupportCategoryDelivered;
                        cslinfo.FundsRemaining = csl.FundsRemaining;
                        cslinfo.ItemOverclaim = csl.ItemOverclaim;
                        cslinfo.SiteGlcode = csl.SiteGlcode;
                        cslinfo.CategoryItemId = csl.CategoryItemId;
                        cslinfo.SiteId = csl.SiteId;
                        cslinfo.SiteName = csl.SiteName;
                        cslinfo.SiteServiceProgramId = csl.SiteServiceProgramId;
                        cslinfo.ServiceId = csl.ServiceId;
                        cslinfo.ServiceName = csl.ServiceName;
                        cslinfo.TravelServiceId = csl.TravelServiceId;
                        cslinfo.TransportServiceId = csl.TransportServiceId;
                        cslinfo.AllowRateNegotiation = csl.AllowRateNegotiation;
                        cslinfo.ModifiedDate = DateTime.Now;
                        dbc.SaveChanges();
                    }
                }
            }
            return true;
        }


        public bool ExistingCustomerLineinfoStatusChanged(List<CustomerServiceLine> cslines)
        {
            bool result = false;
            //if (staticDBACTION.exitingList.Count > 0)
            //{
                using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
                {
                    foreach (var csl in cslines)
                    {
                        var cslinfo = dbc.CustomerServiceLines.FirstOrDefault(k => k.ServiceAgreementId == csl.ServiceAgreementId && k.ServiceAgreementItemId == csl.ServiceAgreementItemId && k.ServiceAgreementCustomerId == csl.ServiceAgreementCustomerId);
                        if (cslinfo != null)
                        {
                            cslinfo.ServiceAgreementStatus = csl.ServiceAgreementStatus;
                            dbc.SaveChanges();
                        }
                    }
                }
            //}
            return result;
        }
        public int GetCustomerId(string CustomerId)

        {
            int result = 0;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var results = dbc.Customers.Where(k => k.CustomerId == CustomerId).FirstOrDefault();
                result = results?.CustId ?? 0;
            }
            return result;
        }

        /// <summary>
        /// SFDC User information mapped into Operator table
        /// </summary>
        /// <param name="SFDCUsers"></param>
        /// <returns></returns>
        public bool IntegrateAllDriver(List<ClsUsersList> SFDCUsers)
        {
            bool result = false;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                foreach (var users in SFDCUsers)
                {
                    try
                    {
                        bool option = false;
                        if (_integrationAppSettings.IntegrationforSandbox != null)
                            option = Convert.ToBoolean(_integrationAppSettings.IntegrationforSandbox);
                        if (option)
                            users.UserName = users.UserName.Remove(users.UserName.Length - 9);
                        var driver = dbc.Drivers.FirstOrDefault(k => k.Username == users.UserName);
                        if (driver != null)
                        {
                            driver.SalesForceUserId = users?.Id ?? "";
                            dbc.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// SFDC User information mapped into Operator table
        /// </summary>
        /// <param name="SFDCUsers"></param>
        /// <returns></returns>
        public string GetSalesforceInformation()
        {
            string filterstring = "";
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var driver = dbc.SalesForceServices.Where(K => K.Active == true).ToList();
                foreach (var dr in driver)
                {
                    if (filterstring.Length > 0)
                        filterstring += " OR ";
                    if (dr.ServiceId.Length > 18)
                        dr.ServiceId += dr.ServiceId + "Q";
                    filterstring += " (enrtcr__Service__c = '" + dr.ServiceId + "')";// AND enrtcr__Rate__r.enrtcr__Quantity_Type__c = 'Each')";
                }
            }
            return filterstring;
        }


        /// <summary>
        /// SFDC User information mapped into Operator table
        /// </summary>
        /// <param name="SFDCUsers"></param>
        /// <returns></returns>
        public List<string> GetDriverInformationIsnotMappedSFDC()
        {
            List<string> result = new List<string>();

            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var objs = dbc.Drivers.Where(k => (string.IsNullOrEmpty(k.SalesForceUserId)) && k.Username != "" && k.IsTerminated == false).ToList();
                foreach (var ob in objs)
                {
                    if (!string.IsNullOrEmpty(ob.Username))
                    {
                        bool option = false;
                        if (_integrationAppSettings.IntegrationforSandbox != null)
                            option = Convert.ToBoolean(_integrationAppSettings.IntegrationforSandbox);
                        if (!ob.Username.Contains("'"))
                        {
                            if (option)
                                result.Add(ob.Username + ".newacuat");
                            else
                                result.Add(ob.Username);
                        }
                    }
                }
            }
            return result;
        }

        public object GetCostcenterId(string ccvalue)
        {
            int? result;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                result = dbc.ConnxCcs.FirstOrDefault(k => k.ConnxCcvalue == ccvalue)?.CostCentreId ?? null;
            }
            return result;
        }

        /// <summary>
        /// SFDC User information mapped into Operator table
        /// </summary>
        /// <param name="SFDCUsers"></param>
        /// <returns></returns>
        public List<SFDCBillingLines> GetBillingInformation()
        {
            List<SFDCBillingLines> result = new List<SFDCBillingLines>();

            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var objBillingLinesList = dbc.BillingLinesNews.Where(k => k.Approved == true && k.Billable == true).ToList();
                
                if (objBillingLinesList != null)
                {
                    var objFinalBillingLinesList = objBillingLinesList.Where(k => k.SentToSalesForceStatus == false || k.SentToSalesForce == false || k.SentToSalesForce == null || k.SentToSalesForceStatus == null).ToList();
                    objFinalBillingLinesList = objFinalBillingLinesList.Where(k => k.CustomerTripId != null).ToList();
                    foreach (var bl in objFinalBillingLinesList)//need to verify the list.
                    {
                        var customerTrip = dbc.BillingCustomerTrips.Where(k => k.CustomerTripId == bl.CustomerTripId).FirstOrDefault();//done
                        var customer = dbc.Customers.Where(k => k.CustId == customerTrip.CustomerId).FirstOrDefault();
                        var Trip = dbc.Trips.Where(k => k.TripId == bl.TripId).FirstOrDefault();
                        if (Trip != null)
                        {
                            var drivers = dbc.Drivers.Where(k => k.DriverId == (Trip.DriverId ?? 0)).FirstOrDefault();
                            var cslines = dbc.CustomerServiceLines.Where(k => k.ServiceAgreementCustomerId == customerTrip.CustomerId && k.ServiceAgreementId == bl.ServiceAgreementId && k.ServiceAgreementItemId == bl.ServiceAgreementItemId).FirstOrDefault();
                            var SFRate = dbc.SalesforceRates.Where(k => k.SalesforceRatesId == bl.SalesforceRatesId).FirstOrDefault();
                            SFDCBillingLines bls = new SFDCBillingLines();
                            bls.BillingID = bl.BillingId;

                            bls.enrtcr__Client__c = customer.CustomerId.ToString();
                            bls.enrtcr__Date__c = customerTrip?.StartDate.ToString();// Automatic from Trip
                            bls.enrtcr__Quantity__c = Math.Round((decimal)(customerTrip?.CustomerKm ?? 0), 1);//Actual Distance (Km) from Trip

                            bls.enrtcr__Support_Contract_Item__c = bl?.ServiceAgreementItemId ?? "";
                            bls.enrtcr__Support_Contract__c = bl?.ServiceAgreementId ?? "";
                            bls.enrtcr__Site__c = cslines?.SiteId ?? null;//site

                            bls.enrtcr__Support_CategoryId__c = cslines?.CategoryItemId ?? "";//CategoryItem
                            bls.enrtcr__Site_Service_Program__c = cslines?.SiteServiceProgramId ?? "";//Site Service Program;
                            bls.enrtcr__Rate__c = SFRate?.RateId ?? "";//ob.UnitOfMeasure.ToString();
                            bls.enrtcr__Comments__c = "BLid " + bl.BillingId + " | DTid " + bl.TripId + " | CTid " + bl.CustomerTripId;
                            bls.enrtcr__Worker__c = drivers?.SalesForceUserId;//worker
                            bls.enrtcr__Client_Rep_Accepted__c = true;//client rep accepted
                            bls.enrtcr__Use_Negotiated_Rate__c = true;//nogotitiated

                            if (bl.AllowNegotiation == true)
                            {
                                bls.enrtcr__Negotiated_Rate_Ex_GST__c = Math.Round((decimal)(bl.BlendedRate ?? 0), 2);//Nogotiated Rate GST                    
                                bls.enrtcr__Negotiated_Rate_GST__c = (decimal)(00.00);//Nogotiated Rate GST
                            }
                            if (drivers?.SalesForceUserId != "121")
                                result.Add(bls);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// SFDC User information mapped into Operator table
        /// </summary>
        /// <param name="SFDCUsers"></param>
        /// <returns></returns>
        public List<SFDCBillingLines> GetBillingInformationusingIds(List<int> BillingIds)
        {
            List<SFDCBillingLines> result = new List<SFDCBillingLines>();

            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var objBillingLinesList = dbc.BillingLinesNews.Where(u => BillingIds.Any(k => k == u.BillingId)).ToList();

                if (objBillingLinesList != null)
                {
                    foreach (var bl in objBillingLinesList)
                    {
                        var customerTrip = dbc.BillingCustomerTrips.Where(k => k.CustomerTripId == bl.CustomerTripId).FirstOrDefault();//done
                        var customer = dbc.Customers.Where(k => k.CustId == customerTrip.CustomerId).FirstOrDefault();
                        var Trip = dbc.Trips.Where(k => k.TripId == bl.TripId).FirstOrDefault();
                        var drivers = dbc.Drivers.Where(k => k.DriverId == (Trip.DriverId ?? 0)).FirstOrDefault();
                        var cslines = dbc.CustomerServiceLines.Where(k => k.ServiceAgreementCustomerId == customerTrip.CustomerId && k.ServiceAgreementId == bl.ServiceAgreementId && k.ServiceAgreementItemId == bl.ServiceAgreementItemId).FirstOrDefault();
                        var SFRate = dbc.SalesforceRates.Where(k => k.SalesforceRatesId == bl.SalesforceRatesId).FirstOrDefault();
                        SFDCBillingLines bls = new SFDCBillingLines();
                        bls.BillingID = bl.BillingId;

                        bls.enrtcr__Client__c = customer.CustomerId.ToString();
                        bls.enrtcr__Date__c = customerTrip?.StartDate.ToString();// Automatic from Trip
                        bls.enrtcr__Quantity__c = Math.Round((decimal)(customerTrip?.CustomerKm ?? 0), 1);//Actual Distance (Km) from Trip

                        if (bl?.ServiceAgreementItemId != null)
                            bls.enrtcr__Support_Contract_Item__c = bl?.ServiceAgreementItemId ?? "";
                        if (bl?.ServiceAgreementId != null)
                            bls.enrtcr__Support_Contract__c = bl?.ServiceAgreementId ?? "";
                        if (cslines != null)
                        {
                            bls.enrtcr__Site__c = cslines?.SiteId ?? null;//site
                            bls.enrtcr__Support_CategoryId__c = cslines?.CategoryItemId ?? "";//CategoryItem
                            bls.enrtcr__Site_Service_Program__c = cslines?.SiteServiceProgramId ?? "";//Site Service Program;
                        }
                        bls.enrtcr__Rate__c = SFRate?.RateId ?? "";//ob.UnitOfMeasure.ToString();
                        bls.enrtcr__Comments__c = "BLid " + bl.BillingId + " | DTid " + bl.TripId + " | CTid " + bl.CustomerTripId;
                        bls.enrtcr__Worker__c = drivers?.SalesForceUserId;//worker
                        bls.enrtcr__Client_Rep_Accepted__c = true;//client rep accepted
                        bls.enrtcr__Use_Negotiated_Rate__c = true;//nogotitiated

                        if (bl.AllowNegotiation == true)
                        {
                            bls.enrtcr__Negotiated_Rate_Ex_GST__c = Math.Round((decimal)(bl.BlendedRate ?? 0), 2);//Nogotiated Rate GST                    
                            bls.enrtcr__Negotiated_Rate_GST__c = (decimal)(00.00);//Nogotiated Rate GST
                        }
                        //if (drivers?.SalesForceUserId != "121")
                        result.Add(bls);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Driver information pushed to driver table
        /// </summary>
        /// <param name="drivers"></param>
        /// <returns></returns>
        public bool IntegrateOpertorintoDB(List<Driver> drivers)
        {
            bool result = false;
            foreach (var drv in drivers)
            {
                using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
                {
                    var existingrecord = dbc.Drivers.FirstOrDefault(k => k.EmployeeCode == drv.EmployeeCode);
                    if (existingrecord == null)
                    {
                        if (drv.IsTerminated == false)
                            dbc.Drivers.Add(drv);
                    }
                    else
                    {
                        existingrecord.FirstName = drv.FirstName;
                        existingrecord.LastName = drv.LastName;
                        existingrecord.CostCenter = drv.CostCenter;
                        existingrecord.Username = drv.Username;
                        existingrecord.Type = drv.Type;
                        existingrecord.JobDescription = drv.JobDescription;
                        existingrecord.Department = drv.Department;
                        existingrecord.ManagerName = drv.ManagerName;
                        existingrecord.JobNumber = drv.JobNumber;
                        existingrecord.IsTerminated = drv.IsTerminated;
                        existingrecord.Disabled = drv.Disabled;
                        existingrecord.PreferedName = drv.PreferedName;
                        existingrecord.ModifiedDate = drv.ModifiedDate;

                    }
                    dbc.SaveChanges();

                }
                result = true;
            }
            return result;
        }

        public bool SFDCActionStatus(int BillingId, bool SentToSalesForceStatus, string SentToSalesForceDescription, string SalesForceBillingID)
        {
            bool result = false;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var billlineNew = dbc.BillingLinesNews.FirstOrDefault(k => k.BillingId == BillingId);
                billlineNew.SentToSalesForce = true;
                billlineNew.SentToSalesForceStatus = SentToSalesForceStatus;
                billlineNew.SentToSalesForceDescription = SentToSalesForceDescription;
                billlineNew.DateTransferred = DateTime.Now;
                if (SalesForceBillingID != "")
                {
                    billlineNew.SalesForceBillingId = SalesForceBillingID;
                }
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool SFDCActionStatus(int BillingId, bool SentToSalesForceStatus, string SentToSalesForceDescription, string billingId, string SalesForceBillingID)
        {
            bool result = false;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var billlineNew = dbc.BillingLinesNews.FirstOrDefault(k => k.BillingId == BillingId);
                billlineNew.SentToSalesForce = true;
                billlineNew.SentToSalesForceStatus = SentToSalesForceStatus;
                billlineNew.SentToSalesForceDescription = SentToSalesForceDescription;
                billlineNew.DateTransferred = DateTime.Now;
                if (SalesForceBillingID != "")
                {
                    billlineNew.SalesForceBillingId = billingId;
                    billlineNew.SDNumber = SalesForceBillingID;
                }
                dbc.SaveChanges();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Customer information pushed to customer table
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool IntegrateCustomerInfotoDB(List<Customer> customer)
        {
            bool result = false;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                foreach (var cs in customer)
                {
                    var csinfo = dbc.Customers.FirstOrDefault(k => k.CustomerId == cs.CustomerId);
                    if (csinfo == null)
                    {
                        if (cs.Status == 1)
                            dbc.Customers.Add(cs);
                    }
                    else
                    {
                        csinfo.CustomerId = cs.CustomerId;
                        csinfo.LumaryId = cs.LumaryId;
                        csinfo.Name = cs.Name;
                        csinfo.Street = cs.Street;
                        csinfo.City = cs.City;
                        csinfo.State = cs.State;
                        csinfo.PostalCode = cs.PostalCode;
                        csinfo.Status = cs.Status;
                        cs.ModifiedDate = DateTime.Now;

                    }
                    dbc.SaveChanges();
                }
            }
            result = true;
            return result;
        }

        public bool DeleteCustomerRecordbasedonSFDCRecord()
        {
            bool result = false;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {                
                var deletedCustomers = dbc.Customers.Where(p => !staticDBACTION.exitingList.Contains(p.CustomerId)).ToList();
                foreach (var cs in deletedCustomers)
                {
                    string NotList = "Customer Not Listed";
                    if (!cs.Name.ToLower().Contains(NotList.ToLower().ToString()))
                    {
                        cs.Status = 3;
                        cs.Active = false;
                    }
                }
                dbc.SaveChanges();
                staticDBACTION.exitingList.Clear();
                staticDBACTION.update = false;
            }
            result = true;
            return result;
        }

        public bool IntegrateTravelandTransportRateInfotoDB(List<SalesforceRate> TTRates)
        {
            bool result = false;

            foreach (var TTR in TTRates)
            {
                using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
                {
                    var existingrecord = dbc.SalesforceRates.FirstOrDefault(k => k.ServiceId == TTR.ServiceId && k.RateId == TTR.RateId);
                    if (existingrecord == null)
                    {
                        dbc.SalesforceRates.Add(TTR);
                    }
                    else
                    {
                        existingrecord.RateId = TTR.RateId;
                        existingrecord.ServiceId = TTR.ServiceId;
                        existingrecord.RateName = TTR.RateName;
                        existingrecord.Negotiation = TTR.Negotiation;
                        existingrecord.Rate = TTR.Rate;
                        existingrecord.StartDate = TTR.StartDate;
                        existingrecord.EndDate = TTR.EndDate;
                        existingrecord.RateType = TTR.RateType;

                    }
                    dbc.SaveChanges();
                }
                result = true;
            }
            result = true;
            return result;
        }

        public DateTime? GetLastintegratedDateandTime(string Name)
        {
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var existingAppsetting = dbc.ApplicationSettings.Where(k => k.Key == Name).SingleOrDefault();
                if (existingAppsetting != null)
                    return Convert.ToDateTime(existingAppsetting.Value);
                else
                    return null;
            }
        }

        public Driver GetDriverInformation(int driverId)
        {
            Driver dr = new Driver();
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                dr = dbc.Drivers.FirstOrDefault(k => k.DriverId == driverId);
            }
            return dr;
        }
        public List<Driver> GetAllDriverInformation()
        {
            List<Driver> dr = new List<Driver>();
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                dr = dbc.Drivers.ToList();
            }
            return dr;
        }

        public int GetIntegrationActivityId()
        {
            int IntegrationActivityId = 0;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                IntegrationActivityId = dbc.IntegrationActivities.Where(k=>k.IntegrationActivityName==11).OrderByDescending(k=>k.IntegrationActivityId).FirstOrDefault().IntegrationActivityId;
            }
            return IntegrationActivityId;
        }
        public bool InsertLogIntoIntegrationActivityLog(IntegrationActivityLog log)
        {
            try
            {
                using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
                {
                    var IntLog = dbc.IntegrationActivityLogs.Where(k => k.IntegrationActivityId == log.IntegrationActivityId).FirstOrDefault();
                    if (IntLog == null)
                    {
                        dbc.IntegrationActivityLogs.Add(log);
                        
                    }
                    else
                    {                       
                        IntLog.TotalPushedRecordCount = log.TotalPushedRecordCount;
                        IntLog.IntegrationActivityId = log.IntegrationActivityId;
                        IntLog.SuccessCount = log.SuccessCount;
                        IntLog.FailedCount = log.FailedCount;
                        IntLog.CreatedRecordCount = log.CreatedRecordCount;
                        IntLog.ModifiedRecordCount = log.ModifiedRecordCount;
                        Utilitys uts = new Utilitys(_integrationAppSettings);
                        var CCDate = uts.GetDateTime();
                        IntLog.CreatedDate = CCDate;
                        IntLog.ModifiedDate = CCDate;
                    }
                    dbc.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public bool UpdatedInformation(int driverId, string FCResourceID)
        {
            Boolean result = true;
            try
            {
                using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
                {
                    var dr = dbc.Drivers.FirstOrDefault(k => k.DriverId == driverId);
                    dr.FCResourceID = FCResourceID;
                    dbc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool UpdatedFCInformationintoDB(Dictionary<string, string> FCLists, Dictionary<string, string> FCOriginalList)
        {
            Boolean result = true;
            try
            {
                using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
                {
                    foreach (var input in FCLists)
                    {
                        var dr = dbc.Drivers.FirstOrDefault(k => k.EmployeeCode == input.Key);
                        var rfid = FCOriginalList.FirstOrDefault(k => k.Key == input.Value);
                        if (dr != null)
                        {
                            dr.FCResourceID = input.Value;
                            dr.RFID = rfid.Value;
                            dbc.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }

    public static class staticDBACTION
    {
        public static List<string> exitingList = new List<string>();
        public static bool update = false;

    }
}
