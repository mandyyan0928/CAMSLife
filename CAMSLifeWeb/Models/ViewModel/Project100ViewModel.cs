using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.AgentRecruit;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class Project100ViewModel
    {

        public Project100ViewModel()
        {
            Agents = new List<AgentUser>();
            AnnualIncomes = new List<MasterData>();
            Ages = new List<MasterData>();
            Occupations = new List<MasterData>();
            MaritalStatuses = new List<MasterData>();
            LengthOfTimeKnowns = new List<MasterData>();
            HowWellKnowns = new List<MasterData>();
            HowOftenSeenInAYears = new List<MasterData>();
            CouldApproachBusinesses = new List<MasterData>();
            AbilityToProvideReferrals = new List<MasterData>();
            Clients = new List<Client>();
                 
        }
        public List<AgentUser>   Agents { get; set; }

        public List<MasterData> AnnualIncomes { get; set; }
        public List<MasterData> Ages { get; set; }
        public List<MasterData> Occupations { get; set; }
        public List<MasterData> MaritalStatuses { get; set; }
        public List<MasterData> LengthOfTimeKnowns { get; set; }
        public List<MasterData> HowWellKnowns { get; set; }
        public List<MasterData> HowOftenSeenInAYears { get; set; }
        public List<MasterData> CouldApproachBusinesses { get; set; }
        public List<MasterData> AbilityToProvideReferrals { get; set; }

        public List<Client> Clients { get; set; }
    }


    public class Project100RecruitmentViewModel
    {

        public Project100RecruitmentViewModel()
        {
            AnnualIncomes = new List<MasterData>();
            Ages = new List<MasterData>();
            Occupations = new List<MasterData>();
            MaritalStatuses = new List<MasterData>();
            AgentRecruiments = new List<AgentRecruiment>();

        }
        public List<AgentUser> Agents { get; set; }

        public List<MasterData> AnnualIncomes { get; set; }
        public List<MasterData> Ages { get; set; }
        public List<MasterData> Occupations { get; set; }
        public List<MasterData> MaritalStatuses { get; set; }

        public List<AgentRecruiment> AgentRecruiments { get; set; }
    }
}