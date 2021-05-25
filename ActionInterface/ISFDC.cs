using FC_NDIS.ApplicationIntegartionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.ActionInterface
{
    public interface ISFDC
    {    
        public bool IntegerateSfCustServiceLine();
        public bool IntegerateSfTransportRate();
        public bool IntegerateSfTravelRate();
        public bool IntegerateSfCustomeList();
        public bool IntegrateSFDCId_OperatortoDB(string Usernames);
        public List<string> GetAllDriverInfo_NotMappedSFDC();
    }
}
