using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.APIModels.AccessResult
{  
    public class Result
    {
        public string referenceId { get; set; }
        public string id { get; set; }
    }

    public class Root
    {
        public bool hasErrors { get; set; }
        public List<Result> results { get; set; }
    }
}
