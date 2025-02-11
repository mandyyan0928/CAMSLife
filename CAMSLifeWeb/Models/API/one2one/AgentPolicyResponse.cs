using CaliphWeb.Helper;
using Microsoft.Owin.Security.Provider;
using System;

namespace CaliphWeb.Models.API.one2one
{
    public class AgentPolicyResponse
    {
        public string leader_code { get; set; }
        public string leader_name { get; set; }
        public string selling_agent_code { get; set; }
        public string selling_agent_name { get; set; }
        public string service_agent_code { get; set; }
        public string service_agent_name { get; set; }
        public string life_assured { get; set; }
        public string certificate_no { get; set; }
        public string certificate_status { get; set; }
        public string pay_mode { get; set; }
        public string product_name { get; set; }
        public string term { get; set; }
        public DateTime issue_date { get; set; }
        public DateTime summission_date { get; set; }
        public DateTime due_date { get; set; }
        public double contribution_amount { get; set; }
        public string payment_method_code { get; set; }
        public DateTime status_updated_date { get; set; }
        public string spinoff_leader_code { get; set; }
        public string spinoff_leader_name { get; set; }
        public string type { get; set; }

        public int Factor
        {
            get
            {
                if (string.IsNullOrEmpty(pay_mode))
                    return 0;
               else if (pay_mode.ToLower() == "monthly")
                    return 12;
                else if (pay_mode.ToLower() == "half annual")
                    return 2;
                else if (pay_mode.ToLower() == "annual")
                    return 1;
                else if (pay_mode.ToLower() == "quarterly")
                    return 4;
                else
                    return 0;
               
            }
        }
        public int PayUpTo
        {
            get
            {

                var daysDiff = (due_date - issue_date).TotalDays;
                decimal months = ConvertHelper.ConvertDecimal((((daysDiff / (365 / 12)) / 12)  ).ToString() );
                var payup = ConvertHelper.ConvertInt((Math.Ceiling(months * Factor)).ToString());
                return payup;
            }
        }

        public double AnnualisedPremium
        {
            get
            {

                var annualPremium = Factor * contribution_amount;
                return annualPremium;
            }
        }

        public double TotalPremiumCollected
        {
            get
            {

                var annualPremium = PayUpTo * contribution_amount;
                return annualPremium;
            }
        }


     
    }

    public class AgentPolicyByProdyctResponse
    {
        public string leader_code { get; set; }
        public string leader_name { get; set; }
        public string selling_agent_code { get; set; }
        public DateTime selling_agent_coded_date { get; set; }
        public string selling_agent_name { get; set; }
        public string service_agent_code { get; set; }
        public string service_agent_name { get; set; }
        public string life_assured { get; set; }
        public string certificate_no { get; set; }
        public string certificate_status { get; set; }
        public string pay_mode { get; set; }
        public string plan_name { get; set; }
        public string term { get; set; }
        public DateTime issue_date { get; set; }
        public DateTime summission_date { get; set; }
        public DateTime due_date { get; set; }
        public string product_type { get; set; }
        public string product_name { get; set; }
        public string product_term { get; set; }

        public double contribution_amount { get; set; }
        public string payment_method_code { get; set; }
        public DateTime status_updated_date { get; set; }
        public string spinoff_leader_code { get; set; }
        public string spinoff_leader_name { get; set; }
        public decimal fyc { get; set; }
        public decimal  ace { get; set; }

        public int Factor
        {
            get
            {
                if (pay_mode.ToLower() == "monthly")
                    return 12;
                else if (pay_mode.ToLower() == "half annual")
                    return 2;
                else if (pay_mode.ToLower() == "annual")
                    return 1;
                else if (pay_mode.ToLower() == "quarterly")
                    return 4;
                else
                    return 0;

            }
        }
        public int PayUpTo
        {
            get
            {

                var daysDiff = (due_date - issue_date).TotalDays;
                decimal months = ConvertHelper.ConvertDecimal((((daysDiff / (365 / 12)) / 12)).ToString());
                var payup = ConvertHelper.ConvertInt((Math.Ceiling(months * Factor)).ToString());
                return payup;
            }
        }

        public double AnnualisedPremium
        {
            get
            {
                if (product_type.ToLower() != "basic" && product_type.ToLower() != "rider")
                    return 0;

                var annualPremium =  Factor * contribution_amount;
                return annualPremium;
            }
        }


        public double NetAnnualisedPremium
        {
            get
            {

                var annualPremium = ConvertHelper.ConvertInt(term, 0) <= 10 ? AnnualisedPremium / 2 : AnnualisedPremium;
                return annualPremium;
            }
        }

        public double TotalPremiumCollected
        {
            get
            {
                if (product_type.ToLower() != "basic" && product_type.ToLower() != "rider")
                    return 0;

                if (certificate_status.ToLower() != "in force"&& due_date<= new DateTime(2023, 2, 28))
                    return 0;

                var annualPremium = PayUpTo * contribution_amount;
                return annualPremium;
            }
        }



    }
}