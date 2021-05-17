using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.APIModels.AccessResult
{
      public class Root
    {
        public string id { get; set; }
        public bool success { get; set; }
        public List<object> errors { get; set; }
    }
}
