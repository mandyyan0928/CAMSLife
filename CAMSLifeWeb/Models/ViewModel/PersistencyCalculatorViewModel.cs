using CaliphWeb.Models.API.one2one;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CaliphWeb.ViewModel
{
    public class PersistencyCalculatorViewModel
    {


        public PersistencyCalculatorViewModel()
        {
            AgentPolicies = new List<AgentPolicyResponse>();
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TargetRatio { get; set; }
        public DateTime PersistencyDate { get; set; }
        public string AgentId { get; set; }
        public int Generation { get; set; }
        public string Type { get; set; }
        public string AgentName { get; set; }
        public List<AgentPolicyResponse> AgentPolicies { get; set; } = new List<AgentPolicyResponse>();
        public List<AgentHierarchyResponse> DirectGroups { get; set; }
        public List<GenerationGroupPolicy> HierarchyPolicies { get; set; } = new List<GenerationGroupPolicy>();
        public int TotalMonthly => AgentPolicies.Where(x => x.Factor == 12).Count();
        public int TotalQuarterly => AgentPolicies.Where(x => x.Factor == 4).Count();
        public int TotalAnnual => AgentPolicies.Where(x => x.Factor == 1).Count();
        public int TotalHalfAnnual => AgentPolicies.Where(x => x.Factor == 2).Count();
        public int TotalInForce => AgentPolicies.Where(x => x.certificate_status.ToLower() == PolicyCertificateStatus.InForce).Count();
        public int TotalLapsed => AgentPolicies.Where(x => x.certificate_status.ToLower() == PolicyCertificateStatus.Lapsed).Count(); 
      //  public int TotalFreelookCancellation => AgentPolicies.Where(x => x.certificate_status.ToLower() == PolicyCertificateStatus.FreelookCancellation).Count();
        public int TotalContractSurrendereds => AgentPolicies.Where(x => x.certificate_status.ToLower() == PolicyCertificateStatus.ContractSurrended).Count();

        public int RegisteredDeathClaim => AgentPolicies.Where(x => x.certificate_status.ToLower() == PolicyCertificateStatus.RegisteredDeathClaim).Count();
        public int Postponed => AgentPolicies.Where(x => x.certificate_status.ToLower() == PolicyCertificateStatus.Postponed).Count();
        public int ApprovedDeathClaim => AgentPolicies.Where(x => x.certificate_status.ToLower() == PolicyCertificateStatus.ApprovedDeathClaim).Count();
        public double ACE => AgentPolicies.Sum(x => x.AnnualisedPremium);
        public double PersistencyPremium => AgentPolicies.Where(x => x.due_date >= PersistencyDate).Sum(x => x.AnnualisedPremium);
        public double Ratio => ACE > 0 ? (PersistencyPremium / ACE) * 100 : 0;
    }

    public class    AgentHierarchyPolicy {
        public AgentHierarchyPolicy()
        {
            EndDate = DateTime.Now;
            Agent = new AgentHierarchyResponse();
            Policies = new List<AgentPolicyResponse>();
        }
        public DateTime EndDate { get; set; }
        public AgentHierarchyResponse Agent { get; set; } = new AgentHierarchyResponse();
        public List<AgentPolicyResponse> Policies { get; set; } = new List<AgentPolicyResponse>();
        public double ACE => Policies.Sum(x => x.AnnualisedPremium);
        public double PersistencyPremium => Policies.Where(x=>x.due_date>=EndDate).Sum(x => x.AnnualisedPremium);
        public double Ratio=> ACE>0?(PersistencyPremium / ACE) * 100:0;
    }

    public class GenerationGroupPolicy
    {

        public int Generation { get; set; }
        public string GenerationName { get {
                var name = "";
                if (Generation == 0)
                    name = "Direct Group";
                else
                    name = "Group " + Generation.ToString();

                return name;
            } }


        public List<AgentHierarchyPolicy> AgentHierarchyPolicies { get; set; }
        public string LeaderId { get; set; }


    }
        public class PersistencyProductSummary
    {

        public string ProductName { get; set; }
        public int Cases { get; set; }
        public decimal ACE { get; set; }
        public decimal Ratio { get; set; }
        public decimal Average { get; set; }
    }


    public class PersistencyOverallSummary
    {

        public string PaymentMode { get; set; }
        public int Cases { get; set; }
        public int Percentage { get; set; }
    }

    public class PolicyPaymentMethodCode {
        public const string CreditCard = "credit card";
        public const string DirectBilling = "direct billing (cash)";
        public const string BiroAngkasa = "biro angkasa";
        public const string DirectDebit = "direct debit";

    }
    public class PolicyPaymentMode
    {
        public const string Monthly = "monthly";
        public const string Quarterly = "quarterly";
        public const string HalfAnnual = "halfAnnual";
        public const string Annual = "annual";

    }


    public class PolicyCertificateStatus
    {
        public const string InForce = "in force";
        public const string Lapsed = "lapsed";
       public const string FreelookCancellation = "freelook cancellation";
        public const string ContractSurrended = "contract surrendered";
        public const string RegisteredDeathClaim = "registered death claim";
        public const string Postponed = "postponed";
        public const string ApprovedDeathClaim = "approved death claim";
    }


    public class FWDProduct
    {
        public const string FutureFirst = "FWD Future First";
        public const string CIFirst = "FWD CI First";
        public const string FamilyFirst = "FWD Family First";
        public const string IncomeFirst = "FWD Income First";
        public const string LifeFirst = "FWD Life First";
        public const string InvestFirst_Legacy = "FWD Invest First (Legacy)";
        public const string InvestFirst_Wealth = "FWD Invest First (Wealth)";
        public const string InvestFirst_Education = "FWD Invest First (Education)";
        public const string InvestFirstPlus_Legacy = "FWD Invest First Plus (Legacy)";
        public const string InvestFirstPlus_Wealth = "FWD Invest First Plus (Wealth)";
        public const string InvestFirstPlus_Education = "FWD Invest First Plus (Education)";

    }


    public class PersistencySummaryData
    {


        public PersistencySummaryData()
        {
            GroupPolicies = new List<AgentPolicyResponse>();
            PersonalPolicies = new List<AgentPolicyResponse>();
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PersistencyDate { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public List<AgentPolicyResponse> GroupPolicies { get; set; } = new List<AgentPolicyResponse>();
        public double GroupACE => GroupPolicies.Sum(x => x.AnnualisedPremium);
        public double GroupPersistencyPremium => GroupPolicies.Where(x => x.due_date >= PersistencyDate).Sum(x => x.AnnualisedPremium);
        public double GroupRatio => GroupACE > 0 ? (GroupPersistencyPremium / GroupACE) * 100 : 0;


        public List<AgentPolicyResponse> PersonalPolicies { get; set; } = new List<AgentPolicyResponse>();
        public double PersonalACE => PersonalPolicies.Sum(x => x.AnnualisedPremium);
        public double PersonalPersistencyPremium => PersonalPolicies.Where(x => x.due_date >= PersistencyDate).Sum(x => x.AnnualisedPremium);
        public double PersonalRatio => PersonalACE > 0 ? (PersonalPersistencyPremium / PersonalACE) * 100 : 0;
    }


    public class PersistencySummary
    {
         
        public PersistencySummary()
        { 
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PersistencyDate { get; set; }
        public string PersistencyDateDisplay => PersistencyDate.ToString("dd MMM yyyy");
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public double GroupRatio { get; set; }
        public double PersonalRatio { get; set; }
    }
}