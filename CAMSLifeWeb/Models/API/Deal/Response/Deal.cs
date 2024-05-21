using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaliphWeb.Models.API;

namespace CaliphWeb.Models.API.Deal
{
    public class Deal
    {
        public int ClientDealId { get; set; }
        public int ClientId { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string DealTitleDesc { get; set; }
        public int DealTitleId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string ClientName { get; set; }
        public string ContactNo { get; set; }
        public string NextActivityDesc { get; set; }
        public DateTime? NextActivityDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<DealActivity> Activities { get; set; }
    }
}