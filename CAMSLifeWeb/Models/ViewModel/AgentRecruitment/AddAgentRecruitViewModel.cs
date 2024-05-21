using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.AgentRecruit;
using CaliphWeb.Models.Data;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class AddAgentRecruitViewModel
    {
        public List<MasterData> EducationalBackgrounds { get; set; }
        public List<MasterData> Age { get; set; }
        public List<MasterData> AnnualIncome { get; set; }
        public List<MasterData> Occupation { get; set; }
        public List<MasterData> MaritalStatus { get; set; }
        public List<MasterData> Type { get; set; }
    }


    public class UpdateAgentRecruitViewModel
    {
        public List<MasterData> EducationalBackgrounds { get; set; }
        public List<MasterData> Age { get; set; }
        public List<AgentRecruitmentTrack>  AgentRecruitmentTracks { get; set; }
        public List<MasterData> AnnualIncome { get; set; }
        public List<MasterData> Occupation { get; set; }
        public List<MasterData> MaritalStatus { get; set; }
        public List<MasterData> Type { get; set; }
        public AgentRecruiment AgentRecruiment { get; set; } = new AgentRecruiment();
       
    }

    public class AgentRecruitListViewModel
    {
        public AgentRecruitListViewModel()
        {
            Status = new List<MasterData>();
            AgentRecruimentData = new AgentRecruimentData();
            AddAgentRecruitmentActivityViewModel = new AddAgentRecruitmentActivityViewModel();
        }
        public List<MasterData> Status { get; set; }
        public AgentRecruimentData AgentRecruimentData { get; set; }
        public AddAgentRecruitmentActivityViewModel  AddAgentRecruitmentActivityViewModel { get; set; }
    }

    public class AgentRecruimentData
    {
        public AgentRecruimentData()
        {
            AgentRecruiments = new List<AgentRecruiment>();
            Paging = new Paging();
        }
        public List<AgentRecruiment> AgentRecruiments { get; set; }
        public Paging Paging { get; set; }
    }


    public class AddAgentRecruitmentActivityViewModel
    {


        public AddAgentRecruitmentActivityViewModel()
        {
            AgentRecruiments = new List<AgentRecruiment>();
            Activities = new List<ActivityPoint>();
          
        }
        public List<AgentRecruiment>  AgentRecruiments { get; set; }
        public List<ActivityPoint> Activities { get; set; }


    }


    public class AgentRecruitmentLeadListViewModel
    {
        public AgentRecruitmentLeadListViewModel()
        {
            Status = new List<MasterData>();
            ReferralData = new AgentRecruitmentLeadData();
            Users = new List<AgentUser>();

        }
        public List<MasterData> Status { get; set; }
        public AgentRecruitmentLeadData ReferralData { get; set; }
        public List<AgentUser> Users { get; set; }

    }
    public class AgentRecruitmentLeadData
    {
        public AgentRecruitmentLeadData()
        {
            Referral = new List<AgentRecruitmentLead>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<AgentRecruitmentLead> Referral { get; set; }
        public Paging Paging { get; set; }
    }


    
}