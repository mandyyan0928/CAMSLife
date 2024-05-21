using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Announcement.Response;
using CaliphWeb.ViewModel;
using System.Collections.Generic;

namespace CaliphWeb.Models.ViewModel
{
    public class EditAnnouncementViewModel
    {
        public EditAnnouncementViewModel()
        {
            Announcement = new Announcement();
            AnnouncementTypeList = new List<MasterData>();
            AgentUsers = new List<AgentUser>();
        }
        public Announcement Announcement { get; set; }
        public List<MasterData> AnnouncementTypeList { get; set; }
        public List<AgentUser> AgentUsers { get; set; }
    }
}