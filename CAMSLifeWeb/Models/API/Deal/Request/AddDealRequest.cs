using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Deal
{
    public class AddDealRequest
    {
        public int ClientId { get; set; }
        public int DealTitleId { get; set; }
        public string Remarks { get; set; }

        public string Name { get; set; }
        public string CreatedBy { get; set; }
    }


}