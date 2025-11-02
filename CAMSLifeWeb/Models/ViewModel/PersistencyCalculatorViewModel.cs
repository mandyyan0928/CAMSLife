using CaliphWeb.Models.API.one2one;
using DocumentFormat.OpenXml.Bibliography;
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
        public const string InForce = "In Force";
        public const string Lapsed = "Lapsed";
       public const string FreelookCancellation = "Free Look Cancellation";
        public const string ContractSurrended = "Surrendered";
        public const string RegisteredDeathClaim = "Death Registered";
        public const string Postponed = "Postponed";
        public const string ApprovedDeathClaim = "Death Claim";
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


    public class SunlifeProduct
    {
        public static readonly List<string> SunHeritage = new List<string> { "ULFH", "ULFI", "ULFJ", "ULFK", "ULFL", "ULFM" };
        public static readonly List<string> SunPrimeLink= new List<string> { "ULFA", "ULFB", "ULFC", "ULFD"};
        public static readonly List<string> SunSecureSaver = new List<string> { "TTRG", "TTRH", "TTRJ"};
        public static readonly List<string> SunHajj = new List<string> { "TTFL", "TTFM", "TTFU", "TTFV", "TTFW", "TTFX", "TTFY" };

    }
    // home
    public class PersistencySummaryData
    {


        public PersistencySummaryData()
        {
            GroupPolicies = new List<AgentPolicyResponse>();
            Month = DateTime.Now.Month;
            
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Month { get; set; }
        public DateTime PersistencyDate { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public List<AgentPolicyResponse> GroupPolicies { get; set; } = new List<AgentPolicyResponse>();
        public double GroupACE => GroupPolicies.Sum(x => x.AnnualisedPremium);
        public double GroupPersistencyPremium => GroupPolicies.Where(x => x.due_date >= PersistencyDate).Sum(x => x.AnnualisedPremium);
        public double GroupRatio => GroupACE > 0 ? (GroupPersistencyPremium / GroupACE) * 100 : 0;
        public double GroupAFYCMTD => GroupPolicies
             .Where(x =>  x.due_date >= DateTime.Now)
.Sum(x => x.AnnualisedPremium);

        public List<AgentPolicyResponse> PersonalPolicies => (GroupPolicies == null) ? new List<AgentPolicyResponse>():GroupPolicies.Where(x => x.selling_agent_code == AgentId).ToList();
        public double PersonalACE => PersonalPolicies.Sum(x => x.AnnualisedPremium);
        public double PersonalPersistencyPremium => PersonalPolicies.Where(x => x.due_date >= PersistencyDate).Sum(x => x.AnnualisedPremium);
        public double PersonalRatio => PersonalACE > 0 ? (PersonalPersistencyPremium / PersonalACE) * 100 : 0;
        public double PersonalAFYCMTD => PersonalPolicies
     .Where(x => x.selling_agent_code == AgentId && x.due_date>= DateTime.Now )
     .Sum(x => x.AnnualisedPremium);

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
        public double PersonalAFYCYTD { get; set; }
        public double PersonalAFYCMTD { get; set; }
        public double GroupAFYCYTD { get; set; }
        public double GroupAFYCMTD { get; set; }
    }
}