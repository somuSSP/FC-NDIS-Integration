using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FC_NDIS.JsonModels;
namespace FC_NDIS.APIModels.FCModel
{
   
    public class RecordStatus
    {
        public int DriverId { get; set; }
        public int ActionStatus { get; set; }
        public object Error { get; set; }
        public root root { get; set; }
    }
}
