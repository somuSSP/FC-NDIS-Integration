using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS.APIModels.AccessResult
{
    //public class Error
    //{
    //    public string statusCode { get; set; }
    //    public string message { get; set; }
    //    public List<string> fields { get; set; }
    //}

    //public class Result
    //{
    //    public string referenceId { get; set; }
    //    public string id { get; set; }
    //    public List<Error> errors { get; set; }
    //}

    //public class Root
    //{
    //    public bool hasErrors { get; set; }
    //    public List<Result> results { get; set; }
    //}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    //public class Result
    //{
    //    public result result { get; set; }
    //    public int statusCode { get; set; }
    //}

    //public class Root
    //{
    //    public bool hasErrors { get; set; }
    //    public List<Result> results { get; set; }
    //}
    //public class result
    //{
    //    public string id { get; set; }
    //    public bool success { get; set; }
    //    public string errorCode { get; set; }
    //    public string message { get; set; }
    //    public object errors { get; set; }
    //}

    public class Result
    {
        public int statusCode { get; set; }
        public object result { get; set; }
    }

    public class Root
    {
        public bool hasErrors { get; set; }
        public List<Result> results { get; set; }
    }

}
