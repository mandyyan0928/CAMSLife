using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Report;
using CaliphWeb.Models.Data;
using CaliphWeb.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class ReferralListViewModel
    {
        public ReferralListViewModel()
        {
            Status = new List<MasterData>();
            ReferralData = new ReferralData();
            Users = new List<AgentUser>();

        }
        public List<MasterData> Status { get; set; }
        public ReferralData ReferralData { get; set; }
        public List<AgentUser> Users { get; set; }

    }
    public class ReferralData
    {
        public ReferralData()
        {
            Referral = new List<Referral>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<Referral> Referral { get; set; }
        public Paging Paging { get; set; }
    }
}