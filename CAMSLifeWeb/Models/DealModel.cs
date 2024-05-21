using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models
{
    public class DealModel
    {
        public int ClientDealId { get; set; }
        public int ClientId { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string DealTitleDesc { get; set; }
        public int DealTitleId { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public string Contact { get; set; }
        public string NextActivityDesc { get; set; }
        public DateTime? NextActivityDate { get; set; }
        public string CreatedBy { get; set; }



    }
}