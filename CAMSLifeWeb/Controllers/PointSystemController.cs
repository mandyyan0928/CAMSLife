using AutoMapper;
using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models;
using CaliphWeb.Models.API;
using CaliphWeb.Models.API.AgentRecruit;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    [Authorize]
    public class PointSystemController : Controller
    {

        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;
        private readonly IOne2OneApiHelper _one2oneAPIHelper;

        public PointSystemController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService, IOne2OneApiHelper one2oneAPIHelper )
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
            this._one2oneAPIHelper = one2oneAPIHelper;
        }

        // GET: PointSystem
        public ActionResult List()
        {
            return View();
        }
        [Authorize]
        public async Task<ActionResult> Summary()
        {
            var vm = new PointSummaryViewModel();
            vm.AgentUsers =await _userService.GetAgentUserAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Summary(PointSummaryPostFilter pointSummaryFilter)
        {
            var pointSummary = new List<PointSummary>();
           

            var filter = new ActivityFilterRequest { ActivityStartDate = pointSummaryFilter.StartDate, ActivityEndDate = pointSummaryFilter.EndDate.AddDays(1), CreatedBy = pointSummaryFilter.CreatedBy };
            var responseData = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(filter, "/api/v1/client-deal-activity/get-by-filter");
            responseData.Data = responseData.Data == null ? new List<DealActivity>() : responseData.Data;


            var table = new PointSummaryTable { StartDate = pointSummaryFilter.StartDate, EndDate = pointSummaryFilter.EndDate };
            var activityPoints = await _masterService.GetActivityPointAsync();

            if (UserHelper.GetLoginUserViewModel().IsLeader)
            {
                var agentRecruitActivityFilter = new FilterAgentRecruitActivityRequest { ActivityStartDate = pointSummaryFilter.StartDate, ActivityEndDate = pointSummaryFilter.EndDate, CreatedBy = pointSummaryFilter.CreatedBy, PageSize = 9999, PageNumber = 1 };
                var agentActivityResponse = await _caliphAPIHelper.PostAsync<FilterAgentRecruitActivityRequest, ResponseData<List<AgentRecruimentActivity>>>(agentRecruitActivityFilter, "/api/v1/agent-activity/get-by-filter");
                agentActivityResponse.Data = agentActivityResponse.Data ?? new List<AgentRecruimentActivity>();
                var activities = AutoMapHelper.MapList<AgentRecruimentActivity, DealActivity>(agentActivityResponse.Data);
                responseData.Data.AddRange(activities);
            }
            var user = UserHelper.GetLoginUserViewModel();
            var eventFilter = new EventFilterViewModel { EventDateFrom = pointSummaryFilter.StartDate, EventDateTo = pointSummaryFilter.EndDate, UserEventUserId = user.UserId, AttendanceId = (int)MasterDataEnum.Status.Attended, PageSize = 9999, PageNumber = 1 };
            var eventResponse = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilter, "/api/v1/event/upcoming-get-by-filter");
            eventResponse.Data = eventResponse.Data ?? new List<EventListResponse>();

            var aceReq = new AgentACERequest { agent_id = user.Username, date_from = pointSummaryFilter.StartDate, date_to = pointSummaryFilter.EndDate, type = "p" };
            var aceRes =await  _one2oneAPIHelper.PostAsync<AgentACERequest, One2OneResponse<AgentACEResponse>>(aceReq, "/edfwebapi/alc/agentace",  new One2OneResponse<AgentACEResponse>());
            aceRes.data = aceRes.data ?? new List<AgentACEResponse>();


            foreach (var activitypoint in activityPoints)
            {
                //api contain, remove from here
                //if (activitypoint.Name == "ACE (API) (amount of premium)")
                //    continue;

                var summarData = new PointSummaryData { ActivityPointId= activitypoint.ActivityPointId, ActivityPointDesc=activitypoint.Name, Point= activitypoint.Points };


               
                switch (activitypoint.ActivityPointId)
                {
                    case (int)MasterDataEnum.ActivityPoint.SalesCall:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.SalesCall;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.ApptSecured:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.ApptSecured;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.Survey:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.Survey;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.ApproachInPerson:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.ApprochInPerson;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.ClosingInterview:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.ClosingInterview;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.Servicing_Followup:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.Servicing;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.ReferralsLead:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.RefLeads;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.Sales:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.Sales;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.AgentContracted:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.AgentContracted;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.Training:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.Training;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.JointFieldWork:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.JoinFieldWork;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.Coaching_OneToOne:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.Coaching;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.MiniBOP:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.MiniBop;
                        break;
                    case (int)MasterDataEnum.ActivityPoint.ACE:
                        summarData.Sorting = (int)MasterDataEnum.PointSummaryOrdering.ACE;
                        break;
                    default:
                        summarData.Sorting = 99;
                        break;
                }

                DateTime loopDate = pointSummaryFilter.StartDate;
                while (loopDate <= pointSummaryFilter.EndDate)
                {
                    var activities = responseData.Data.Where(x => x.ActivityStartDate.Value.Date == loopDate && x.ActivityPointId == activitypoint.ActivityPointId).ToList();
                    var summaryDetails = new PointSummary
                    {
                        Date = loopDate,
                        PointReceive = activities.Count(),
                        ClientDealActivityId = activitypoint.ActivityPointId,
                        ClientDealActivityDesc = activitypoint.Name
                    };

                    if (activitypoint.ActivityPointId == (int)MasterDataEnum.ActivityPoint.Training)
                    {
                        var trainingEvents = eventResponse.Data.Where(x => x.EventDateFrom.Date == loopDate).ToList();
                        summaryDetails.PointReceive += trainingEvents.Count();
                    }

                    //if (activitypoint.ActivityPointId == (int)MasterDataEnum.ActivityPoint.ACE)
                    //{
                    //    var ace = aceRes.data.Where(x => x.date == loopDate).ToList();
                    //    summaryDetails.PointReceive += ConvertHelper.ConvertInt( ace.Sum(x => x.ace).ToString());
                    //}

                    summarData.PointSummaries.Add(summaryDetails);
                    loopDate = loopDate.AddDays(1);

                }

                table.PointSummaryData.Add(summarData);
            }
             
            return PartialView("_PointSummaryTable", table);
        }


        [HttpPost]
        public async Task<JsonResult> SummaryDetails(PointSummaryPostDetailsFilter filter)
        {

            var filterReq = new ActivityFilterRequest { ActivityStartDate = filter.StartDate, ActivityEndDate = filter.EndDate, CreatedBy = filter.CreatedBy };
            var responseData = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(filterReq, "/api/v1/client-deal-activity/get-by-filter");
            var activity =  await _masterService.GetActivityPointAsync();
            responseData.Data = (responseData.Data == null ? new List<DealActivity>() : responseData.Data).Where(x=>x.ActivityPointId== filter.ActivityId).ToList();


            if (UserHelper.GetLoginUserViewModel().IsLeader)
            {
                var agentRecruitActivityFilter = new FilterAgentRecruitActivityRequest { ActivityStartDate = filter.StartDate, ActivityEndDate = filter.EndDate, CreatedBy = filter.CreatedBy, PageSize = 9999, PageNumber = 1 };
                var agentActivityResponse = await _caliphAPIHelper.PostAsync<FilterAgentRecruitActivityRequest, ResponseData<List<AgentRecruimentActivity>>>(agentRecruitActivityFilter, "/api/v1/agent-activity/get-by-filter");

                agentActivityResponse.Data = agentActivityResponse.Data ?? new List<AgentRecruimentActivity>();

                //var activities = AutoMapHelper.MapList<AgentRecruimentActivity, DealActivity>(agentActivityResponse.Data);

                var agentActivityconfig = new MapperConfiguration(cfg =>
              cfg.CreateMap<AgentRecruimentActivity, DealActivity>()
              .ForMember(dest => dest.DealTitleDesc, act => act.MapFrom(src => "Agent Recruitment"))
              .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.AgentName))
              .ForMember(dest => dest.ActivityStartDate, act => act.MapFrom(src => src.ActivityStartDate.HasValue ? src.ActivityStartDate.Value.ToString("yyyy-MM-dd HH:mm") : ""))
              .ForMember(dest => dest.Points, act => act.MapFrom(src => src.Points))
              .ForMember(dest => dest.ActivityPointsDesc, act => act.MapFrom(src => src.ActivityPointsDesc))
              .ForMember(dest => dest.ActivityPointId, act => act.MapFrom(src => src.ActivityPointId))
               );
                IMapper iMapper = agentActivityconfig.CreateMapper();
                var activities = iMapper.Map<List<AgentRecruimentActivity>, List<DealActivity>>(agentActivityResponse.Data);
                responseData.Data.AddRange(activities);
            }
            var user = UserHelper.GetLoginUserViewModel();
            var eventFilter = new EventFilterViewModel { EventDateFrom = filter.StartDate, EventDateTo = filter.EndDate, UserEventUserId = user.UserId, AttendanceId = (int)MasterDataEnum.Status.Attended, PageSize = 9999, PageNumber = 1 };
            var eventResponse = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilter, "/api/v1/event/upcoming-get-by-filter");
            eventResponse.Data = eventResponse.Data ?? new List<EventListResponse>();


            var eventConfig = new MapperConfiguration(cfg =>
            cfg.CreateMap<EventListResponse, DealActivity>()
            .ForMember(dest => dest.DealTitleDesc, act => act.MapFrom(src => "Training"))
            .ForMember(dest => dest.ActivityStartDate, act => act.MapFrom(src => src.EventDateFrom.ToString("yyyy-MM-dd HH:mm")))
            .ForMember(dest => dest.Points, act => act.MapFrom(src => 10))
            .ForMember(dest => dest.ActivityPointsDesc, act => act.MapFrom(src => src.EventName))
            .ForMember(dest => dest.ActivityPointId, act => act.MapFrom(src => (int)MasterDataEnum.ActivityPoint.Training)))
;
          IMapper    eventMapper = eventConfig.CreateMapper();
            var events = eventMapper.Map<List<EventListResponse>, List<DealActivity>>(eventResponse.Data);
            responseData.Data.AddRange(events);

            foreach (var d in responseData.Data)
            {
                var currentActivity = activity.Where(x => x.ActivityPointId == filter.ActivityId).FirstOrDefault();
                d.Points = currentActivity == null? 0:currentActivity.Points;
            }
            return Json(responseData.Data);
        }
    }
}