using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.ALCData;
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
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;
        private readonly IALCDataGetter _alcDataGetter;

        public ICaliphAPIHelper CaliphAPIHelper => _caliphAPIHelper;

        public HomeController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService, IALCDataGetter alcDataGetter)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
            this._alcDataGetter = alcDataGetter;
        }
        public async Task<ActionResult> Index()
        {


            var dealfilter = new DealFilterRequest { PageNumber = 1, PageSize = 9999, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var dealresponseData = await CaliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(dealfilter, "/api/v1/client-deal/get-by-filter");


            var clientfilter = new ClientFilterRequest { PageNumber = 1, PageSize = 9999, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var clientresponseData = await CaliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(clientfilter, "/api/v1/client/get-by-client-filter");


            var activityFilter = new ActivityFilterRequest { PageNumber = 1, PageSize = 9999, ActivityStartDate = DateTime.Now, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var activityresponseData = await CaliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(activityFilter, "/api/v1/client-deal-activity/get-by-filter");
            var vm = new IndexViewModel();

            if (dealresponseData == null || dealresponseData.Data == null)
            {
                dealresponseData = new ResponseData<List<Deal>> { Data = new List<Deal>() };
            }
            if (dealresponseData != null && dealresponseData.Data != null)
            {
                vm.TotalDeal = dealresponseData.Data.Where(x =>
                 x.StatusId == (int)MasterDataEnum.Status.Active ||
                 x.StatusId == (int)MasterDataEnum.Status.Closed ||
                 x.StatusId == (int)MasterDataEnum.Status.Lost).Count();


                vm.ActiveDeal = dealresponseData.Data.Where(x =>
                 x.StatusId == (int)MasterDataEnum.Status.Active).Count();


                vm.ClosedDeal = dealresponseData.Data.Where(x =>
                 x.StatusId == (int)MasterDataEnum.Status.Closed).Count();
            }

            if (clientresponseData != null && clientresponseData.Data != null)
            {
                vm.TotalClient = clientresponseData.Data.Where(x =>
                 x.StatusId == (int)MasterDataEnum.Status.Confirm ||
                 x.StatusId == (int)MasterDataEnum.Status.Potential).Count();

                vm.ConfirmedClient = clientresponseData.Data.Where(x =>
           x.StatusId == (int)MasterDataEnum.Status.Confirm).Count();
            }


            if (activityresponseData != null && activityresponseData.Data != null)
            {

                vm.UpcomingActivity = activityresponseData.Data.Where(x =>
         x.StatusId == (int)MasterDataEnum.Status.Active).Count();
            }

            var startDate = DateTime.Now.AddDays(-6);
            var endDate = DateTime.Now;

            var loopDate = startDate;

            while (loopDate <= endDate)
            {
                var activeDeals = dealresponseData.Data.Where(x => (x.StatusId == (int)MasterDataEnum.Status.Active ||
                x.StatusId == (int)MasterDataEnum.Status.Closed ||
                x.StatusId == (int)MasterDataEnum.Status.Lost) && x.CreatedDate.Date == loopDate.Date).Count();
                var closedDeals = dealresponseData.Data.Where(x => x.StatusId == (int)MasterDataEnum.Status.Closed && x.CreatedDate.Date == loopDate.Date).Count();
                var lostDeals = dealresponseData.Data.Where(x => x.StatusId == (int)MasterDataEnum.Status.Lost && x.CreatedDate.Date == loopDate.Date).Count();

                vm.WeeklyActiveDeal.Add(activeDeals);
                vm.WeeklyClosedDeal.Add(closedDeals);
                vm.WeeklyLostDeal.Add(lostDeals);
                vm.WeeklyDays.Add(loopDate.Date.ToString("dddd"));
                loopDate = loopDate.AddDays(1);

            }

            var user = UserHelper.GetLoginUserViewModel();
            var filter = new AnnouncementGet
            {
                PageSize = 100,
                PageNumber = 1,
                UserId = user.UserId,
                PublishEndDate = DateTime.Now
            };

            var response = await _caliphAPIHelper.PostAsync<AnnouncementGet, ResponseData<List<Announcement>>>(filter, "/api/v1/announcement/get-by-filter")?? new ResponseData<List<Announcement>>{ Data = new List<Announcement>()};
            if (response.Data != null)
            {
                vm.Announcements = response.Data.Where(x => x.PublishEndDate >= DateTime.Now).ToList();
            }
            else
            {
                vm.Announcements = new List<Announcement>();
            }


            var firstdayOfTheTodayMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var filter1 = new AgentCommissionGet
            {
                PayoutDateFrom = firstdayOfTheTodayMonth,
                PayoutDateTo = DateTime.Now.Date,
                Username = user.Username,
                PageSize = 100,
                PageNumber = 1,
            };

            var response1 = await _caliphAPIHelper.PostAsync<AgentCommissionGet, ResponseData<List<AgentCommission>>>(filter1, "/api/v1/agent-commission/get-by-filter");
            decimal total = 0;
            if (response1.Data != null && response1.Data.Count > 0)
            {
                foreach (var item in response1.Data)
                {
                    total = total + item.CommAmt;
                }
            }
            vm.TotalCommissionAmt = total;


          
            return View(vm);
        }


        [HttpPost]
        public async Task<ActionResult> GetACE(string type )
        {
            var user = UserHelper.GetTopLeader();

            var req = new AgentMapaRequest { agent_id = user, type = type, start_month = 1,
                end_month = DateTime.Now.Month,
                start_year = DateTime.Now.Year, end_year = DateTime.Now.Year

            };
            var responseData = await _alcDataGetter.GetMapaAsync(req);
            var ace = responseData.OrderByDescending(x => x.month).FirstOrDefault();

            return Json(ace);
        }

        [HttpPost]
        public async Task<ActionResult> GetPersistency(string type)
        {

            var startDate = DateTime.Now;
            var endDate = DateTime.Now;
            var persistencyDate = DateTime.Now;
            if (type == "d0")
            {
                startDate = new DateTime(DateTime.Now.Year, 1, 1);
                endDate = new DateTime(DateTime.Now.Year, 12, 31);
                var lastMonth = DateTime.Now.AddMonths(-1);
                var daysInMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
                persistencyDate = new DateTime(lastMonth.Year, lastMonth.Month, daysInMonth);
            }
            else if (type == "d1")
            {
                startDate = new DateTime(  DateTime.Now.Year - 1,1,1);
                endDate = new DateTime( DateTime.Now.Year - 1, 12, 31);
                var lastMonth = DateTime.Now.AddMonths(-1);
                var daysInMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
                persistencyDate = new DateTime(lastMonth.Year, lastMonth.Month, daysInMonth);
            }


            var user = UserHelper.GetTopLeader();

            var req = new AgentPolicyRequest
            {
                agent_id = user,
                date_from = startDate, date_to = endDate

            };
            var responseData = await _alcDataGetter.GetPolicyDataAsync(req);
            var vm = new PersistencySummaryData { StartDate = startDate, EndDate = endDate, PersistencyDate = persistencyDate, AgentId = user, GroupPolicies = responseData };
            var personalPolicies = vm.GroupPolicies.Where(x => x.selling_agent_code == user).ToList();
            vm.PersonalPolicies = (personalPolicies == null) ? new List<AgentPolicyResponse>() : personalPolicies;
            return Json(new PersistencySummary { GroupRatio = vm.GroupRatio, PersonalRatio = vm.PersonalRatio, PersistencyDate = vm.PersistencyDate });

          //  return Json(new PersistencySummary { GroupRatio = 0, PersonalRatio = 0, PersistencyDate = DateTime.Now });
        }


        [HttpPost]
        public async Task<ActionResult> GetBonusTracker()
        {
            var user = UserHelper.GetLoginUserViewModel();
            if (user.IsAgent || user.IsLeader)
            {
                var startDate = new DateTime(DateTime.Now.Year, 1, 1);
                var endDate = new DateTime(DateTime.Now.Year, 12, 31);
                var lastMonth = DateTime.Now.AddMonths(-1);
                var daysInMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);
                var persistencyDate = new DateTime(lastMonth.Year, lastMonth.Month, daysInMonth);

                var req = new AgentPolicyRequest
                {
                    agent_id = user.Username,
                    date_from = startDate,
                    date_to = endDate

                };
                var responseData = await _alcDataGetter.GetPolicyDataAsync(req);
                var  vm = new BonusTrackerViewModel { PersistencyDate = persistencyDate };

                // ** bonus tracker not in use , and new SLM API does not have this API **
                //    vm.AgentProductPolicies = (responseData == null || responseData.data == null) ? new List<AgentPolicyByProdyctResponse>() : responseData.data.Where(x=>x.selling_agent_code== user.Username).ToList();

                double persistencyPremium = 0;
                foreach (var policy in vm.AgentProductPolicies)
                {
                    if (policy.due_date >= persistencyDate)
                        persistencyPremium += policy.AnnualisedPremium;
                }
                var agentData = vm.AgentProductPolicies.FirstOrDefault()?? new AgentPolicyByProdyctResponse();
                var bonusContestRules = BonusService.GetContests(agentData.selling_agent_coded_date);
                var bonusDashboard = AutoMapHelper.Map<BonusTrackerViewModel, BonusTrackerDashboard>(vm);

                bonusDashboard.PersistencyRatio = bonusDashboard.ACE>0? (persistencyPremium / bonusDashboard.ACE)*100:0;
              

                bonusDashboard.QualifiedBonus = bonusContestRules.Where(
                    x =>
                bonusDashboard.NetACE >= x.FromNetACE &&
                bonusDashboard.Cases >= x.Cases &&
                bonusDashboard.PersistencyRatio >= x.PR_D0).OrderBy(x => x.BonusPercent).ToList();

               
                var nextBonus = bonusContestRules.Where(x =>  
                bonusDashboard.NetACE <= x.FromNetACE  ).OrderBy(x => x.BonusPercent).FirstOrDefault();

                if (nextBonus == null)
                    nextBonus = bonusContestRules.OrderByDescending(x => x.BonusPercent).FirstOrDefault();
                bonusDashboard.PotentialBonus = nextBonus; //bonusContestRules[nextBonus-1] ;
                return PartialView("~/Views/Home/_BonusTrackerDashboard.cshtml", bonusDashboard);
            }
            return Json("");
        }

        [HttpPost]
        public async Task<ActionResult> GetCPDPoint()
        {

           
            var user = UserHelper.GetLoginUserViewModel();

            var userEvent = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(new EventFilterViewModel {  UserId=user.UserId , AttendanceId = (int)MasterDataEnum.Status.Attended  }, "/api/v1/event/user-get-by-filter");
            var responseData =    (userEvent == null || userEvent.Data == null) ? new List<EventAttendanceListResponse>() : userEvent.Data;
            var cpd = responseData.Sum(x => x.CPDPoint);
            return Json(cpd);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}