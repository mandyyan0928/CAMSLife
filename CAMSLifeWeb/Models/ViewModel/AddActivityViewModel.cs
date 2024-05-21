using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Deal;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class AddActivityViewModel
    {


        public AddActivityViewModel()
        {
            Deals = new List<Deal>();
            Clients = new List<Client>();
            Activities = new List<ActivityPoint>();
            AgentUsers = new List<AgentUser>();
        }
        public List<Client> Clients { get; set; }
        public List<Deal> Deals { get; set; }
        public List<ActivityPoint> Activities { get; set; }

        public List<AgentUser>  AgentUsers { get; set; }
        public List<ActivityReview> ActivityReviews { get; set; }

    }
}


