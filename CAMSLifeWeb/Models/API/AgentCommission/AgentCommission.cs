using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class AgentCommission
    {
        public int AgentCommissionId { get; set; }
        public string Username { get; set; }
        public string AgentName { get; set; }
        public DateTime PayoutDate { get; set; }
        public DateTime CycleStartDate { get; set; }
        public DateTime CycleEndDate { get; set; }
        public int StatusId { get; set; }
        public decimal CommAmt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
         
    }
}