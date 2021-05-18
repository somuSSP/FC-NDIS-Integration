using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.APIModels.SFDCtoDriver
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Attributes
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Record
    {
        public Attributes attributes { get; set; }
        public string Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string UserRoleId { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public string CompanyName { get; set; }
    }

    public class Root
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public List<Record> records { get; set; }
    }


}
