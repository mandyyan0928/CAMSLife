using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel.Data
{
    public class ReferralLeadFilterRequest
    {
      
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ReferralName { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string CreatedBy { get; set; }
    }
}