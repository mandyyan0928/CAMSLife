using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Budget
{
    public class BudgetFilter
    {
        public int? BudgetId { get; set; }
        public int? BudgetYear { get; set; }
        public int? BudgetMonth { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public int PageSize { get; set; } = 999;
        public int PageNumber { get; set; } = 1;
    }

    public class MonthlyFilter
    {
        public int? BudgetMonthlyId { get; set; }
        public int? UserId { get; set; }
        public int? BudgetYear { get; set; }
        public int? BudgetMonth { get; set; }
        public int? MonthlyBudgetTypeId { get; set; } //115 = highend, 116= other
        public int? StatusId { get; set; }
        public int PageSize { get; set; } = 999;
        public int PageNumber { get; set; } = 1;
    }

    public class StrategyFilter
    {
        public int? BudgetStrategyId { get; set; }
        public int? UserId { get; set; }
        public int? BudgetStrategyYear { get; set; }
        public int? StatusId { get; set; }
        public int PageSize { get; set; } = 999;
        public int PageNumber { get; set; } = 1;
    }

    public class SimulatorAdd
    {
        public int UserId { get; set; }
        public int BudgetId { get; set; }
        public int BudgetYear { get; set; }
        public int BudgetMonth { get; set; }
        public string PricePlan { get; set; }
        public decimal ProductPricePlan { get; set; }
        public int ProductStartMonth { get; set; }
        public int ProductQtyMonth1 { get; set; }
        public int ProductQtyMonth2 { get; set; }
        public int ProductQtyMonth3 { get; set; }
        public int ProductQtyMonth4 { get; set; }
        public int ProductQtyMonth5 { get; set; }
        public int ProductQtyMonth6 { get; set; }
        public int ProductQtyMonth7 { get; set; }
        public int ProductQtyMonth8 { get; set; }
        public int ProductQtyMonth9 { get; set; }
        public int ProductQtyMonth10 { get; set; }
        public int ProductQtyMonth11 { get; set; }
        public int ProductQtyMonth12 { get; set; }
        public int RecruitmentCount1 { get; set; }
        public int RecruitmentCount2 { get; set; }
        public int RecruitmentCount3 { get; set; }
        public int RecruitmentCount4 { get; set; }
        public int RecruitmentCount5 { get; set; }
        public int RecruitmentCount6 { get; set; }
        public int RecruitmentCount7 { get; set; }
        public int RecruitmentCount8 { get; set; }
        public int RecruitmentCount9 { get; set; }
        public int RecruitmentCount10 { get; set; }
        public int RecruitmentCount11 { get; set; }
        public int RecruitmentCount12 { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }

    public class PropotionUpdate
    {
        public int BudgetId { get; set; }
        public decimal BudgetProportionPercentage1 { get; set; }
        public decimal BudgetProportionPercentage2 { get; set; }
        public decimal BudgetProportionPercentage3 { get; set; }
        public decimal BudgetProportionPercentage4 { get; set; }
        public decimal BudgetProportionPercentage5 { get; set; }
        public decimal BudgetProportionPercentage6 { get; set; }
        public decimal BudgetProportionPercentage7 { get; set; }
        public decimal BudgetProportionPercentage8 { get; set; }
        public decimal BudgetProportionPercentage9 { get; set; }
        public decimal BudgetProportionPercentage10 { get; set; }
        public decimal BudgetProportionPercentage11 { get; set; }
        public decimal BudgetProportionPercentage12 { get; set; }
        public decimal BudgetProportionAmt1 { get; set; }
        public decimal BudgetProportionAmt2 { get; set; }
        public decimal BudgetProportionAmt3 { get; set; }
        public decimal BudgetProportionAmt4 { get; set; }
        public decimal BudgetProportionAmt5 { get; set; }
        public decimal BudgetProportionAmt6 { get; set; }
        public decimal BudgetProportionAmt7 { get; set; }
        public decimal BudgetProportionAmt8 { get; set; }
        public decimal BudgetProportionAmt9 { get; set; }
        public decimal BudgetProportionAmt10 { get; set; }
        public decimal BudgetProportionAmt11 { get; set; }
        public decimal BudgetProportionAmt12 { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class GroupAdd {
        public int? BudgetId { get; set; }
        public int? BudgetGroupId { get; set; }
        public string BudgetTitle { get; set; }
        public string TargetCount { get; set; }
        public string TargetComm { get; set; }
        public int? UserId { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }

    public class MonthlyAdd {
        public int UserId { get; set; }
        public int? BudgetYear { get; set; }
        public int? BudgetMonth { get; set; }
        public int? MonthlyBudgetTypeId { get; set; }
        public decimal MonthlyBudgetPercentage { get; set; }
        public string NoOfCases { get; set; }
        public string PersonName { get; set; }
        public int? ClientId { get; set; }
        public decimal BudgetValue { get; set; }
        public string AchieveValue { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }
    public class MonthlyUpdate
    {
        public int BudgetMonthlyId { get; set; }
        public decimal MonthlyBudgetPercentage { get; set; }
        public string NoOfCases { get; set; }
        public string PersonName { get; set; }
        public decimal BudgetValue { get; set; }
        public string AchieveValue { get; set; }
        public int? ClientId { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class StrategyAdd
    {
        public int UserId { get; set; }
        public int? BudgetStrategyId { get; set; }
        public int? BudgetStrategyYear { get; set; }
        public int? NoOfCasesForTheYear { get; set; }
        public decimal GoalACEValue { get; set; }
        public decimal HighEndPercentage { get; set; }
        public decimal LowEndPercentage { get; set; }
        public decimal HighEndAveragePremium { get; set; }
        public decimal LowEndAveragePremium { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }



    public class UpdateBudget
    {
        public int BudgetId { get; set; }
        public int TargetApptClosingRatio { get; set; }
        public int TargetApptCallRatio { get; set; }
        public string UpdatedBy { get; set; }
    }
}