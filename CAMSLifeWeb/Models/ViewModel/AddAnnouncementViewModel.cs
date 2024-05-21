using CaliphWeb.Models.API.Agent;
using CaliphWeb.ViewModel;
using System.Collections.Generic;

namespace CaliphWeb.Models.ViewModel
{
    public class AddAnnouncementViewModel
    {
        public List<MasterData> AnnouncementTypeList { get; set; }
        public List<AgentUser> AgentUsers { get; set; }
    }
}