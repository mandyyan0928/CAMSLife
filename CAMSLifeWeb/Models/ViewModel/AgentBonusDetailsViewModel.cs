using CaliphWeb.Models;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace CaliphWeb.ViewModel
{
    public class AgentBonusDetailsViewModel {
        public AgentBonusDetailsViewModel()
        {
            AgentPolicies = new List<AgentPolicyByProdyctResponse>();
        }
        public List<AgentPolicyByProdyctResponse>  AgentPolicies     { get; set; }

        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public int Year { get; set; }
        public DateTime PersistencyDate { get; set; }
        public BonusTrackerDashboard BonusTrackerDashboard { get; set; } = new BonusTrackerDashboard();
    }

    public class LeaderBonusSummaryViewModel
    {
        public string LeaderId { get; set; }
        public string LeaderName { get; set; }
        public int Year { get; set; }
        public List<LeaderSummaryData> LeaderSummaryDatas { get; set; } = new List<LeaderSummaryData>();
    }

    public class LeaderSummaryData {
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime InForceDate { get; set; }
        public DateTime PersistencyDate { get; set; }
        public double ACE { get; set; }
        public double NetACE { get; set; }
        public double PersistencyPremium { get; set; }
        public double PersistencyRatio { get; set; }
        public int Cases { get; set; }
        public BonusContest PotentialBonus { get; set; } = new BonusContest();
        public List<BonusContest> QualifiedBonus { get; set; } = new List<BonusContest>();
        public double TotalPremiumCollected_FYC { get; set; }
    }
}