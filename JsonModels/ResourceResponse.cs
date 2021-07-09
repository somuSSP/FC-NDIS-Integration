using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.JsonModels
{
    public class root
    {
        public string Data { get; set; }
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }
}
