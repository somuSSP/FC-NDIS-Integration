using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.ApplicationIntegartionModels
{
    public class ClsCustomerServiceLineResponse
    {
        public int StatusID { get; set; }
        public string StatusDescription { get; set; }

        public List<ClsListofCustServiceLine> ObjLstCutomers = new List<ClsListofCustServiceLine>();
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

    public class ClsCusServiceLineIntegRequest
    {
        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }

    }

    public class ClsCusIntegRequest
    {
        public string LoginUserName { get; set; }

        public string LoginPassword { get; set; }

    }

    public class ClsListofCustomers
    {
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
}
