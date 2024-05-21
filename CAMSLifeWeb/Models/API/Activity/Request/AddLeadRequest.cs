using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class AddLeadRequest
    {
        public int ClientDealActivityId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }
}