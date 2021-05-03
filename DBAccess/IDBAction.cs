using FC_NDIS.ApplicationIntegartionModels;
using FC_NDIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.DBAccess
{
    interface IDBAction
    {
        public bool IntegrateAssetsintoDB(List<Vehicle> vehicles);
        public bool IntegrateCustomerLineinfointoDB(List<CustomerServiceLine> cslines);
        public bool IntegrateAllDriver(List<ClsUsersList> SFDCUsers);
        public List<string> GetDriverInformationIsnotMappedSFDC();
        public bool IntegrateOpertorintoDB(List<Driver> drivers);
        public bool IntegrateCustomerInfotoDB(List<Customer> customer);
    }
}
