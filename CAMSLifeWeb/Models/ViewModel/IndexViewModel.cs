using CaliphWeb.Models.API.Announcement.Response;
using CaliphWeb.Models.API.one2one;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            WeeklyActiveDeal = new List<int>();
            WeeklyClosedDeal = new List<int>();
            WeeklyLostDeal = new List<int>();
            WeeklyDays = new List<string>();
            Announcements = new List<Announcement>();
        }
        public int ActiveDeal { get; set; }
        public int ClosedDeal { get; set; }
        public int TotalDeal { get; set; }
        public decimal DealClosingRate =>TotalDeal>0? ((decimal)ClosedDeal / (decimal)TotalDeal)*100:0;

        public int PotentialClient { get; set; }
        public int ConfirmedClient { get; set; }
        public int TotalClient { get; set; }
        public decimal ClientConfirmRate => TotalClient > 0 ? ((decimal)ConfirmedClient / (decimal)TotalClient )*100: 0;

        public int UpcomingActivity { get; set; }
        public decimal TotalCommissionAmt { get; set; }
        public List<string> WeeklyDays { get; set; }

        public List<int> WeeklyActiveDeal { get; set; }

        public List<int> WeeklyClosedDeal { get; set; }


        public List<int> WeeklyLostDeal { get; set; }

        public List<Announcement> Announcements { get; set; }

        public BonusTrackerViewModel  BonusTrackerViewModel { get; set; }
    }
    public class BonusTrackerDashboard {
        public DateTime PersistencyDate { get; set; }
        public double ACE { get; set; }
        public double NetACE { get; set; }
        public double PersistencyPremium { get; set; }
        public double PersistencyRatio{ get; set; }
        public int Cases { get; set; }

        public BonusContest PotentialBonus { get; set; } = new BonusContest();
        public List<BonusContest> QualifiedBonus { get; set; } = new List<BonusContest>();
        public double TotalPremiumCollected_FYC { get; set; }

        public double ACE_CompleteProgress => PotentialBonus.FromNetACE == 0 ? 0 :( (NetACE / (double)PotentialBonus.FromNetACE) * 100)>100?100: (NetACE / (double)PotentialBonus.FromNetACE) * 100;
        public double Cases_CompleteProgress => PotentialBonus.Cases == 0 ? 0 : ((Cases / (double)PotentialBonus.Cases) * 100)>100?100: (Cases / (double)PotentialBonus.Cases) * 100;
        public double PersistencyRatio_CompleteProgress => PotentialBonus.PR_D0 == 0 ? 0 : ((PersistencyRatio / (double)PotentialBonus.PR_D0) * 100)>100?100: (PersistencyRatio / (double)PotentialBonus.PR_D0) * 100;
        public double Overall_CompleteProgress => ((ACE_CompleteProgress + Cases_CompleteProgress + PersistencyRatio_CompleteProgress) / 300) * 100;
    }
    public class BonusTrackerViewModel {
        public DateTime PersistencyDate { get; set; }
        public List<AgentPolicyByProdyctResponse> AgentProductPolicies { get; set; } = new List<AgentPolicyByProdyctResponse>();

        public double ACE => AgentProductPolicies.Sum(x => x.AnnualisedPremium);
        public double NetACE => AgentProductPolicies.Sum(x => x.NetAnnualisedPremium);
        public double TotalPremiumCollected_FYC => AgentProductPolicies.Sum(x => x.TotalPremiumCollected);

        public double PersistencyPremium => AgentProductPolicies.Where(x => x.due_date >= PersistencyDate).Sum(x => x.AnnualisedPremium);
        public double PersistencyRatio => ACE == 0 ? 0 :( PersistencyPremium / ACE)*100;

        public int Cases => AgentProductPolicies.GroupBy(x => x.certificate_no).Count();


        public BonusContest PotentialBonus { get; set; } = new BonusContest();

    }
}