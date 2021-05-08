using FC_NDIS.ActionInterface;
using FC_NDIS.ApplicationIntegartionModels;
using FC_NDIS.DBAccess;
using FC_NDIS.Models;
using Microsoft.Extensions.Configuration;
using NLog;
using SfServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FC_NDIS.Action
{
    public class SFDCAction : ISFDC
    {
        public string URL = "";
        public ConfigurationBuilder _configurationBuilder = null;
        private string sessionId = null;
        private string serverUrl = null;
        private object createClient;
        private readonly IntegrationAppSettings _integrationAppSettings;
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public SFDCAction(IntegrationAppSettings integrationAppSettings)
        {
            _configurationBuilder = new ConfigurationBuilder();
            this._integrationAppSettings = integrationAppSettings;
        }

        //public bool IntegerateSfCustServiceLine(string userName, string password)
        //{
        //    logger.Info("Scheduled Customer Service Line job triggered");
        //    DBAction dba = new DBAction(_integrationAppSettings);
        //    string filterstring = dba.GetSalesforceInformation().Replace("\n", "");
        //    bool result = false;
        //    List<CustomerServiceLine> ltsCusline = new List<CustomerServiceLine>();

        //    SoapClient ss = new SoapClient();
        //    LoginResult lr = new LoginResult();
        //    LoginScopeHeader LH = new LoginScopeHeader();
        //    if (sessionId == null | sessionId == "")
        //    {
        //        lr = ss.login(null, userName, password);
        //        if (!lr.passwordExpired)
        //        {
        //            sessionId = lr.sessionId.ToString().Trim();
        //            serverUrl = lr.serverUrl.ToString().Trim();
        //        }
        //    }

        //    // Store SessionId in SessionHeader; We will need while making query() call
        //    SessionHeader sHeader = new SessionHeader();
        //    sHeader.sessionId = sessionId;

        //    // Variable to store query results
        //    QueryResult qr = new QueryResult();
        //    SoapClient ss1 = new SoapClient();
        //    ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);

        //    var lastintegratedDate = dba.GetLastintegratedDateandTime("Last_CustomerLineIntegrate");

        //    ss1.query(sHeader, null, null, null, @"SELECT Id
        //                                            ,Name
        //                                            ,enrtcr__Remaining__c
        //                                            ,enrtcr__Item_Overclaim__c
        //                                            ,enrtcr__Support_Contract__c
        //                                            ,enrtcr__Support_Contract__r.Name
        //                                            ,enrtcr__Support_Contract__r.enrtcr__End_Date__c
        //                                            ,enrtcr__Support_Contract__r.enrtcr__Status__c
        //                                            ,enrtcr__Support_Contract__r.enrtcr__Funding_Type__c
        //                                            ,enrtcr__Support_Contract__r.enrtcr__Funding_Management__c
        //                                            ,enrtcr__Support_Contract__r.enrtcr__Client__c
        //                                            ,enrtcr__Category_Item__r.enrtcr__Support_Category_Amount__c
        //                                            ,enrtcr__Category_Item__r.enrtcr__Delivered__c
        //                                            ,enrtcr__Site__c
        //                                            ,enrtcr__Site__r.Name
        //                                            ,enrtcr__Site__r.enrtcr__Site_GL_Code__c
        //                                            ,enrtcr__Service__c
        //                                            ,enrtcr__Service__r.Name
        //                                            ,enrtcr__Site_Service_Program__c
        //                                            ,enrtcr__Rate__c
        //                                            ,enrtcr__Rate__r.Name
        //                                            ,enrtcr__Rate__r.enrtcr__Amount_Ex_GST__c
        //                                            ,enrtcr__Rate__r.enrtcr__Quantity_Type__c
        //                                            ,enrtcr__Rate__r.enrtcr__Allow_Rate_Negotiation__c
        //                                            FROM enrtcr__Support_Contract_Item__c
        //                                            WHERE(" + filterstring + ") AND enrtcr__Support_Contract__r.enrtcr__Status__c = 'Current' ", out qr);



        //    sObject[] records = qr.records;

        //    if (records != null)
        //    {
        //        if (records.Length > 0)
        //        {
        //            for (var i = 0; i <= records.Length - 1; i++)
        //            {
        //                CustomerServiceLine csl = new CustomerServiceLine();
        //                var customerId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Client__c;
        //                csl.ServiceAgreementCustomerId = dba.GetCustomerId(customerId);
        //                csl.ServiceAgreementId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__c; ;
        //                csl.ServiceAgreementName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.Name;
        //                csl.ServiceAgreementEndDate = Convert.ToDateTime(((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__End_Date__c);

        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Current")
        //                    csl.ServiceAgreementStatus = (int)CustomerStatus.Current;
        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Expired")
        //                    csl.ServiceAgreementStatus = (int)CustomerStatus.Expired;
        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Rollover")
        //                    csl.ServiceAgreementStatus = (int)CustomerStatus.Rollover;
        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Cancelled")
        //                    csl.ServiceAgreementStatus = (int)CustomerStatus.Cancelled;
        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Quote Submitted")
        //                    csl.ServiceAgreementStatus = (int)CustomerStatus.QuoteSubmitted;
        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Client Declined")
        //                    csl.ServiceAgreementStatus = (int)CustomerStatus.ClientDeclined;

        //                csl.ServiceAgreementFundingManagement = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Funding_Management__c;
        //                csl.ServiceAgreementFundingType = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Funding_Type__c;
        //                csl.ServiceAgreementItemId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).Id;
        //                csl.ServiceAgreementItemName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).Name;
        //                csl.SupportCategoryAmount = (float)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Category_Item__r.enrtcr__Support_Category_Amount__c;
        //                csl.SupportCategoryDelivered = (float?)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Category_Item__r.enrtcr__Delivered__c;
        //                csl.FundsRemaining = (float?)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Remaining__c;

        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Item_Overclaim__c == "Allow")
        //                    csl.ItemOverclaim = (int)ItemOverClaim.Allow;
        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Item_Overclaim__c == "Warn")
        //                    csl.ItemOverclaim = (int)ItemOverClaim.Warn;
        //                if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Item_Overclaim__c == "Prevent")
        //                    csl.ItemOverclaim = (int)ItemOverClaim.Prevent;


        //                csl.SiteId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site__c;
        //                csl.SiteName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site__r.Name;
        //                // csl.SiteGlCode = "";
        //                csl.SiteServiceProgramId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site_Service_Program__c;
        //                csl.ServiceId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__c;
        //                csl.ServiceName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__r.Name;
        //                // csl.RateId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__c;
        //                // csl.RateName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.Name;
        //                //csl.RateAmount = (float?)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Amount_Ex_GST__c;
        //                // csl.RateType = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Quantity_Type__c;
        //                // csl.AllowRateNegotiation = Convert.ToBoolean(((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Allow_Rate_Negotiation__c == null ? false : true);
        //                csl.Default = false;
        //                ltsCusline.Add(csl);
        //            }
        //            //Insert record to Database

        //            dba.IntegrateCustomerLineinfointoDB(ltsCusline);
        //        }
        //        result = true;
        //    }
        //    return result;
        //}

        public bool IntegerateSfCustServiceLine(string userName, string password)
        {
            logger.Info("Scheduled Customer Service Line job triggered");
            DBAction dba = new DBAction(_integrationAppSettings);
            string filterstring = dba.GetSalesforceInformation().Replace("\n", "");
            bool result = false;
            List<CustomerServiceLine> ltsCusline = new List<CustomerServiceLine>();

            SoapClient ss = new SoapClient();
            LoginResult lr = new LoginResult();
            LoginScopeHeader LH = new LoginScopeHeader();
            if (sessionId == null | sessionId == "")
            {
                lr = ss.login(null, userName, password);
                if (!lr.passwordExpired)
                {
                    sessionId = lr.sessionId.ToString().Trim();
                    serverUrl = lr.serverUrl.ToString().Trim();
                }
            }

            // Store SessionId in SessionHeader; We will need while making query() call
            SessionHeader sHeader = new SessionHeader();
            sHeader.sessionId = sessionId;

            // Variable to store query results
            QueryResult qr = new QueryResult();
            SoapClient ss1 = new SoapClient();
            ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);

            var lastintegratedDate = dba.GetLastintegratedDateandTime("Last_CustomerLineIntegrate");

            ss1.query(sHeader, null, null, null, @"SELECT Id
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
                                                WHERE (
                                                        ( enrtcr__Service__r.enrtcr__Allow_Non_Labour_Transport__c = true
                                                        AND enrtcr__Service__r.enrtcr__Transport_Service__c != null
                                                        AND (
                                                                (
                                                                enrtcr__Support_Contract__r.enrtcr__Funding_Type__c = 'NDIS'                                                               
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
                                                                )
                                                                OR enrtcr__Support_Contract__r.enrtcr__Funding_Type__c != 'NDIS'
                                                           )
                                                        )
                                                    )
                                                ", out qr);



            sObject[] records = qr.records;

            if (records != null)
            {
                if (records.Length > 0)
                {
                    for (var i = 0; i <= records.Length - 1; i++)
                    {
                        CustomerServiceLine csl = new CustomerServiceLine();
                        var customerId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Client__c;
                        csl.ServiceAgreementCustomerId = dba.GetCustomerId(customerId);
                        csl.ServiceAgreementId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__c; ;
                        csl.ServiceAgreementName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.Name;
                        csl.ServiceAgreementEndDate = Convert.ToDateTime(((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__End_Date__c);

                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Current")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Current;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Expired")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Expired;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Rollover")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Rollover;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Cancelled")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.Cancelled;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Quote Submitted")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.QuoteSubmitted;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Status__c == "Client Declined")
                            csl.ServiceAgreementStatus = (int)CustomerStatus.ClientDeclined;

                        csl.ServiceAgreementFundingManagement = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Funding_Management__c;
                        csl.ServiceAgreementFundingType = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Support_Contract__r.enrtcr__Funding_Type__c;
                        csl.ServiceAgreementItemId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).Id;
                        csl.ServiceAgreementItemName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).Name;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i])?.enrtcr__Category_Item__r?.enrtcr__Support_Category_Amount__c == null)
                            csl.SupportCategoryAmount = 0;
                        else
                        csl.SupportCategoryAmount = (float)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Category_Item__r.enrtcr__Support_Category_Amount__c;
                        csl.SupportCategoryDelivered = (float?)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i])?.enrtcr__Category_Item__r.enrtcr__Delivered__c??0;
                        csl.FundsRemaining = (float?)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Remaining__c;

                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Item_Overclaim__c == "Allow")
                            csl.ItemOverclaim = (int)ItemOverClaim.Allow;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Item_Overclaim__c == "Warn")
                            csl.ItemOverclaim = (int)ItemOverClaim.Warn;
                        if (((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Item_Overclaim__c == "Prevent")
                            csl.ItemOverclaim = (int)ItemOverClaim.Prevent;


                        csl.SiteId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site__c;
                        csl.SiteName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site__r.Name;
                        // csl.SiteGlCode = "";
                        csl.SiteServiceProgramId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Site_Service_Program__c;
                        csl.ServiceId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__c;
                        csl.ServiceName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__r.Name;
                        csl.TravelServiceId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__r.enrtcr__Travel_Service__c;
                        csl.TransportServiceId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__r.enrtcr__Transport_Service__c==null?"": ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Service__r.enrtcr__Transport_Service__c;
                       
                        // csl.RateId = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__c;
                        // csl.RateName = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.Name;
                        //csl.RateAmount = (float?)((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Amount_Ex_GST__c;
                        // csl.RateType = ((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Quantity_Type__c;
                        // csl.AllowRateNegotiation = Convert.ToBoolean(((SfServiceRef.enrtcr__Support_Contract_Item__c)qr.records[i]).enrtcr__Rate__r.enrtcr__Allow_Rate_Negotiation__c == null ? false : true);
                        csl.Default = false;
                        ltsCusline.Add(csl);
                    }
                    //Insert record to Database

                    dba.IntegrateCustomerLineinfointoDB(ltsCusline);
                }
                result = true;
            }
            return result;
        }

        public bool IntegerateSfTransportRate(string userName, string password)
        {
            bool result = false;

            logger.Info("Scheduled Transport Rate job triggered");
            DBAction dba = new DBAction(_integrationAppSettings);
                     
            List<SalesforceRate> ltsTransportRate = new List<SalesforceRate>();

            SoapClient ss = new SoapClient();
            LoginResult lr = new LoginResult();
            LoginScopeHeader LH = new LoginScopeHeader();
            if (sessionId == null | sessionId == "")
            {
                lr = ss.login(null, userName, password);
                if (!lr.passwordExpired)
                {
                    sessionId = lr.sessionId.ToString().Trim();
                    serverUrl = lr.serverUrl.ToString().Trim();
                }
            }

            // Store SessionId in SessionHeader; We will need while making query() call
            SessionHeader sHeader = new SessionHeader();
            sHeader.sessionId = sessionId;

            // Variable to store query results
            QueryResult qr = new QueryResult();
            SoapClient ss1 = new SoapClient();
            ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);

           

            ss1.query(sHeader, null, null, null, @"SELECT Id
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
                                                            (Name LIKE '%WA%' AND enrtcr__Funding_Type__c = 'NDIS')
                                                            OR enrtcr__Funding_Type__c != 'NDIS'
                                                        )
                                                    AND enrtcr__Service__c IN (
                                                        SELECT enrtcr__Transport_Service__c
                                                        FROM enrtcr__Service__c
                                                        WHERE enrtcr__Transport_Service__c != null
                                                            AND enrtcr__Allow_Non_Labour_Transport__c = true
                                                    )
                                                ORDER BY enrtcr__Service__c, enrtcr__Effective_Date__c desc
                                                    ", out qr);

            sObject[] records = qr.records;

            if (records != null)
            {
                if (records.Length > 0)
                {
                    for (var i = 0; i <= records.Length - 1; i++)
                    {
                        SalesforceRate tr = new SalesforceRate ();
                        tr.RateId = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).Id;
                        tr.StartDate = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Effective_Date__c;
                        tr.EndDate = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__End_Date__c;
                        tr.RateName = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).Name;
                        tr.ServiceId = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Service__c;
                        tr.Negotiation = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Allow_Rate_Negotiation__c;
                        tr.PostalCode ="";
                        tr.Rate = (float)((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Amount_Ex_GST__c;
                        tr.RateType = 1;

                        tr.IsDeleted = false;
                        tr.CreatedDate = DateTime.Now;
                        tr.ModifiedDate = DateTime.Now;
                        ltsTransportRate.Add(tr);
                    }
                    //Insert record to Database
                    dba.IntegrateTravelandTransportRateInfotoDB(ltsTransportRate);


                }
                result = true;
            }          

            return result;
        }

        public bool IntegerateSfTravelRate(string userName, string password)
        {
            bool result = false;

            logger.Info("Scheduled Travel Rate job triggered");
            DBAction dba = new DBAction(_integrationAppSettings);

            List<SalesforceRate> ltsTransportRate = new List<SalesforceRate>();

            SoapClient ss = new SoapClient();
            LoginResult lr = new LoginResult();
            LoginScopeHeader LH = new LoginScopeHeader();
            if (sessionId == null | sessionId == "")
            {
                lr = ss.login(null, userName, password);
                if (!lr.passwordExpired)
                {
                    sessionId = lr.sessionId.ToString().Trim();
                    serverUrl = lr.serverUrl.ToString().Trim();
                }
            }

            // Store SessionId in SessionHeader; We will need while making query() call
            SessionHeader sHeader = new SessionHeader();
            sHeader.sessionId = sessionId;

            // Variable to store query results
            QueryResult qr = new QueryResult();
            SoapClient ss1 = new SoapClient();
            ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);

            var lastintegratedDate = dba.GetLastintegratedDateandTime("Last_CustomerLineIntegrate");

            ss1.query(sHeader, null, null, null, @"SELECT Id
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
                                                        (Name LIKE '%WA%' AND enrtcr__Funding_Type__c = 'NDIS')
                                                        OR enrtcr__Funding_Type__c != 'NDIS'
                                                    )
                                                    AND enrtcr__Service__c IN (
                                                        SELECT enrtcr__Travel_Service__c
                                                        FROM enrtcr__Service__c
                                                        WHERE enrtcr__Travel_Service__c != null
                                                            AND enrtcr__Allow_Non_Labour_Travel__c = true
                                                    )
                                                ORDER BY enrtcr__Service__c, enrtcr__Effective_Date__c desc
                                                ", out qr);



            sObject[] records = qr.records;

            if (records != null)
            {
                if (records.Length > 0)
                {
                    for (var i = 0; i <= records.Length - 1; i++)
                    {
                        SalesforceRate tr = new SalesforceRate();
                        tr.RateId = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).Id;
                        tr.StartDate = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Effective_Date__c;
                        tr.EndDate = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__End_Date__c;
                        tr.RateName = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).Name;
                        tr.ServiceId = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Service__c;
                        tr.Negotiation = ((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Allow_Rate_Negotiation__c;
                        tr.PostalCode = "";
                        tr.Rate = (float)((SfServiceRef.enrtcr__Rate__c)qr.records[i]).enrtcr__Amount_Ex_GST__c;
                        tr.RateType = 1;

                        tr.IsDeleted = false;
                        tr.CreatedDate = DateTime.Now;
                        tr.ModifiedDate = DateTime.Now;
                        ltsTransportRate.Add(tr);
                    }
                    //Insert record to Database
                    dba.IntegrateTravelandTransportRateInfotoDB(ltsTransportRate);

                }
                result = true;
            }
           
            return result;
        }
        public bool IntegerateSfCustomeList(string userName, string password)
        {
            logger.Info("Scheduled Customer List job triggered");
            List<Customer> lstCus = new List<Customer>();
            bool result = false;

            SoapClient ss = new SoapClient();
            LoginResult lr = new LoginResult();
            LoginScopeHeader LH = new LoginScopeHeader();
            DBAction dba = new DBAction(_integrationAppSettings);
            var lastintegratedDate = dba.GetLastintegratedDateandTime("Last_CustomerIntegrate");
            if (sessionId == null | sessionId == "")
            {
                lr = ss.login(null, userName, password);
                if (!lr.passwordExpired)
                {
                    sessionId = lr.sessionId.ToString().Trim();
                    serverUrl = lr.serverUrl.ToString().Trim();
                    // Store SessionId in SessionHeader; We will need while making query() call
                    SessionHeader sHeader = new SessionHeader();
                    sHeader.sessionId = sessionId;

                    // Variable to store query results
                    QueryResult qr = new QueryResult();
                    SoapClient ss1 = new SoapClient();
                    ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);

                    //  if (lastintegratedDate == null)
                    ss1.query(sHeader, null, null, null, @"SELECT Id,Name,OtherStreet,OtherCity,OtherState,OtherPostalCode,RecordType.Name,Enrite_Care_Auto_Number__c,enrtcr__Status__c,LastModifiedDate FROM Contact WHERE RecordType.Name = 'Client' 
                        AND (enrtcr__Status__c='Current' OR enrtcr__Status__c='Deceased' OR enrtcr__Status__c='Inactive')", out qr);
                    //else
                    //{
                    //    var UTCDate = Convert.ToDateTime(lastintegratedDate);
                    //    var UTCTime = UTCDate.ToString("yyyy-MM-dd") + "T" + UTCDate.ToString("HH:mm:ss") + "Z";

                    //    ss1.query(sHeader, null, null, null, @"SELECT Id,Name,OtherStreet,OtherCity,OtherState,OtherPostalCode,RecordType.Name,Enrite_Care_Auto_Number__c,enrtcr__Status__c,LastModifiedDate FROM Contact WHERE RecordType.Name = 'Client' 
                    //    AND (enrtcr__Status__c='Current' OR enrtcr__Status__c='Deceased' OR enrtcr__Status__c='Inactive') and LastModifiedDate >YESTERDAY", out qr);

                    //}
                    sObject[] records = qr.records;
                    if (records != null)
                    {
                        if (records.Length > 0)
                        {
                            for (var i = 0; i <= records.Length - 1; i++)
                            {
                                Customer cs = new Customer();
                                cs.CustomerId = ((SfServiceRef.Contact)qr.records[i]).Id;
                                cs.Name = ((SfServiceRef.Contact)qr.records[i]).Name;
                                cs.Street = ((SfServiceRef.Contact)qr.records[i]).OtherStreet;
                                cs.City = ((SfServiceRef.Contact)qr.records[i]).OtherCity;
                                cs.State = ((SfServiceRef.Contact)qr.records[i]).OtherState;
                                cs.PostalCode = ((SfServiceRef.Contact)qr.records[i]).OtherPostalCode;
                                cs.LumaryId = ((SfServiceRef.Contact)qr.records[i]).Enrite_Care_Auto_Number__c;
                                if (((SfServiceRef.Contact)qr.records[i]).enrtcr__Status__c != null)
                                {
                                    cs.Active = false;
                                    if (((SfServiceRef.Contact)qr.records[i]).enrtcr__Status__c == "Current")
                                    {
                                        cs.Status = 1;
                                    }
                                    if (((SfServiceRef.Contact)qr.records[i]).enrtcr__Status__c == "Deceased")
                                    {
                                        cs.Status = 2;
                                    }
                                    if (((SfServiceRef.Contact)qr.records[i]).enrtcr__Status__c == "Inactive")
                                    {
                                        cs.Status = 3;
                                    }

                                }
                                cs.Active = true;
                                cs.OnHold = false;
                                lstCus.Add(cs);
                            }
                        }

                        dba.IntegrateCustomerInfotoDB(lstCus);
                    }
                }
                result = true;
            }
            return result;
        }

        public bool IntegrateSFDCId_OperatortoDB(string Usernames, string UserName, string Password)
        {
            logger.Info("Scheduled Driver job triggered");
            bool result = false;
            SoapClient ss = new SoapClient();
            LoginResult lr = new LoginResult();
            DBAction dba = new DBAction(_integrationAppSettings);
            List<ClsUsersList> ObjList = new List<ClsUsersList>();
            if (sessionId == null | sessionId == "")
            {
                // Login Call
                var lastintegratedDate = dba.GetLastintegratedDateandTime("Integrate_CustomerInfo");
                lr = ss.login(null, UserName, Password);
                if (!lr.passwordExpired)
                {
                    sessionId = lr.sessionId.ToString().Trim();
                    serverUrl = lr.serverUrl.ToString().Trim();
                    // Store SessionId in SessionHeader; We will need while making query() call
                    SessionHeader sHeader = new SessionHeader();
                    sHeader.sessionId = sessionId;

                    // Variable to store query results
                    QueryResult qr = new QueryResult();
                    SoapClient ss1 = new SoapClient();
                    ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);
                    ss1.query(sHeader, null, null, null, @"Select Id,EmployeeNumber,UserRoleID,IsActive,Username,CompanyName From User WHERE Username IN (" + Usernames + ")", out qr);

                    sObject[] records = qr.records;
                    if (records != null)
                    {
                        if (records.Length > 0)
                        {
                            for (int i = 0; i <= records.Length - 1; i++)
                            {
                                ClsUsersList Obj = new ClsUsersList();
                                Obj.Id = ((SfServiceRef.User)qr.records[i]).Id;
                                Obj.IsActive = (bool)((SfServiceRef.User)qr.records[i]).IsActive;
                                Obj.UserName = ((SfServiceRef.User)qr.records[i]).Username;
                                Obj.UserRoleID = ((SfServiceRef.User)qr.records[i]).UserRoleId;
                                ObjList.Add(Obj);
                            }
                            dba.IntegrateAllDriver(ObjList);
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        public bool InsertDataintoSFDC(string userName, string password)
        {
            bool Result = false;
            var bllist = GetBillingInformation();

            List<Customer> lstCus = new List<Customer>();
            bool result = false;


            SoapClient ss = new SoapClient();
            LoginResult lr = new LoginResult();
            LoginScopeHeader LH = new LoginScopeHeader();

            if (sessionId == null | sessionId == "")
            {
                lr = ss.login(null, userName, password);
                if (!lr.passwordExpired)
                {
                    sessionId = lr.sessionId.ToString().Trim();
                    serverUrl = lr.serverUrl.ToString().Trim();
                    // Store SessionId in SessionHeader; We will need while making query() call
                    SessionHeader sHeader = new SessionHeader();
                    sHeader.sessionId = sessionId;
                    serverUrl = "https://abilitycentre--NEWACUAT.my.salesforce.com/services/Soap/c/51.0/00D5P0000008nfN";
                    // Variable to store query results
                    QueryResult qr = new QueryResult();
                    SoapClient ss1 = new SoapClient();
                    //ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress("https://abilitycentre--NEWACUAT.my.salesforce.com/services/data/v51.0/composite/tree/enrtcr__Support_Delivered__c/");
                    ss1.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverUrl);
                    //foreach (var res in bllist)
                    //{
                    enrtcr__Support_Delivered__c esdc = new enrtcr__Support_Delivered__c();
                    esdc.Batch_Created__c = true;
                    esdc.enrtcr__Client__c = "0035P000003ws2OQAQ";
                    esdc.enrtcr__Date__c = Convert.ToDateTime("2021-03-30");
                    esdc.enrtcr__Quantity__c = 10;
                    esdc.enrtcr__Support_Contract_Item__c = "a0n5P000000kHNgQAM";
                    esdc.enrtcr__Support_Contract__c = "a0o5P000000Bc9vQAC";
                    esdc.enrtcr__Site__c = "a0l5P000000046nQAA";
                    esdc.enrtcr__Support_CategoryId__c = "a0c5P000000Co8EQAS";
                    esdc.enrtcr__Site_Service_Program__c = "a0j5P000000dLBpQAM";
                    esdc.enrtcr__Rate__c = "a0b5P0000014SHkQAM";
                    esdc.enrtcr__Worker__c = "0057F000005AtbWQAS";
                    esdc.enrtcr__Client_Rep_Accepted__c = true;
                    esdc.enrtcr__Use_Negotiated_Rate__c = true;
                    esdc.enrtcr__Negotiated_Rate_Ex_GST__c = 0.85;
                    esdc.enrtcr__Negotiated_Rate_GST__c = 0.00;
                    LimitInfo[] limt;
                    SaveResult[] createResults;
                    try
                    {

                        ss.create(sHeader, null, null, null, null, null, null, null, null, null, null, null, new sObject[] { esdc }, out limt, out createResults);
                        // CreateRecord(new HttpClient(), "New Record", "enrtcr__Support_Delivered__c", esdc);
                        //  Result = true;

                        var results = createResults[0].success;
                    }
                    catch (Exception ex)
                    {
                        var exp = ex.ToString();
                        Result = false;
                    }
                    // }
                    //

                }
            }
            return Result;
        }

        private string CreateRecord(HttpClient client, string createMessage, string recordType, object result)
        {
            HttpContent contentCreate = new StringContent(createMessage, Encoding.UTF8, "application/xml");
            string uri = $"--/sobjects/{recordType}";

            HttpRequestMessage requestCreate = new HttpRequestMessage(HttpMethod.Post, uri);
            requestCreate.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            requestCreate.Content = contentCreate;

            HttpResponseMessage response = client.SendAsync(requestCreate).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public List<string> GetAllDriverInfo_NotMappedSFDC()
        {
            DBAction dba = new DBAction(_integrationAppSettings);
            return dba.GetDriverInformationIsnotMappedSFDC();
        }

        public List<FC_NDIS.JsonModels.SFDCBillingLines> GetBillingInformation()
        {
            DBAction dba = new DBAction(_integrationAppSettings);
            var res = dba.GetBillingInformation();
            return res;
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
