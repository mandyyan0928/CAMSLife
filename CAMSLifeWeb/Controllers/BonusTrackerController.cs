using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Announcement.Request;
using CaliphWeb.Models.API.Announcement.Response;
using CaliphWeb.Models.API.Deal;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace CaliphWeb.Controllers
{
    public class BonusTrackerController : Controller
    {

        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;
        private readonly IALCApiHelper _one2oneAPIHelper;

        public ICaliphAPIHelper CaliphAPIHelper => _caliphAPIHelper;

        public BonusTrackerController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService, IALCApiHelper one2oneAPIHelper)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
            this._one2oneAPIHelper = one2oneAPIHelper;
        }
        // GET: BonusTracker
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> AgentBonusDetails(string id)
        {
            var user = UserHelper.GetLoginUserViewModel();
            if (user.IsAgent || user.IsLeader)
            {
                var startDate = new DateTime(DateTime.Now.Year-1, 1, 1);
                var endDate = new DateTime(DateTime.Now.Year-1, 12, 31);
                var lastMonth = DateTime.Now.AddMonths(-1);
                var daysInMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
                var persistencyDate = new DateTime(2023,2, 28);

                var agent = string.IsNullOrEmpty(id) ? user.Username : id;
                var req = new AgentPolicyRequest
                {
                    agent_id = agent,
                    date_from = startDate,
                    date_to = endDate

                };
                var responseData = await _one2oneAPIHelper.GetDataAsync<AgentPolicyRequest, One2OneResponse<AgentPolicyByProdyctResponse>>(req, "/edfwebapi/alc/AgentPolicyProductData",  new One2OneResponse<AgentPolicyByProdyctResponse>());
                var vm = new  AgentBonusDetailsViewModel ();
                vm.AgentPolicies = (responseData == null || responseData.data == null) ? new List<AgentPolicyByProdyctResponse>() : responseData.data.Where(x => x.selling_agent_code == agent).ToList();

                var agentDetails = vm.AgentPolicies.FirstOrDefault();
                vm.AgentId = agent;
               vm.AgentName = agentDetails==null?"": agentDetails.selling_agent_name;
                vm.PersistencyDate = persistencyDate;
                vm.Year = DateTime.Now.Year;

                vm.BonusTrackerDashboard.ACE = vm.AgentPolicies.Sum(x => x.AnnualisedPremium);
                vm.BonusTrackerDashboard.NetACE = vm.AgentPolicies.Sum(x => x.NetAnnualisedPremium);
                vm.BonusTrackerDashboard.PersistencyDate = persistencyDate;
                vm.BonusTrackerDashboard.PersistencyPremium = vm.AgentPolicies.Where(x=>x.due_date>= persistencyDate).Sum(x => x.AnnualisedPremium);
                vm.BonusTrackerDashboard.PersistencyRatio = vm.BonusTrackerDashboard.ACE == 0 ? 0 : (vm.BonusTrackerDashboard.PersistencyPremium/ vm.BonusTrackerDashboard.ACE) * 100;
                vm.BonusTrackerDashboard.Cases = vm.AgentPolicies.GroupBy(x => x.certificate_no).Count();


                var codedDate = agentDetails == null ? new DateTime(DateTime.Now.Year, 1, 1) : agentDetails.selling_agent_coded_date;
                var bonusContestRules = BonusService.GetContests(codedDate);

                var nextBonus = bonusContestRules.Where(x =>
            vm.BonusTrackerDashboard.NetACE <= x.FromNetACE).OrderBy(x => x.BonusPercent).FirstOrDefault();

                if (nextBonus == null)
                    nextBonus = bonusContestRules.OrderByDescending(x => x.BonusPercent).FirstOrDefault();

                vm.BonusTrackerDashboard.PotentialBonus = nextBonus;
                return View(vm);
            }
            return View();
        }


        public async Task<ActionResult> LeaderBonusSummary()
        {
            var user = UserHelper.GetLoginUserViewModel();
            var vm = new LeaderBonusSummaryViewModel { Year = DateTime.Now.Year };

            if (user.IsAgent || user.IsLeader)
            {
                var startDate = new DateTime(2022, 1, 1);
                var endDate = new DateTime(2022, 12, 31);
                var lastMonth = DateTime.Now.AddMonths(-1);
                var daysInMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
                var persistencyDate = new DateTime(2023, 2,28);

                var req = new AgentPolicyRequest
                {
                    agent_id = user.Username,
                    date_from = startDate,
                    date_to = endDate

                };
                var responseData = await _one2oneAPIHelper.GetDataAsync<AgentPolicyRequest, One2OneResponse<AgentPolicyByProdyctResponse>>(req, "/edfwebapi/alc/AgentPolicyProductData",   new One2OneResponse<AgentPolicyByProdyctResponse>());
               var policies = (responseData == null || responseData.data == null) ? new List<AgentPolicyByProdyctResponse>() : responseData.data.ToList();



                var groupPolicyByAgent = policies.GroupBy(x => x.selling_agent_code).ToList();


                foreach (var agentPolicy in groupPolicyByAgent)
                {
                    var agentData = agentPolicy.FirstOrDefault();
                    if (agentData == null)
                        continue;


                    var ace = agentPolicy.Sum(x => x.AnnualisedPremium);
                    var netAce = agentPolicy.Sum(x => x.NetAnnualisedPremium);
                    var cases = agentPolicy.GroupBy(x => x.certificate_no).Count();
                    var fyc = agentPolicy.Sum(x => x.TotalPremiumCollected);
                    var persistencyPremium = agentPolicy.Where(x => x.due_date >= persistencyDate).Sum(x => x.AnnualisedPremium);
                    var persistencyRatio = ace == 0 ? 0 : (persistencyPremium / ace) * 100;
                    
                    var bonusContestRules = BonusService.GetContests(agentData.selling_agent_coded_date);

                    var qualifiedBonus = bonusContestRules.Where(x =>
                        netAce >= x.FromNetACE &&
                        cases >= x.Cases &&
                        persistencyRatio >= x.PR_D0).OrderBy(x => x.BonusPercent).ToList();
                    
                    var nextBonus = bonusContestRules.Where(x =>
                        netAce <= x.FromNetACE).OrderBy(x => x.BonusPercent).FirstOrDefault();

                    if (nextBonus == null)
                        nextBonus = bonusContestRules.OrderByDescending(x => x.BonusPercent).FirstOrDefault();

                    vm.LeaderSummaryDatas.Add(new LeaderSummaryData
                    {
                        AgentName = agentData.selling_agent_name,
                        AgentId = agentData.selling_agent_code,
                        InForceDate = agentData.selling_agent_coded_date,
                        ACE = ace,
                        Cases = cases,
                        NetACE = netAce,
                        PersistencyDate = persistencyDate,
                        PersistencyPremium = persistencyPremium,
                        PersistencyRatio = persistencyRatio,
                        TotalPremiumCollected_FYC = fyc,
                        QualifiedBonus = qualifiedBonus,
                        PotentialBonus = nextBonus,
                    });
                }


                DataTable dt = new DataTable("Bonus Report");
                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("AgentName"),
                 new DataColumn("AgentId"),
                 new DataColumn("InForceDate"),
                 new DataColumn("ACE"),
                 new DataColumn("Cases"),
                 new DataColumn("NetACE"),
                 new DataColumn("PersistencyPremium"),
                 new DataColumn("PersistencyRatio"),
                 new DataColumn("TotalPremiumCollected_FYC"),
                 new DataColumn("QualifiedBonus") });

                TempData["Bonus"] = dt;
                foreach (var column in vm.LeaderSummaryDatas)
                {
                    dt.Rows.Add(column.AgentName, column.AgentId, column.InForceDate, column.ACE, column.Cases,
                        column.NetACE, column.PersistencyPremium, column.PersistencyRatio, column.TotalPremiumCollected_FYC
                        , column.QualifiedBonus);
                }
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> AgentBonusDetails(AgentPolicyRequest request)
        {
            var user = UserHelper.GetLoginUserViewModel();
            if (user.IsAgent || user.IsLeader)
            {
                var lastMonth = request.date_to;
                var daysInMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
                var persistencyDate = new DateTime(lastMonth.Year, lastMonth.Month, daysInMonth);

                request.agent_id = user.Username;
                var responseData = await _one2oneAPIHelper.GetDataAsync<AgentPolicyRequest, One2OneResponse<AgentPolicyByProdyctResponse>>(request, "/edfwebapi/alc/AgentPolicyProductData",  new One2OneResponse<AgentPolicyByProdyctResponse>());
                var vm = new AgentBonusDetailsViewModel();
                vm.AgentPolicies = (responseData == null || responseData.data == null) ? new List<AgentPolicyByProdyctResponse>() : responseData.data.Where(x => x.selling_agent_code == user.Username).ToList();

                return PartialView("~/Views/BonusTracker/_AgentBonusPolicies.cshtml", vm);
            }
            return PartialView("~/Views/BonusTracker/_AgentBonusPolicies.cshtml", new AgentBonusDetailsViewModel());
        }
    }
}