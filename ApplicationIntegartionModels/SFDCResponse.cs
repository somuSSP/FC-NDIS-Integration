using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.ApplicationIntegartionModels
{
    public class FCResponseTokenandUserId
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }

    class FCResponse
    {
        List<FCResponseInfo> data { get; set; }
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }
    class FCResponseInfo
    {
        public string Domain { get; set; }
        public bool IsMustChangePassword { get; set; }
        public int LicenseExpiryDays { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string UserID { get; set; }
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }
}
