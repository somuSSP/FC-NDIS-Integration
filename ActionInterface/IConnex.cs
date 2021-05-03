using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.ActionInterface
{
    interface IConnex
    {
        public bool validator(string EnvName, string EnvAPI, int stagingSourceID);
        public bool IntegrateDriverDetails(string userName, string Password);
    }
}
