using CaliphWeb.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel
{
    public class Referral
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ReferralName { get; set; }
        public string HP { get; set; }
        public string Remark { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string CreatedBy { get; set; }     
        public DateTime CreatedDate { get; set; }
    }
}