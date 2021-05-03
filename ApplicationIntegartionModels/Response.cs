using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.ApplicationIntegartionModels
{
    public class Response
    {
        public object Results { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
