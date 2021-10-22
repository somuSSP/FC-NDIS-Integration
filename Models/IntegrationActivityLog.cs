using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.Models
{
    public partial class IntegrationActivityLog
    {
        public int IntegrationActivityLogId { get; set; }
        public int IntegrationActivityId { get; set; }
        public int TotalPushedRecordCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public int CreatedRecordCount { get; set; }
        public int ModifiedRecordCount { get; set; }
        public string ExceptionDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
