using System;
using System.Collections.Generic;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class UserGridSetting
    {
        public int Ugsid { get; set; }
        public int UserId { get; set; }
        public string FormName { get; set; }
        public string GridName { get; set; }
        public string ColumnPairVal { get; set; }
    }
}
