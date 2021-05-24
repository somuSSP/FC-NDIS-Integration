using FC_NDIS.ActionInterface;
using FC_NDIS.ApplicationIntegartionModels;
using FC_NDIS.DBAccess;
using FC_NDIS.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FC_NDIS.RestAPIModels;
using FC_NDIS.APIModels.BllingLines;
using RestSharp;
using FC_NDIS.APIModels.Patch;

namespace FC_NDIS.Action
{
    public class SFDCRestAPIAccess : ISFDC
    {
        public const string LoginEndpoint = "https://test.salesforce.com/services/oauth2/token";
        public const string ApiEndpoint = "/services/data/v36.0/"; //Use your org's version number

        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthToken { get; set; }
        public string ServiceUrl { get; set; }
        public ConfigurationBuilder _configurationBuilder = null;
        readonly HttpClient Client;

        private readonly IntegrationAppSettings _integrationAppSettings;
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public List<Customer> FinalCustomer;


        public SFDCRestAPIAccess(IntegrationAppSettings integrationAppSettings)
        {
            Client = new HttpClient();
            _configurationBuilder = new ConfigurationBuilder();
            this._integrationAppSettings = integrationAppSettings;
        }


        public bool IntegerateSfCustServiceLine()
        {
            bool result = false;
            DBAction dba = new DBAction(_integrationAppSettings);
            List<CustomerServiceLine> ltsCusline = new List<CustomerServiceLine>();


            Login();
            logger.Info("Scheduled Customer Service Line job triggered");
            var firstDownload = Convert.ToBoolean(_integrationAppSettings.FirstTimeDownload);
            string queryCustomer = "";
            if (firstDownload)
            {
                queryCustomer = @"SELECT Id
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
,enrtcr__Support_Category__c
,enrtcr__Category_Item__r.enrtcr__Support_Category_Amount__c
,enrtcr__Category_Item__r.enrtcr__Delivered__c
,enrtcr__Site__c
,enrtcr__Site__r.Name
,enrtcr__Site__r.enrtcr__Site_GL_Code__c
,enrtcr__Service__c
,enrtcr__Service__r.Name
,enrtcr__Service__r.enrtcr__Travel_Service__c
,enrtcr__Service__r.enrtcr__Transport_Service__c
,enrtcr__Site_Service_Program__c
FROM enrtcr__Support_Contract_Item__c
WHERE (enrtcr__Support_Contract__r.enrtcr__Status__c = 'Current'
AND (( enrtcr__Service__r.enrtcr__Allow_Non_Labour_Transport__c = true
AND enrtcr__Service__r.enrtcr__Transport_Service__c != null
AND (
(
enrtcr__Support_Contract__r.enrtcr__Funding_Type__c = 'NDIS'
AND enrtcr__Support_Contract__r.enrtcr__Transport_Non_Labour_Cost_Claims__c != 'Prevent'
)
OR enrtcr__Support_Contract__r.enrtcr__Funding_Type__c != 'NDIS'
)
)
OR
( enrtcr__Service__r.enrtcr__Allow_Non_Labour_Travel__c = true
AND enrtcr__Service__r.enrtcr__Travel_Service__c != null
AND (
(
enrtcr__Support_Contract__r.enrtcr__Funding_Type__c = 'NDIS'
AND enrtcr__Support_Contract__r.enrtcr__Travel_Non_Labour_Cost_Claims__c != 'Prevent'
)
OR enrtcr__Support_Contract__r.enrtcr__Funding_Type__c != 'NDIS'
)
))
)";
            }
            else
            {
                queryCustomer = @"SELECT Id
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
,enrtcr__Support_Category__c
,enrtcr__Category_Item__r.enrtcr__Support_Category_Amount__c
,enrtcr__Category_Item__r.enrtcr__Delivered__c
,enrtcr__Site__c
,enrtcr__Site__r.Name
,enrtcr__Site__r.enrtcr__Site_GL_Code__c
,enrtcr__Service__c
,enrtcr__Service__r.Name
,enrtcr__Service__r.enrtcr__Travel_Service__c
,enrtcr__Service__r.enrtcr__Transport_Service__c
,enrtcr__Site_Service_Program__c
FROM enrtcr__Support_Contract_Item__c
WHERE ((( enrtcr__Service__r.enrtcr__Allow_Non_Labour_Transport__c = true
AND enrtcr__Service__r.enrtcr__Transport_Service__c != null
AND (
(
enrtcr__Support_Contract__r.enrtcr__Funding_Type__c = 'NDIS'
AND enrtcr__Support_Contract__r.enrtcr__Transport_Non_Labour_Cost_Claims__c != 'Prevent'
)
OR enrtcr__Support_Contract__r.enrtcr__Funding_Type__c != 'NDIS'
)
)
OR
( enrtcr__Service__r.enrtcr__Allow_Non_Labour_Travel__c = true
AND enrtcr__Service__r.enrtcr__Travel_Service__c != null
AND (
(
enrtcr__Support_Contract__r.enrtcr__Funding_Type__c = 'NDIS'
AND enrtcr__Support_Contract__r.enrtcr__Travel_Non_Labour_Cost_Claims__c != 'Prevent'
)
OR enrtcr__Support_Contract__r.enrtcr__Funding_Type__c != 'NDIS'
)
))
)";
            }

            var APIResponse = QueryAllRecord(Client, queryCustomer);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var rootObject = JsonConvert.DeserializeObject<FC_NDIS.APIModels.CustomerServiceLine.Root>(APIResponse, settings);


            if (rootObject != null)
            {
                if (rootObject.records.Count() > 0)
                {
                    for (var i = 0; i <= rootObject.records.Count - 1; i++)
                    {
                        CustomerServiceLine csl = new CustomerServiceLine();
                        var customerId = rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Client__c;
                        csl.ServiceAgreementCustomerId = dba.GetCustomerId(customerId);
                        csl.ServiceAgreementId = rootObject.records[i].enrtcr__Support_Contract__c; ;
                        csl.ServiceAgreementName = rootObject.records[i].enrtcr__Support_Contract__r.Name;
                        csl.ServiceAgreementEndDate = Convert.ToDateTime(rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__End_Date__c);

                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Current")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Current;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Expired")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Expired;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Rollover")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Rollover;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Cancelled")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Cancelled;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Quote Submitted")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.QuoteSubmitted;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Client Declined")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.ClientDeclined;

                        csl.ServiceAgreementFundingManagement = rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Funding_Management__c;
                        csl.ServiceAgreementFundingType = rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Funding_Type__c;
                        csl.ServiceAgreementItemId = rootObject.records[i].Id;
                        csl.ServiceAgreementItemName = rootObject.records[i].Name;
                        if (rootObject.records[i]?.enrtcr__Category_Item__r?.enrtcr__Support_Category_Amount__c == null)
                            csl.SupportCategoryAmount = 0;
                        else
                            csl.SupportCategoryAmount = (float)rootObject.records[i].enrtcr__Category_Item__r?.enrtcr__Support_Category_Amount__c;
                        csl.SupportCategoryDelivered = (float?)rootObject.records[i]?.enrtcr__Category_Item__r?.enrtcr__Delivered__c ?? 0;
                        csl.FundsRemaining = (float?)rootObject.records[i].enrtcr__Remaining__c;

                        if (rootObject.records[i].enrtcr__Item_Overclaim__c == "Allow")
                            csl.ItemOverclaim = (int)ItemOverClaim.Allow;
                        if (rootObject.records[i].enrtcr__Item_Overclaim__c == "Warn")
                            csl.ItemOverclaim = (int)ItemOverClaim.Warn;
                        if (rootObject.records[i].enrtcr__Item_Overclaim__c == "Prevent")
                            csl.ItemOverclaim = (int)ItemOverClaim.Prevent;


                        csl.SiteId = rootObject.records[i].enrtcr__Site__c;
                        csl.SiteName = rootObject.records[i].enrtcr__Site__r.Name;
                        csl.SiteGlcode = rootObject.records[i].enrtcr__Site__r.enrtcr__Site_GL_Code__c;
                        csl.SiteServiceProgramId = rootObject.records[i]?.enrtcr__Site_Service_Program__c ?? "";
                        csl.ServiceId = rootObject.records[i].enrtcr__Service__c;
                        csl.ServiceName = rootObject.records[i].enrtcr__Service__r.Name;
                        csl.TravelServiceId = rootObject.records[i].enrtcr__Service__r.enrtcr__Travel_Service__c;
                        csl.TransportServiceId = rootObject.records[i].enrtcr__Service__r.enrtcr__Transport_Service__c == null ? "" : rootObject.records[i].enrtcr__Service__r.enrtcr__Transport_Service__c;
                        csl.CategoryItemId = rootObject.records[i].enrtcr__Support_Category__c;
                        csl.Default = false;
                        if (csl.ServiceAgreementCustomerId != 0)
                            ltsCusline.Add(csl);
                    }
                }
                if (ltsCusline.Count > 0)
                {
                    dba.IntegrateCustomerLineinfointoDB(ltsCusline);
                }
                if (rootObject.nextRecordsUrl != "" && rootObject.nextRecordsUrl != null)
                {
                    RemainingCustomerServiceLineRecord(rootObject.nextRecordsUrl);
                }
                result = true;
            }


            return result;
        }

        public bool IntegerateSfTransportRate()
        {
            bool result = false;
            List<SalesforceRate> ltsTransportRate = new List<SalesforceRate>();
            logger.Info("Scheduled Travel Rate job triggered");
            DBAction dba = new DBAction(_integrationAppSettings);
            Login();


            var queryCustomer = @"SELECT Id
                                                    ,enrtcr__Effective_Date__c
                                                    ,enrtcr__End_Date__c
                                                    ,Name
                                                    ,enrtcr__Service__c
                                                    ,enrtcr__Allow_Rate_Negotiation__c
                                                    ,enrtcr__Amount_Ex_GST__c
                                                    ,enrtcr__Quantity_Type__c
                                                FROM enrtcr__Rate__c
                                                WHERE enrtcr__Effective_Date__c <= TODAY
                                                    AND enrtcr__End_Date__c >= TODAY
                                                    AND (
                                                            (Name LIKE '%25WA%25' AND enrtcr__Funding_Type__c = 'NDIS')
                                                            OR enrtcr__Funding_Type__c != 'NDIS'
                                                        )
                                                    AND enrtcr__Service__c IN (
                                                        SELECT enrtcr__Transport_Service__c
                                                        FROM enrtcr__Service__c
                                                        WHERE enrtcr__Transport_Service__c != null
                                                            AND enrtcr__Allow_Non_Labour_Transport__c = true
                                                    )
                                                ORDER BY enrtcr__Service__c, enrtcr__Effective_Date__c desc
                                                ";
            var APIResponse = QueryAllRecord(Client, queryCustomer);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var rootObject = JsonConvert.DeserializeObject<FC_NDIS.APIModels.Rate.Root>(APIResponse, settings);

            if (rootObject != null)
            {
                if (rootObject.records.Count > 0)
                {
                    for (var i = 0; i <= rootObject.records.Count - 1; i++)
                    {
                        SalesforceRate tr = new SalesforceRate();
                        tr.RateId = rootObject.records[i].Id;
                        tr.StartDate = Convert.ToDateTime(rootObject.records[i].enrtcr__Effective_Date__c);
                        tr.EndDate = Convert.ToDateTime(rootObject.records[i].enrtcr__End_Date__c);
                        tr.RateName = rootObject.records[i].Name;
                        tr.ServiceId = rootObject.records[i].enrtcr__Service__c;
                        tr.Negotiation = rootObject.records[i].enrtcr__Allow_Rate_Negotiation__c;
                        tr.Rate = (float)rootObject.records[i].enrtcr__Amount_Ex_GST__c;
                        tr.RateType = 1;

                        tr.IsDeleted = false;
                        tr.CreatedDate = DateTime.Now;
                        tr.ModifiedDate = DateTime.Now;
                        ltsTransportRate.Add(tr);
                    }
                }
                //Insert record to Database
                if (rootObject.records.Count > 0)
                {
                    dba.IntegrateTravelandTransportRateInfotoDB(ltsTransportRate);
                    result = true;
                }
                else
                    result = true;
            }

            return result;
        }

        public bool IntegerateSfTravelRate()
        {
            bool result = false;
            List<SalesforceRate> ltsTransportRate = new List<SalesforceRate>();
            logger.Info("Scheduled Travel Rate job triggered");
            DBAction dba = new DBAction(_integrationAppSettings);
            Login();


            var queryCustomer = @"SELECT Id ,enrtcr__Effective_Date__c,enrtcr__End_Date__c,Name,enrtcr__Service__c,enrtcr__Allow_Rate_Negotiation__c,enrtcr__Amount_Ex_GST__c,enrtcr__Quantity_Type__c FROM enrtcr__Rate__c WHERE enrtcr__Effective_Date__c <= TODAY AND enrtcr__End_Date__c >= TODAY AND ((Name LIKE '%25WA%25' AND enrtcr__Funding_Type__c = 'NDIS') OR enrtcr__Funding_Type__c != 'NDIS') AND enrtcr__Service__c IN ( SELECT enrtcr__Travel_Service__c FROM enrtcr__Service__c WHERE enrtcr__Travel_Service__c != null AND enrtcr__Allow_Non_Labour_Travel__c = true )ORDER BY enrtcr__Service__c, enrtcr__Effective_Date__c desc";

            var APIResponse = QueryAllRecord(Client, queryCustomer);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var rootObject = JsonConvert.DeserializeObject<FC_NDIS.APIModels.Rate.Root>(APIResponse, settings);

            if (rootObject != null)
            {
                if (rootObject.records.Count > 0)
                {
                    for (var i = 0; i <= rootObject.records.Count - 1; i++)
                    {
                        SalesforceRate tr = new SalesforceRate();
                        tr.RateId = rootObject.records[i].Id;
                        tr.StartDate = Convert.ToDateTime(rootObject.records[i].enrtcr__Effective_Date__c);
                        tr.EndDate = Convert.ToDateTime(rootObject.records[i].enrtcr__End_Date__c);
                        tr.RateName = rootObject.records[i].Name;
                        tr.ServiceId = rootObject.records[i].enrtcr__Service__c;
                        tr.Negotiation = rootObject.records[i].enrtcr__Allow_Rate_Negotiation__c;
                        tr.Rate = (float)rootObject.records[i].enrtcr__Amount_Ex_GST__c;
                        tr.RateType = 1;

                        tr.IsDeleted = false;
                        tr.CreatedDate = DateTime.Now;
                        tr.ModifiedDate = DateTime.Now;
                        ltsTransportRate.Add(tr);
                    }
                }
                //Insert record to Database
                dba.IntegrateTravelandTransportRateInfotoDB(ltsTransportRate);
            }
            return result;
        }
        public bool IntegerateSfCustomeList()
        {
            Login();
            logger.Info("Integrate Customer Informations");
            var result = true;
            string queryCustomer = "";
            var firstDownload = Convert.ToBoolean(_integrationAppSettings.FirstTimeDownload);

            if (firstDownload)
            {
                queryCustomer = @"SELECT Id,Name,OtherStreet,OtherCity,OtherState,OtherPostalCode,RecordType.Name,Enrite_Care_Auto_Number__c,enrtcr__Status__c,LastModifiedDate FROM Contact WHERE RecordType.Name = 'Client' and enrtcr__Status__c='Current'";
            }
            else
            {
                queryCustomer = @"SELECT Id,Name,OtherStreet,OtherCity,OtherState,OtherPostalCode,RecordType.Name,Enrite_Care_Auto_Number__c,enrtcr__Status__c,LastModifiedDate FROM Contact WHERE RecordType.Name = 'Client' 
                       AND (enrtcr__Status__c='Current' OR enrtcr__Status__c='Deceased' OR enrtcr__Status__c='Inactive')";
            }

            var APIResponse = QueryAllRecord(Client, queryCustomer);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var rootObject = JsonConvert.DeserializeObject<FC_NDIS.RestAPIModels.Customer.Root>(APIResponse, settings);
            FinalCustomer = new List<Customer>();
            List<Customer> lstCus = new List<Customer>();
            if (rootObject != null)
            {
                if (rootObject.records.Count > 0)
                {
                    for (var i = 0; i <= rootObject.records.Count - 1; i++)
                    {
                        Customer cs = new Customer();
                        cs.CustomerId = rootObject.records[i].Id;
                        cs.Name = rootObject.records[i].Name;
                        cs.Street = rootObject.records[i].OtherStreet;
                        cs.City = rootObject.records[i].OtherCity;
                        cs.State = rootObject.records[i].OtherState;
                        cs.PostalCode = rootObject.records[i].OtherPostalCode;
                        cs.LumaryId = rootObject.records[i].Enrite_Care_Auto_Number__c;
                        if (rootObject.records[i].enrtcr__Status__c != null)
                        {
                            cs.Active = false;
                            if (rootObject.records[i].enrtcr__Status__c == "Current")
                            {
                                cs.Status = 1;
                            }
                            if (rootObject.records[i].enrtcr__Status__c == "Deceased")
                            {
                                cs.Status = 2;
                            }
                            if (rootObject.records[i].enrtcr__Status__c == "Inactive")
                            {
                                cs.Status = 3;
                            }
                        }
                        cs.Active = true;
                        cs.OnHold = false;
                        cs.CreatedDate = DateTime.Now;
                        cs.ModifiedDate = DateTime.Now;
                        lstCus.Add(cs);
                    }
                }
            }
            if (lstCus.Count > 0)
            {
                try
                {
                    DBAction dba = new DBAction(_integrationAppSettings);
                    dba.IntegrateCustomerInfotoDB(lstCus);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
            }

            if (rootObject.nextRecordsUrl != "" && rootObject.nextRecordsUrl != null)
            {
                RemainingRecord(rootObject.nextRecordsUrl);
            }


            return result;
        }

        public void RemainingRecord(string NextURL)
        {
            var APIResponse = QueryNextRecord(Client, NextURL);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var rootObject = JsonConvert.DeserializeObject<FC_NDIS.RestAPIModels.Customer.Root>(APIResponse, settings);

            List<Customer> lstCus = new List<Customer>();
            if (rootObject != null)
            {
                if (rootObject.records.Count > 0)
                {
                    for (var i = 0; i <= rootObject.records.Count - 1; i++)
                    {
                        Customer cs = new Customer();
                        cs.CustomerId = rootObject.records[i].Id;
                        cs.Name = rootObject.records[i].Name;
                        cs.Street = rootObject.records[i].OtherStreet;
                        cs.City = rootObject.records[i].OtherCity;
                        cs.State = rootObject.records[i].OtherState;
                        cs.PostalCode = rootObject.records[i].OtherPostalCode;
                        cs.LumaryId = rootObject.records[i].Enrite_Care_Auto_Number__c;
                        if (rootObject.records[i].enrtcr__Status__c != null)
                        {
                            cs.Active = false;
                            if (rootObject.records[i].enrtcr__Status__c == "Current")
                            {
                                cs.Status = 1;
                            }
                            if (rootObject.records[i].enrtcr__Status__c == "Deceased")
                            {
                                cs.Status = 2;
                            }
                            if (rootObject.records[i].enrtcr__Status__c == "Inactive")
                            {
                                cs.Status = 3;
                            }
                        }
                        cs.Active = true;
                        cs.OnHold = false;
                        lstCus.Add(cs);
                    }
                }
                DBAction dba = new DBAction(_integrationAppSettings);
                dba.IntegrateCustomerInfotoDB(lstCus);
                if (rootObject.nextRecordsUrl != "" && rootObject.nextRecordsUrl != null)
                {
                    RemainingRecord(rootObject.nextRecordsUrl);
                }
            }
        }

        public void RemainingCustomerServiceLineRecord(string NextURL)
        {
            DBAction dba = new DBAction(_integrationAppSettings);
            List<CustomerServiceLine> ltsCusline = new List<CustomerServiceLine>();
            var APIResponse = QueryNextRecord(Client, NextURL);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var rootObject = JsonConvert.DeserializeObject<FC_NDIS.APIModels.CustomerServiceLine.Root>(APIResponse, settings);

            List<Customer> lstCus = new List<Customer>();
            if (rootObject != null)
            {
                if (rootObject.records.Count > 0)
                {
                    for (var i = 0; i <= rootObject.records.Count - 1; i++)
                    {
                        CustomerServiceLine csl = new CustomerServiceLine();
                        var customerId = rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Client__c;
                        csl.ServiceAgreementCustomerId = dba.GetCustomerId(customerId);
                        csl.ServiceAgreementId = rootObject.records[i].enrtcr__Support_Contract__c; ;
                        csl.ServiceAgreementName = rootObject.records[i].enrtcr__Support_Contract__r.Name;
                        csl.ServiceAgreementEndDate = Convert.ToDateTime(rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__End_Date__c);

                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Current")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Current;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Expired")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Expired;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Rollover")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Rollover;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Cancelled")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Cancelled;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Quote Submitted")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.QuoteSubmitted;
                        if (rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Status__c == "Client Declined")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.ClientDeclined;

                        csl.ServiceAgreementFundingManagement = rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Funding_Management__c;
                        csl.ServiceAgreementFundingType = rootObject.records[i].enrtcr__Support_Contract__r.enrtcr__Funding_Type__c;
                        csl.ServiceAgreementItemId = rootObject.records[i].Id;
                        csl.ServiceAgreementItemName = rootObject.records[i].Name;
                        if (rootObject.records[i]?.enrtcr__Category_Item__r?.enrtcr__Support_Category_Amount__c == null)
                            csl.SupportCategoryAmount = 0;
                        else
                            csl.SupportCategoryAmount = (float)rootObject.records[i].enrtcr__Category_Item__r?.enrtcr__Support_Category_Amount__c;
                        csl.SupportCategoryDelivered = (float?)rootObject.records[i]?.enrtcr__Category_Item__r?.enrtcr__Delivered__c ?? 0;
                        csl.FundsRemaining = (float?)rootObject.records[i].enrtcr__Remaining__c;

                        if (rootObject.records[i].enrtcr__Item_Overclaim__c == "Allow")
                            csl.ItemOverclaim = (int)ItemOverClaim.Allow;
                        if (rootObject.records[i].enrtcr__Item_Overclaim__c == "Warn")
                            csl.ItemOverclaim = (int)ItemOverClaim.Warn;
                        if (rootObject.records[i].enrtcr__Item_Overclaim__c == "Prevent")
                            csl.ItemOverclaim = (int)ItemOverClaim.Prevent;


                        csl.SiteId = rootObject.records[i].enrtcr__Site__c;
                        csl.SiteName = rootObject.records[i].enrtcr__Site__r.Name;
                        csl.SiteGlcode = rootObject.records[i].enrtcr__Site__r.enrtcr__Site_GL_Code__c;
                        csl.SiteServiceProgramId = rootObject.records[i]?.enrtcr__Site_Service_Program__c ?? "";
                        csl.ServiceId = rootObject.records[i].enrtcr__Service__c;
                        csl.ServiceName = rootObject.records[i].enrtcr__Service__r.Name;
                        csl.TravelServiceId = rootObject.records[i].enrtcr__Service__r.enrtcr__Travel_Service__c;
                        csl.TransportServiceId = rootObject.records[i].enrtcr__Service__r.enrtcr__Transport_Service__c == null ? "" : rootObject.records[i].enrtcr__Service__r.enrtcr__Transport_Service__c;
                        csl.CategoryItemId = rootObject.records[i].enrtcr__Support_Category__c;
                        csl.Default = false;
                        if (csl.ServiceAgreementCustomerId != 0)
                            ltsCusline.Add(csl);
                    }
                }
                if (ltsCusline.Count > 0)
                {
                    dba.IntegrateCustomerLineinfointoDB(ltsCusline);
                }
                if (rootObject.nextRecordsUrl != "" && rootObject.nextRecordsUrl != null)
                {
                    RemainingCustomerServiceLineRecord(rootObject.nextRecordsUrl);
                }
            }
        }

        private string QueryAllRecord(HttpClient client, string queryMessage)
        {
            string restQuery = $"{ServiceUrl}{_integrationAppSettings.SFDCApiEndpoint}queryAll?q={queryMessage}";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthToken);
            HttpResponseMessage response = client.GetAsync(restQuery).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        private string QueryNextRecord(HttpClient client, string NextURL)
        {
            string restQuery = $"{ServiceUrl}" + NextURL;//{_integrationAppSettings.SFDCApiEndpoint}queryAll?q={queryMessage}";          
            HttpResponseMessage response = client.GetAsync(restQuery).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        private string CreateRecord(HttpClient client, string createMessage, string recordType)
        {
            HttpContent contentCreate = new StringContent(createMessage, Encoding.UTF8, "application/xml");
            string uri = $"{ServiceUrl}{_integrationAppSettings.SFDCApiEndpoint}sobjects/{recordType}";
            var clienttest = new RestClient(uri);
            clienttest.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + AuthToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", createMessage, ParameterType.RequestBody);
            IRestResponse responses = clienttest.Execute(request);
            return responses.Content;
        }

        private string CreatePatchRecord(HttpClient clients, string createMessage, string entpointURL)
        {
            string uri = $"{ServiceUrl}{entpointURL}";
            var client = new RestClient(uri);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + AuthToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", createMessage, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }


        public bool Login()
        {
            logger.Info("Login Method Triggered");

            try
            {
                HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                  {
                      {"grant_type", "password"},
                      {"client_id", _integrationAppSettings.SFDCClientId},
                      {"client_secret", _integrationAppSettings.SFDCClientSecret},
                      {"username",  _integrationAppSettings.SFDCUserName},
                      {"password", _integrationAppSettings.SFDCUserPassword}
                  });

                HttpResponseMessage message = Client.PostAsync(_integrationAppSettings.SFDCLoginEndpoint, content).Result;

                string response = message.Content.ReadAsStringAsync().Result;
                JObject obj = JObject.Parse(response);

                AuthToken = (string)obj["access_token"];
                ServiceUrl = (string)obj["instance_url"];

                logger.Info("Login Method successfully completed");
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("Issue occured in the Login Method .Issue:" + ex.Message.ToString());
                return false;
            }
        }

        public bool IntegrateSFDCId_OperatortoDB(string Usernames)
        {
            bool result = false;
            DBAction dba = new DBAction(_integrationAppSettings);
            List<ClsUsersList> ObjList = new List<ClsUsersList>();
            logger.Info("Scheduled SFDC ID to Drivers");
            Login();
            var queryCustomer = @"Select Id,EmployeeNumber,UserRoleID,IsActive,Username,CompanyName From User WHERE Username IN (" + Usernames + ")";
            HttpClient cl = new HttpClient();
            cl.Timeout = new TimeSpan(0, 5, 0);
            var APIResponse = QueryAllRecord(cl, queryCustomer);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var rootObject = JsonConvert.DeserializeObject<FC_NDIS.APIModels.SFDCtoDriver.Root>(APIResponse, settings);

            List<Customer> lstCus = new List<Customer>();
            if (rootObject.records != null)
            {
                if (rootObject.records != null)
                {
                    if (rootObject.records.Count > 0)
                    {
                        for (int i = 0; i <= rootObject.records.Count - 1; i++)
                        {
                            ClsUsersList Obj = new ClsUsersList();
                            Obj.Id = rootObject.records[i].Id;
                            Obj.IsActive = rootObject.records[i].IsActive;
                            Obj.UserName = rootObject.records[i].Username;
                            Obj.UserRoleID = rootObject.records[i].UserRoleId;
                            ObjList.Add(Obj);
                        }
                        dba.IntegrateAllDriver(ObjList);
                        result = true;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool InsertDataintoSFDC()
        {
            bool Result = false;
            var bllist = GetBillingInformation();
            logger.Info("Insert Data into SFDC");

            DBAction dba = new DBAction(_integrationAppSettings);

            FC_NDIS.APIModels.Patch.Root PatchRoot = new APIModels.Patch.Root();
            PatchRoot.batchRequests = new List<APIModels.Patch.BatchRequest>();
            Login();
            for (int i = 0; i < bllist.Count; i = i + 25)
            {
                var items = bllist.Skip(i).Take(25).ToList();
                foreach (var bl in items)
                {
                    APIModels.Patch.BatchRequest br = new APIModels.Patch.BatchRequest();
                    br.method = "POST";
                    br.url = "v36.0/sobjects/enrtcr__Support_Delivered__c";
                    br.richInput = new RichInput();
                    br.richInput.Batch_Created__c = true;
                    br.richInput.enrtcr__Client__c = bl.enrtcr__Client__c;
                    br.richInput.enrtcr__Date__c = Convert.ToDateTime(bl.enrtcr__Date__c).ToString("yyyy-MM-dd");
                    br.richInput.enrtcr__Quantity__c = bl.enrtcr__Quantity__c;
                    br.richInput.enrtcr__Support_Contract_Item__c = bl.enrtcr__Support_Contract_Item__c;
                    br.richInput.enrtcr__Support_Contract__c = bl.enrtcr__Support_Contract__c;
                    br.richInput.enrtcr__Site__c = bl.enrtcr__Site__c;
                    br.richInput.enrtcr__Support_CategoryId__c = bl.enrtcr__Support_CategoryId__c;
                    br.richInput.enrtcr__Site_Service_Program__c = bl.enrtcr__Site_Service_Program__c;
                    br.richInput.enrtcr__Rate__c = bl.enrtcr__Rate__c;
                    br.richInput.enrtcr__Worker__c = bl.enrtcr__Worker__c;
                    br.richInput.enrtcr__Client_Rep_Accepted__c = true;
                    br.richInput.enrtcr__Use_Negotiated_Rate__c = true;
                    br.richInput.enrtcr__Negotiated_Rate_Ex_GST__c = bl.enrtcr__Negotiated_Rate_Ex_GST__c;
                    br.richInput.enrtcr__Negotiated_Rate_GST__c = bl.enrtcr__Negotiated_Rate_GST__c;
                    PatchRoot.batchRequests.Add(br);
                }
                if (PatchRoot.batchRequests.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(PatchRoot);
                    var response = CreatePatchRecord(Client, json, _integrationAppSettings.SFDCApiEndpoint + "composite/batch/");
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    var rootObject = JsonConvert.DeserializeObject<FC_NDIS.APIModels.AccessResult.Root>(response, settings);
                    if (!rootObject.hasErrors)
                    {
                        int recordcount = 0;
                        foreach (dynamic res in rootObject.results)
                        {
                            string errorCode = "";
                            string message = "";
                            int statusCode = 0;
                            statusCode = res.statusCode;
                            if (statusCode == 201)
                            {
                                dba.SFDCActionStatus(items[recordcount].BillingID, true, "Success", (string)res.result.id);
                            }
                            else
                            {
                                errorCode = res.result.errorCode;
                                message = res.result.message;
                                string ErrorMessage = "statusCode:" + statusCode.ToString() + ", Error Code :" + errorCode.ToString() + ", Message:" + message;
                                dba.SFDCActionStatus(items[recordcount].BillingID, false, ErrorMessage, "");
                            }
                            recordcount++;
                        }
                        Result = true;
                    }
                    else
                    {
                        int recordcount = 0;
                        foreach (dynamic res in rootObject.results)
                        {

                            string errorCode = "";
                            string message = "";
                            int statusCode = 0;
                            statusCode = res.statusCode;
                            if (statusCode == 201)
                            {
                                dba.SFDCActionStatus(items[recordcount].BillingID, true, "Success", (string)res.result.id);
                            }
                            else
                            {
                                errorCode = res.result.errorCode;
                                message = res.result.message;
                                string ErrorMessage = "statusCode:" + statusCode.ToString() + ", Error Code :" + errorCode.ToString() + ", Message:" + message;
                                dba.SFDCActionStatus(items[recordcount].BillingID, false, ErrorMessage, "");
                            }

                            recordcount++;
                        }
                        Result = true;
                    }
                }
            }


            return Result; ;
        }


        public bool InsertDataintoSFDCFromPortal(List<int> BillingIds)
        {
            bool Result = false;
            var bllist = GetBillingInformationUsingIds(BillingIds);
            logger.Info("Insert Data into SFDC");

            DBAction dba = new DBAction(_integrationAppSettings);

            FC_NDIS.APIModels.Patch.Root PatchRoot = new APIModels.Patch.Root();
            PatchRoot.batchRequests = new List<APIModels.Patch.BatchRequest>();
            Login();
            for (int i = 0; i < bllist.Count; i = i + 25)
            {
                var items = bllist.Skip(i).Take(25).ToList();
                foreach (var bl in items)
                {
                    APIModels.Patch.BatchRequest br = new APIModels.Patch.BatchRequest();
                    br.method = "POST";
                    br.url = "v36.0/sobjects/enrtcr__Support_Delivered__c";
                    br.richInput = new RichInput();
                    br.richInput.Batch_Created__c = true;
                    br.richInput.enrtcr__Client__c = bl.enrtcr__Client__c;
                    br.richInput.enrtcr__Date__c = Convert.ToDateTime(bl.enrtcr__Date__c).ToString("yyyy-MM-dd");
                    br.richInput.enrtcr__Quantity__c = bl.enrtcr__Quantity__c;
                    br.richInput.enrtcr__Support_Contract_Item__c = bl.enrtcr__Support_Contract_Item__c;
                    br.richInput.enrtcr__Support_Contract__c = bl.enrtcr__Support_Contract__c;
                    br.richInput.enrtcr__Site__c = bl.enrtcr__Site__c;
                    br.richInput.enrtcr__Support_CategoryId__c = bl.enrtcr__Support_CategoryId__c;
                    br.richInput.enrtcr__Site_Service_Program__c = bl.enrtcr__Site_Service_Program__c;
                    br.richInput.enrtcr__Rate__c = bl.enrtcr__Rate__c;
                    br.richInput.enrtcr__Worker__c = bl.enrtcr__Worker__c;
                    br.richInput.enrtcr__Client_Rep_Accepted__c = true;
                    br.richInput.enrtcr__Use_Negotiated_Rate__c = true;
                    br.richInput.enrtcr__Negotiated_Rate_Ex_GST__c = bl.enrtcr__Negotiated_Rate_Ex_GST__c;
                    br.richInput.enrtcr__Negotiated_Rate_GST__c = bl.enrtcr__Negotiated_Rate_GST__c;
                    PatchRoot.batchRequests.Add(br);
                }
                if (PatchRoot.batchRequests.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(PatchRoot);
                    var response = CreatePatchRecord(Client, json, _integrationAppSettings.SFDCApiEndpoint + "composite/batch/");
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    var rootObject = JsonConvert.DeserializeObject<FC_NDIS.APIModels.AccessResult.Root>(response, settings);
                    if (!rootObject.hasErrors)
                    {
                        int recordcount = 0;
                        foreach (dynamic res in rootObject.results)
                        {
                            string errorCode = "";
                            string message = "";
                            int statusCode = 0;
                            statusCode = res.statusCode;
                            if (statusCode == 201)
                            {
                                dba.SFDCActionStatus(items[recordcount].BillingID, true, "Success", (string)res.result.id);
                            }
                            else
                            {
                                errorCode = res.result.errorCode;
                                message = res.result.message;
                                string ErrorMessage = "statusCode:" + statusCode.ToString() + ", Error Code :" + errorCode.ToString() + ", Message:" + message;
                                dba.SFDCActionStatus(items[recordcount].BillingID, false, ErrorMessage, "");
                            }
                            recordcount++;
                        }
                        Result = true;
                    }
                    else
                    {
                        int recordcount = 0;
                        foreach (dynamic res in rootObject.results)
                        {

                            string errorCode = "";
                            string message = "";
                            int statusCode = 0;
                            statusCode = res.statusCode;
                            if (statusCode == 201)
                            {
                                dba.SFDCActionStatus(items[recordcount].BillingID, true, "Success", (string)res.result.id);
                            }
                            else
                            {
                                errorCode = res.result.errorCode;
                                message = res.result.message;
                                string ErrorMessage = "statusCode:" + statusCode.ToString() + ", Error Code :" + errorCode.ToString() + ", Message:" + message;
                                dba.SFDCActionStatus(items[recordcount].BillingID, false, ErrorMessage, "");
                            }

                            recordcount++;
                        }
                        Result = true;
                    }
                }
            }


            return Result; ;
        }

        public List<FC_NDIS.JsonModels.SFDCBillingLines> GetBillingInformation()
        {
            DBAction dba = new DBAction(_integrationAppSettings);
            var res = dba.GetBillingInformation();
            return res;
        }
        public List<FC_NDIS.JsonModels.SFDCBillingLines> GetBillingInformationUsingIds(List<int> BillingIds)
        {
            DBAction dba = new DBAction(_integrationAppSettings);
            var res = dba.GetBillingInformationusingIds(BillingIds);
            return res;
        }


        public List<string> GetAllDriverInfo_NotMappedSFDC()
        {
            DBAction dba = new DBAction(_integrationAppSettings);
            return dba.GetDriverInformationIsnotMappedSFDC();
        }

        public enum ItemOverClaim
        {
            Allow = 1, Warn = 2, Prevent = 3
        }

        public enum CustomerStatus
        {
            Current = 1,
            Expired = 2,
            Rollover = 3,
            Cancelled = 4,
            QuoteSubmitted = 5,
            ClientDeclined = 6
        }
        public enum ItemServiceAgreement
        {
            Current = 1, Expired = 2
        }
    }
}
