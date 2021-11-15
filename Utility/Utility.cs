using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.Utility
{
    public class Utilitys : IUtility
    {

        private readonly IntegrationAppSettings _integrationAppSettings;
        public Utilitys(IntegrationAppSettings integrationAppSettings)
        {
            this._integrationAppSettings = integrationAppSettings;

        }
        public DateTime GetDateTime()
        {
           return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById(_integrationAppSettings.ApplicationTimeZone));
        }
    }
}
