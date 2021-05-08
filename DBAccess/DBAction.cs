using FC_NDIS.ApplicationIntegartionModels;
using FC_NDIS.JsonModels;
using FC_NDIS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
                    var vehinfo = dbc.Vehicles.FirstOrDefault(k => k.Registration == veh.Registration);
                    if (vehinfo == null)
                    {
                        dbc.Vehicles.Add(veh);
                    }
                    else
                    {
                        vehinfo.Make = veh.Make;
                        vehinfo.Model = veh.Model;
                        vehinfo.Type = veh.Type;
                        vehinfo.Category = veh.Category;
                        vehinfo.DriverId = veh.DriverId;
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
            // _logger.LogInformation("Method IntegrateCustomerLineinfointoDB");
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                foreach (var csl in cslines)
                {
                    var cslinfo = dbc.CustomerServiceLines.FirstOrDefault(k => k.ServiceAgreementId == csl.ServiceAgreementId && k.ServiceAgreementItemId == csl.ServiceAgreementItemId && k.ServiceAgreementCustomerId == csl.ServiceAgreementCustomerId);
                    if (cslinfo == null)
                    {
                        dbc.CustomerServiceLines.Add(csl);
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
                        cslinfo.SiteId = csl.SiteId;
                        cslinfo.SiteName = csl.SiteName;
                        cslinfo.SiteServiceProgramId = csl.SiteServiceProgramId;
                        cslinfo.ServiceId = csl.ServiceId;
                        cslinfo.ServiceName = csl.ServiceName;                        
                        cslinfo.TravelServiceId = csl.TravelServiceId;
                        cslinfo.TransportServiceId = csl.TransportServiceId;
                        cslinfo.AllowRateNegotiation = csl.AllowRateNegotiation;
                        dbc.SaveChanges();
                    }

                }
            }
            return true;
        }

        public int GetCustomerId(string CustomerId)

        {
            int result = 0;
            using (NDISINT18Apr2021Context dbc = new NDISINT18Apr2021Context(this._integrationAppSettings))
            {
                var results = dbc.Customers.Where(k => k.CustomerId == CustomerId).FirstOrDefault();
                result = results.CustId;
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
                    users.UserName = users.UserName.Remove(users.UserName.Length - 9);
                    var driver = dbc.Drivers.FirstOrDefault(k => k.Username == users.UserName);
                    driver.SalesForceUserId = users.Id;
                    dbc.SaveChanges();
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
                var objs = dbc.Drivers.Where(k => k.SalesForceUserId != null && k.SalesForceUserId == "" && k.Username != "").ToList();
                foreach (var ob in objs)
                {
                    if (!string.IsNullOrEmpty(ob.Username))
                    {
                        if (!ob.Username.Contains("'"))
                            result.Add(ob.Username + ".newacuat");
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
                var objs = dbc.BillingLines.Where(k => k.Approved == true).ToList();

                var objBillingLinesList = dbc.BillingLines.Where(k => k.Approved == true).ToList();
                var objCustomerList = dbc.Customers.ToList();
                var objCustomerServiceLineList = dbc.CustomerServiceLines.ToList();
                var objTripList = dbc.Trips.ToList();
                var objDriverList = dbc.Drivers.ToList();
                foreach (var bl in objBillingLinesList)
                {
                    var trip = objTripList.FirstOrDefault(k => k.TripId == bl.TripId);
                    var cslines = objCustomerServiceLineList.FirstOrDefault(k => k.ServiceAgreementId == bl.ServiceAgreementId && k.ServiceAgreementItemId == bl.ServiceAgreementItemId);
                    var drivers = objDriverList.FirstOrDefault(k => k.DriverId == bl.DriverId);
                    SFDCBillingLines bls = new SFDCBillingLines();

                    bls.enrtcr__Client__c = bl.CustomerId.ToString();
                    bls.enrtcr__Date__c = trip?.StartDate.ToString();// Automatic from Trip
                    bls.enrtcr__Quantity__c = (int)(trip?.TotalKm ?? 0);//Actual Distance (Km) from Trip

                    bls.enrtcr__Support_Contract_Item__c = bl?.ServiceAgreementId ?? "";
                    bls.enrtcr__Support_Contract__c = bl?.ServiceAgreementItemId ?? "";
                    bls.enrtcr__Site__c = cslines?.SiteId ?? null;//site

                    bls.enrtcr__Support_CategoryId__c = cslines?.ServiceId ?? "";//service
                    bls.enrtcr__Site_Service_Program__c = cslines?.ServiceName ?? "";//Site Service Program;
                    bls.enrtcr__Rate__c = bl?.Rate.ToString();//ob.UnitOfMeasure.ToString();

                    bls.enrtcr__Worker__c = drivers?.SalesForceUserId;//worker
                    bls.enrtcr__Client_Rep_Accepted__c = true;//client rep accepted
                    bls.enrtcr__Use_Negotiated_Rate__c = true;//nogotitiated

                    bls.enrtcr__Negotiated_Rate_Ex_GST__c = (decimal)(00.00);//Nogotiated Rate GST
                    bls.enrtcr__Negotiated_Rate_GST__c = (decimal)(00.00);//Nogotiated Rate GST
                    result.Add(bls);
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
                        dbc.Drivers.Add(drv);
                    }
                    else
                    {
                        if (drv.Disabled == true)
                        {
                            existingrecord.Disabled = true;
                        }
                        else
                        {
                            existingrecord.FirstName = drv.FirstName;
                            existingrecord.LastName = drv.LastName;
                            existingrecord.CostCenter = drv.CostCenter;
                            existingrecord.Username = drv.Username;
                            existingrecord.Disabled = drv.Disabled;
                            existingrecord.Type = drv.Type;
                            existingrecord.JobDescription = drv.JobDescription;
                            existingrecord.Department = drv.Department;
                            existingrecord.ManagerName = drv.ManagerName;
                            existingrecord.IsPortalUser = drv.IsPortalUser;
                            existingrecord.JobNumber = drv.JobNumber;
                            existingrecord.PreferedName = drv.PreferedName;
                            if (existingrecord.SalesForceUserId != "" && drv.SalesForceUserId != "")
                                existingrecord.SalesForceUserId = drv.SalesForceUserId;
                        }

                    }
                    dbc.SaveChanges();

                }
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

                    }
                    dbc.SaveChanges();
                }

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
    }
}
