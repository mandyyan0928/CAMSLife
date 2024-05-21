using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Deal;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EditDealViewModel
    {
        public EditDealViewModel()
        {
            Clients = new List<Client>();
            Titles = new List<MasterData>();
            Deal = new Deal();
            AgentUsers = new List<AgentUser>();
        }
        public List<Client> Clients { get; set; }
        public List<MasterData> Titles { get; set; }
        public List<AgentUser>  AgentUsers { get; set; }
        public Deal Deal { get; set; }

    }
}