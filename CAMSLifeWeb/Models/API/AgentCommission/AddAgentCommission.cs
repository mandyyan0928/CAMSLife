using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class AddAgentCommission
    {

        public string Username { get; set; }
        public string AgentName { get; set; }
        public DateTime PayoutDate { get; set; }
        public DateTime CycleStartDate { get; set; }
        public DateTime CycleEndDate { get; set; }
        public string CreatedBy { get; set; }
        public decimal CommAmt { get; set; }
    }

}