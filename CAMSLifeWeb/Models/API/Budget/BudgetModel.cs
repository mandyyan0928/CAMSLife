using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaliphWeb.ViewModel;


namespace CaliphWeb.Models.API.Budget
{
    public class BudgetModel
    {
        public BudgetModel()
        {
            BudgetData = new BudgetData();
            Income = new Income();
            Monthly = new Monthly();
            Strategy = new Strategy();
        }
        public BudgetData BudgetData { get; set; }
        public Income Income { get; set; }
        public Monthly Monthly { get; set; }
        public Strategy Strategy { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }
       
    }
    public class BudgetData
    {
        public BudgetData()
        {
            BudgetView = new List<BudgetView>();
         
        }
        public List<BudgetView> BudgetView { get; set; }
       
    }

    public class Income
    {
        public Income()
        {
            IncomeData = new BudgetView();
            IncomeGroup = new BudgetGroup();
        }
        public BudgetView IncomeData { get; set; }
        public BudgetGroup IncomeGroup { get; set; }
    }

    public class Monthly
    {
        public Monthly()
        {
            BudgetMonthly = new List<BudgetMonthly>();
            Clients = new List<CaliphWeb.ViewModel.Client>();
        }
        public List<BudgetMonthly> BudgetMonthly { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal MonthlyBudget { get; set; }
        public List<CaliphWeb.ViewModel.Client> Clients { get; set; }
    }

    public class Strategy
    {
        public Strategy()
        {
            BudgetStrategy = new BudgetStrategy();
        }
        public BudgetStrategy BudgetStrategy { get; set; }
        public int Year { get; set; }
    }


    public class BudgetView 
    {
        public BudgetView()
        {
            BudgetMonth = 0;
            BudgetGroupList = new List<BudgetGroup>();
        }
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal TargetApptClosingRatio { get; set; }
        public decimal TargetApptCallRatio { get; set; }
        public int? BudgetYear { get; set; }
        public int? BudgetMonth { get; set; }
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
        public string Remarks { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public List<BudgetGroup> BudgetGroupList { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public List<PropotionACE> PropotionACEs { get; set; } = new List<PropotionACE>();
    }

public class PropotionACE {
    public int Month { get; set; }
        public int Year { get; set; }
        public decimal ACE { get; set; }
}

    public class BudgetGroup
    {
        public int BudgetGroupId { get; set; }
        public int BudgetId { get; set; }
        public string BudgetTitle { get; set; }
        public int UserId { get; set; }
        public int TargetCount { get; set; }
        public decimal TargetComm { get; set; }
        public decimal TotalCase { get; set; }
        public string Remarks { get; set; }
        public int StatusId { get; set; }        
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }

    public class BudgetMonthly 
    {
        public int BudgetMonthlyId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int? BudgetYear { get; set; }
        public int BudgetMonth { get; set; }
        public int MonthlyBudgetTypeId { get; set; }
        public string MonthlyBudgetTypeDesc { get; set; }
        public decimal MonthlyBudgetPercentage { get; set; }
        public int NoOfCases { get; set; }
        public string PersonName { get; set; }
        public decimal BudgetValue { get; set; }
        public decimal AchieveValue { get; set; }
        public string Remarks { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class BudgetStrategy
    {
        public int BudgetStrategyId { get; set; }
        public int UserId  { get; set; }
        public string UserName { get; set; }
        public int? BudgetStrategyYear { get; set; }
        public int NoOfCasesForTheYear { get; set; }
        public decimal GoalACEValue { get; set; }
        public decimal HighEndPercentage { get; set; }
        public decimal LowEndPercentage { get; set; }
        public decimal HighEndACEValue { get; set; }
        public decimal LowEndACEValue { get; set; }
        public decimal HighEndAveragePremium { get; set; }
        public decimal LowEndAveragePremium { get; set; }
        public decimal HighEndNoOfCases { get; set; }
        public decimal LowEndNoOfCases { get; set; }
        public string Remarks { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

}