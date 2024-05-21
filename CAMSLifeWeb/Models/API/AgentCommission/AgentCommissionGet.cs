using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class AgentCommissionGet
    {
        public string Username { get; set; }
        public int? AgentCommissionId { get; set; }
        public DateTime? PayoutDateFrom { get; set; }
        public DateTime? PayoutDateTo { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class AgentCommissionFilter
    {
        public string Year1 { get; set; }
        public string Year2 { get; set; }
        public string AgentId { get; set; }
    }
}