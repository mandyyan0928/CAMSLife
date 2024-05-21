using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Deal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EditActivityViewModel
    {

        public EditActivityViewModel()
        {
            Deals = new List<Deal>();
            Activities = new List<ActivityPoint>();
            AgentUsers = new List<AgentUser>();
        }
        public List<Deal> Deals { get; set; }
        public List<ActivityPoint> Activities { get; set; }

        public List<AgentUser> AgentUsers { get; set; }
        public int ClientDealActivityId { get; set; }
        public int UserActivityId { get; set; }
        public int UserId { get; set; }
        public int ClientDealId { get; set; }
        public int ActivityPointId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string EventId { get; set; }
        public string Email { get; set; }
        public bool GoogleLinked { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public int ClientId { get; set; }

    }
}