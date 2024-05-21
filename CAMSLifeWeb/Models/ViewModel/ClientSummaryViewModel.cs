using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Report;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class ClientSummaryViewModel
    {
        public List<AgentUser> Users { get; set; }

        public List<ClientSummary>  ClientSummaries { get; set; }
    }
}