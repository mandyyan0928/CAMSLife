using AutoMapper;
using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models;
using CaliphWeb.Models.API;
using CaliphWeb.Models.API.AgentRecruit;
using CaliphWeb.Models.API.Deal;
using CaliphWeb.Models.API.UserActivity.Request;
using CaliphWeb.Models.API.UserActivity.Response;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{

    [Authorize]
    public class ActivityController : Controller
    {

        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public ActivityController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }
        // GET: Activity
        //get client-deal/get-by-filter
        public async Task<ActionResult> Add()
        {
            var addActivityVM = new AddActivityViewModel();

            //add client
            var filterClient = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filterClient, "/api/v1/client/get-by-client-filter");
            addActivityVM.Clients = responseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();

            var filter = new DealFilterRequest {  PageNumber=1, PageSize=9999, StatusId=(int)MasterDataEnum.Status.Active, CreatedBy = UserHelper.GetLoginUser() };
            var responseDeal = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");
            addActivityVM.Deals = responseDeal.Data;
            ViewBag.Deal = addActivityVM.Deals;
            addActivityVM.Activities = await _masterService.GetSalesActivityPointAsync();

            return View(addActivityVM);
        }


        //submit client-deal-activity/add
        [HttpPost]
        public async Task<JsonResult> Add(FormCollection formCollection)
        {
            string eventId = "";
            string email = "";
            var AddToGoogle = formCollection["AddToGoogle"];

            if (AddToGoogle == "1")
            {
                var clientID = int.Parse(formCollection["ClientId"]);

                //getClientNamebyClientID
                var filterClient = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy = UserHelper.GetDefaultSearchUser(), ClientId = clientID };
                var responseDataList = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filterClient, "/api/v1/client/get-by-client-filter");
                var responseData = responseDataList.Data.Select(x => x.Name).FirstOrDefault().ToString();

                //getDealNamebyDealID
                var ClientDealId = int.Parse(formCollection["ClientDealId"]);
                var filter = new DealFilterRequest { PageNumber = 1, PageSize = 9999, StatusId = (int)MasterDataEnum.Status.Active, ClientId = clientID };
                var responseDealList = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");
                var responseDeal = responseDealList.Data.Where(x => x.ClientDealId == ClientDealId).Select(x => x.Name).SingleOrDefault().ToString();

                //getActivityPointId
                var ActivityPointId = int.Parse(formCollection["ActivityPointId"]);
                var responseActList = await _masterService.GetSalesActivityPointAsync();
                var responseAct = responseActList.Where(x => x.ActivityPointId == ActivityPointId).Select(x => x.Name).SingleOrDefault().ToString();

                var addGoogleCalendar = new GoogleCalender()
                {
                    Summary = responseDeal + " - " + responseAct,
                    Description = responseAct + " for " + responseDeal + " , " + responseData,
                    Start = DateTime.Parse(formCollection["ActivityStartDate"], null, System.Globalization.DateTimeStyles.RoundtripKind),
                    End = DateTime.Parse(formCollection["ActivityEndDate"], null, System.Globalization.DateTimeStyles.RoundtripKind)
                };

                string tokenPath = Server.MapPath("~/Cre/token.json");
                string crePath = Server.MapPath("~/Cre/Cre.json");
                var googleResponse = await GoogleCalendarHelper.CreateGoogleCalendar(addGoogleCalendar, tokenPath, crePath);
                if (googleResponse.Status == "confirmed")
                {
                    eventId = googleResponse.Id;
                    email = googleResponse.Organizer.Email;
                }
            }
            var addActivity = FormCollectionMapper.FormToModel<AddActivityRequest>(formCollection);
            if (addActivity.ActivityPointId == (int)MasterDataEnum.ActivityPoint.ReferralsLead)
            { 
                var leadResponse = AddLead(formCollection);
                return Json(leadResponse);
            }
            addActivity.CreatedBy = UserHelper.GetLoginUser();
            addActivity.EventId = (!string.IsNullOrEmpty(eventId) ? eventId : null);
            addActivity.Email = (!string.IsNullOrEmpty(email) ? email : null);
            addActivity.GoogleLinked = (AddToGoogle=="1" ? true : false);
            var response = await _caliphAPIHelper.PostAsync<AddActivityRequest, ResponseData<string>>(addActivity, "/api/v1/client-deal-activity/add");
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddUserActivity(FormCollection formCollection)
        {

            var addActivity = FormCollectionMapper.FormToModel<AddUserActivityRequest>(formCollection);
            addActivity.CreatedBy = UserHelper.GetLoginUser();
            addActivity.UserId = UserHelper.GetLoginUserViewModel().UserId;
            var response = await _caliphAPIHelper.PostAsync<AddUserActivityRequest, ResponseData<string>>(addActivity, "/api/v1/user-activity/add");
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> EditPlannedWeeklyGoal(string ssdate, string ReviewText1,string ReviewText2, string ReviewText3, string ReviewText4, string ReviewText5, string ReviewText6
            , string ReviewText7, string ReviewText8, string ReviewText9, string ReviewText10, string ReviewText11, int LoginId, int ActivityReviewId)
        {
            if (ActivityReviewId == -1)
            {
                AddActivityReviewRequest addActivityReview = new AddActivityReviewRequest();
                addActivityReview.CreatedBy = UserHelper.GetLoginUser();
                addActivityReview.UserId = LoginId;
                addActivityReview.DateInWeek = ssdate;
                addActivityReview.ReviewText1 = string.IsNullOrEmpty(ReviewText1) ? null : ReviewText1;
                addActivityReview.ReviewText2 = string.IsNullOrEmpty(ReviewText2) ? null : ReviewText2;
                addActivityReview.ReviewText3 = string.IsNullOrEmpty(ReviewText3) ? null : ReviewText3;
                addActivityReview.ReviewText4 = string.IsNullOrEmpty(ReviewText4) ? null : ReviewText4;
                addActivityReview.ReviewText5 = string.IsNullOrEmpty(ReviewText5) ? null : ReviewText5;
                addActivityReview.ReviewText6 = string.IsNullOrEmpty(ReviewText6) ? null : ReviewText6;
                addActivityReview.ReviewText7 = string.IsNullOrEmpty(ReviewText7) ? null : ReviewText7;
                addActivityReview.ReviewText8 = string.IsNullOrEmpty(ReviewText8) ? null : ReviewText8;
                addActivityReview.ReviewText9 = string.IsNullOrEmpty(ReviewText9) ? null : ReviewText9;
                addActivityReview.ReviewText10 = string.IsNullOrEmpty(ReviewText10) ? null : ReviewText10;
                addActivityReview.ReviewText11 = string.IsNullOrEmpty(ReviewText11) ? null : ReviewText11;
                addActivityReview.ActivityReviewTypeId = (int)MasterDataEnum.ActivityReviewType.PlannedActivity;
                var response = await _caliphAPIHelper.PostAsync<AddActivityReviewRequest, ResponseData<string>>(addActivityReview, "/api/v1/activity-review/add");

                return Json(response);
            }
            else
            {
                UpdateActivityReviewRequest updateActivityReview = new UpdateActivityReviewRequest();
                updateActivityReview.UserId = LoginId;
                updateActivityReview.DateInWeek = ssdate;
                updateActivityReview.ReviewText1 = string.IsNullOrEmpty(ReviewText1) ? null : ReviewText1;
                updateActivityReview.ReviewText2 = string.IsNullOrEmpty(ReviewText2) ? null : ReviewText2;
                updateActivityReview.ReviewText3 = string.IsNullOrEmpty(ReviewText3) ? null : ReviewText3;
                updateActivityReview.ReviewText4 = string.IsNullOrEmpty(ReviewText4) ? null : ReviewText4;
                updateActivityReview.ReviewText5 = string.IsNullOrEmpty(ReviewText5) ? null : ReviewText5;
                updateActivityReview.ReviewText6 = string.IsNullOrEmpty(ReviewText6) ? null : ReviewText6;
                updateActivityReview.ReviewText7 = string.IsNullOrEmpty(ReviewText7) ? null : ReviewText7;
                updateActivityReview.ReviewText8 = string.IsNullOrEmpty(ReviewText8) ? null : ReviewText8;
                updateActivityReview.ReviewText9 = string.IsNullOrEmpty(ReviewText9) ? null : ReviewText9;
                updateActivityReview.ReviewText10 = string.IsNullOrEmpty(ReviewText10) ? null : ReviewText10;
                updateActivityReview.ReviewText11 = string.IsNullOrEmpty(ReviewText11) ? null : ReviewText11;
                updateActivityReview.ActivityReviewTypeId = (int)MasterDataEnum.ActivityReviewType.PlannedActivity;
                updateActivityReview.UpdatedBy = UserHelper.GetLoginUser();
                updateActivityReview.ActivityReviewId = ActivityReviewId;
                var response = await _caliphAPIHelper.PostAsync<UpdateActivityReviewRequest, ResponseData<string>>(updateActivityReview, "/api/v1/activity-review/update");
                return Json(response);
            }
        }

        [HttpPost]
        public async Task<JsonResult> EditActualWeeklyGoal(string ssdate, string ReviewText1, string ReviewText2, string ReviewText3, string ReviewText4, string ReviewText5, string ReviewText6
    , string ReviewText7, string ReviewText8, string ReviewText9, string ReviewText10, string ReviewText11, int LoginId, int ActivityReviewId)
        {

            if (ActivityReviewId==-1)
            {
                AddActivityReviewRequest addActivityReview = new AddActivityReviewRequest();
                addActivityReview.CreatedBy = UserHelper.GetLoginUser();
                addActivityReview.UserId = LoginId;
                addActivityReview.DateInWeek = ssdate;
                addActivityReview.ReviewText1 = string.IsNullOrEmpty(ReviewText1) ? null : ReviewText1;
                addActivityReview.ReviewText2 = string.IsNullOrEmpty(ReviewText2) ? null : ReviewText2;
                addActivityReview.ReviewText3 = string.IsNullOrEmpty(ReviewText3) ? null : ReviewText3;
                addActivityReview.ReviewText4 = string.IsNullOrEmpty(ReviewText4) ? null : ReviewText4;
                addActivityReview.ReviewText5 = string.IsNullOrEmpty(ReviewText5) ? null : ReviewText5;
                addActivityReview.ReviewText6 = string.IsNullOrEmpty(ReviewText6) ? null : ReviewText6;
                addActivityReview.ReviewText7 = string.IsNullOrEmpty(ReviewText7) ? null : ReviewText7;
                addActivityReview.ReviewText8 = string.IsNullOrEmpty(ReviewText8) ? null : ReviewText8;
                addActivityReview.ReviewText9 = string.IsNullOrEmpty(ReviewText9) ? null : ReviewText9;
                addActivityReview.ReviewText10 = string.IsNullOrEmpty(ReviewText10) ? null : ReviewText10;
                addActivityReview.ReviewText11 = string.IsNullOrEmpty(ReviewText11) ? null : ReviewText11;
                addActivityReview.ActivityReviewTypeId = (int)MasterDataEnum.ActivityReviewType.ActualActivity;
                var response = await _caliphAPIHelper.PostAsync<AddActivityReviewRequest, ResponseData<string>>(addActivityReview, "/api/v1/activity-review/add");

                return Json(response);
            }
            else
            {
                UpdateActivityReviewRequest updateActivityReview = new UpdateActivityReviewRequest();
                updateActivityReview.UserId = LoginId;
                updateActivityReview.DateInWeek = ssdate;
                updateActivityReview.ReviewText1 = string.IsNullOrEmpty(ReviewText1) ? null : ReviewText1;
                updateActivityReview.ReviewText2 = string.IsNullOrEmpty(ReviewText2) ? null : ReviewText2;
                updateActivityReview.ReviewText3 = string.IsNullOrEmpty(ReviewText3) ? null : ReviewText3;
                updateActivityReview.ReviewText4 = string.IsNullOrEmpty(ReviewText4) ? null : ReviewText4;
                updateActivityReview.ReviewText5 = string.IsNullOrEmpty(ReviewText5) ? null : ReviewText5;
                updateActivityReview.ReviewText6 = string.IsNullOrEmpty(ReviewText6) ? null : ReviewText6;
                updateActivityReview.ReviewText7 = string.IsNullOrEmpty(ReviewText7) ? null : ReviewText7;
                updateActivityReview.ReviewText8 = string.IsNullOrEmpty(ReviewText8) ? null : ReviewText8;
                updateActivityReview.ReviewText9 = string.IsNullOrEmpty(ReviewText9) ? null : ReviewText9;
                updateActivityReview.ReviewText10 = string.IsNullOrEmpty(ReviewText10) ? null : ReviewText10;
                updateActivityReview.ReviewText11 = string.IsNullOrEmpty(ReviewText11) ? null : ReviewText11;
                updateActivityReview.ActivityReviewTypeId = (int)MasterDataEnum.ActivityReviewType.ActualActivity;
                updateActivityReview.UpdatedBy = UserHelper.GetLoginUser();
                updateActivityReview.ActivityReviewId = ActivityReviewId;
                var response = await _caliphAPIHelper.PostAsync<UpdateActivityReviewRequest, ResponseData<string>>(updateActivityReview, "/api/v1/activity-review/update");
                return Json(response);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddLead(FormCollection formCollection)
        {
            var addActivity = FormCollectionMapper.FormToModel<AddActivityRequest>(formCollection);
            addActivity.CreatedBy = UserHelper.GetLoginUser();
            addActivity.ActivityPointId = 7;//ref leads
            addActivity.ActivityStartDate = DateTime.Now;

            var response = await _caliphAPIHelper.PostAsync<AddActivityRequest, ResponseData<string>>(addActivity, "/api/v1/client-deal-activity/add");

            var filterReq = new ActivityFilterRequest { CreatedBy = UserHelper.GetLoginUser() };
            var responseData = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(filterReq, "/api/v1/client-deal-activity/get-by-filter");
            var activity = responseData.Data.Where(x => x.ActivityPointId == 7).OrderByDescending(x => x.ClientDealActivityId).FirstOrDefault();

            var addLead = FormCollectionMapper.FormToModel<AddLeadRequest>(formCollection);
            addLead.CreatedBy = UserHelper.GetLoginUser();
            addLead.ClientDealActivityId = activity == null ? 0 : activity.ClientDealActivityId;
            var leadResponse = await _caliphAPIHelper.PostAsync<AddLeadRequest, ResponseData<string>>(addLead, "/api/v1/client-lead/add");
            return Json(leadResponse);
        }
        [HttpPost]
        public async Task<JsonResult> Update(FormCollection formCollection)
        {
            string eventId = formCollection["EventId"];
            string email = "";
            var AddToGoogle = formCollection["AddToGoogle"];

            if (AddToGoogle == "1")
            {
                string tokenPath = Server.MapPath("~/Cre/token.json");
                string crePath = Server.MapPath("~/Cre/Cre.json");

                if (!string.IsNullOrEmpty(eventId))
                {
                    var deleteGoogleResponse = await GoogleCalendarHelper.DeleteGoogleCalendar(eventId, tokenPath,crePath);
                }

                var clientID = int.Parse(formCollection["ClientId"]);

                //getClientNamebyClientID
                var filterClient = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy = UserHelper.GetDefaultSearchUser(), ClientId = clientID };
                var responseDataList = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filterClient, "/api/v1/client/get-by-client-filter");
                var responseData = responseDataList.Data.Select(x => x.Name).FirstOrDefault().ToString();

                //getDealNamebyDealID
                var ClientDealId = int.Parse(formCollection["ClientDealId"]);
                var filter = new DealFilterRequest { PageNumber = 1, PageSize = 9999, StatusId = (int)MasterDataEnum.Status.Active, ClientId = clientID };
                var responseDealList = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");
                var responseDeal = responseDealList.Data.Where(x => x.ClientDealId == ClientDealId).Select(x => x.Name).SingleOrDefault().ToString();

                //getActivityPointId
                var ActivityPointId = int.Parse(formCollection["ActivityPointId"]);
                var responseActList = await _masterService.GetSalesActivityPointAsync();
                var responseAct = responseActList.Where(x => x.ActivityPointId == ActivityPointId).Select(x => x.Name).SingleOrDefault().ToString();

                var addGoogleCalendar = new GoogleCalender()
                {
                    Summary = responseDeal + " - " + responseAct,
                    Description = responseAct + " for " + responseDeal + " , " + responseData,
                    Start = DateTime.Parse(formCollection["EditActivityStartDate"], null, System.Globalization.DateTimeStyles.RoundtripKind),
                    End = DateTime.Parse(formCollection["EditActivityEndDate"], null, System.Globalization.DateTimeStyles.RoundtripKind)
                };
                var googleResponse = await GoogleCalendarHelper.CreateGoogleCalendar(addGoogleCalendar,tokenPath,crePath);
                if (googleResponse.Status == "confirmed")
                {
                    eventId = googleResponse.Id;
                    email = googleResponse.Organizer.Email;
                }
            }
            var updateReq = FormCollectionMapper.FormToModel<UpdateActivityRequest>(formCollection);
            updateReq.UpdatedBy = UserHelper.GetLoginUser();
            updateReq.EventId = ((!string.IsNullOrEmpty(eventId) && AddToGoogle == "1") ? eventId : null);
            updateReq.Email = (!string.IsNullOrEmpty(email) ? email : null);
            updateReq.GoogleLinked = (AddToGoogle == "1" ? true : false);
            var response = await _caliphAPIHelper.PostAsync<UpdateActivityRequest, ResponseData<string>>(updateReq, "/api/v1/client-deal-activity/update");
            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateUserActivity(FormCollection formCollection)
        {
            var updateReq = FormCollectionMapper.FormToModel<EditUserActivityRequest>(formCollection);
            updateReq.UpdatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<EditUserActivityRequest, ResponseData<string>>(updateReq, "/api/v1/user-activity/update");
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> ViewLeads(int id)
        {

            var data = new EditLeadsViewModel();
            var activityRequest = new GetActivityRequest { ClientDealActivityId = id };
            var activitiResponse = await _caliphAPIHelper.PostAsync<GetActivityRequest, ResponseData<DealActivity>>(activityRequest, "/api/v1/client-deal-activity/get-by-deal-activity-id");
            data.ClientDealId = activitiResponse.Data.ClientDealId;


            var dealReq = new DealFilterRequest { PageNumber = 1, PageSize = 9999, StatusId = (int)MasterDataEnum.Status.Active, CreatedBy = UserHelper.GetLoginUser() };
            var dealRes = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(dealReq, "/api/v1/client-deal/get-by-filter");
            data.Deals = dealRes.Data;

            var filter = new GetLeadRequest { PageNumber = 1, PageSize = 5, ClientDealActivityId = id };
            var responseData = await _caliphAPIHelper.PostAsync<GetLeadRequest, ResponseData<List<ClientLead>>>(filter, "/api/v1/client-lead/get-by-filter");
            data.ClientLead = responseData.Data.FirstOrDefault();


            return PartialView("_EditRefLeads", data);
        }


        //submit client-deal-activity/add
        [HttpPost]
        public async Task<JsonResult> UpdateLead(FormCollection formCollection)
        {
            var request = FormCollectionMapper.FormToModel<UpdateLeadRequest>(formCollection);
            request.UpdatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<UpdateLeadRequest, ResponseData<string>>(request, "/api/v1/client-lead/update");
            return Json(response);
        }

        /*****-------------------------------------------------------------------------------------------------------------------------------------------------------------------------******/

        // GET: Activity List
        public async Task<ActionResult> List()
        {
            var dealfilter = new DealFilterRequest { PageNumber = 1, PageSize = 9999,  CreatedBy = UserHelper.GetDefaultSearchUser() };
            var dealresponseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(dealfilter, "/api/v1/client-deal/get-by-filter");
            var vm = new ActivityListViewModel();
            vm.Deals = dealresponseData.Data;
            vm.Activities = await _masterService.GetSalesActivityPointAsync();

            var filter = new ActivityFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var responseData = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(filter, "/api/v1/client-deal-activity/get-by-filter");
 

            vm.ActivityData.Activities = responseData.Data;
            vm.ActivityData.Paging.ItemCount = responseData.ItemCount;
            vm.ActivityData.Paging.PageSize = filter.PageSize;
            vm.ActivityData.Paging.CurrentPage = filter.PageNumber;
         
            return View(vm);
        }


      
        [HttpPost]
        public async Task<ActionResult> List(ActivityFilterRequest filter)
        {
            var size = filter.PageSize;
            filter.CreatedBy = UserHelper.GetDefaultSearchUser();
            if (filter.ActivityPointId.HasValue)
            {
                filter.PageSize = 999;
            }
                var responseData = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(filter, "/api/v1/client-deal-activity/get-by-filter");

            var data = new ActivityData();
            data.Activities = responseData.Data;
            data.Paging.ItemCount = responseData.ItemCount;
            data.Paging.PageSize = filter.PageSize;
            data.Paging.CurrentPage = filter.PageNumber;

            if (filter.ActivityPointId.HasValue)
            {
                var filterActivity = responseData.Data.Where(x => x.ActivityPointId == filter.ActivityPointId.Value).ToList();
                if (filterActivity != null && filterActivity.Count > 0)
                {
                    data.Activities = filterActivity.Take(size).ToList() ;
                    responseData.ItemCount = filterActivity.Count;
                }
            }
          
         
            return PartialView("_ActivityListTable", data);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateStatus(int status, int id)
        {

            var postUrl = "";
            if (status == (int)MasterDataEnum.Status.Done)
                postUrl = "/api/v1/client-deal-activity/update-done";
            else if (status == (int)MasterDataEnum.Status.Missed)
                postUrl = "/api/v1/client-deal-activity/update-missed";
            else
                return Json(new ResponseData<string> { StatusMsg = "Invalid Status" });


            var updateReq = new UpdateActivitySatusRequest {ClientDealActivityId = id , UpdatedBy = UserHelper.GetLoginUser() };
            var response = await _caliphAPIHelper.PostAsync<UpdateActivitySatusRequest, ResponseData<string>>(updateReq, postUrl);
            return Json(response);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var updateReq = new UpdateActivitySatusRequest { ClientDealActivityId = id, UpdatedBy = UserHelper.GetLoginUser() };
            var response = await _caliphAPIHelper.PostAsync<UpdateActivitySatusRequest, ResponseData<string>>(updateReq, "/api/v1/client-deal-activity/delete");
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> View(int id)
        {
            var updateReq = new GetActivityRequest { ClientDealActivityId = id };
            var response = await _caliphAPIHelper.PostAsync<GetActivityRequest, ResponseData<DealActivity>>(updateReq, "/api/v1/client-deal-activity/get-by-deal-activity-id");
            var editActivityvm = AutoMapHelper.Map<DealActivity, EditActivityViewModel>(response.Data);


            var filter = new DealFilterRequest { PageNumber = 1, PageSize = 9999,  CreatedBy = UserHelper.GetDefaultSearchUser() };
            var dealresponseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");
            editActivityvm.Deals = dealresponseData.Data;
            editActivityvm.Activities = await _masterService.GetSalesActivityPointAsync();

            return PartialView("_EditActivity", editActivityvm);
        }


        [HttpPost]
        public async Task<ActionResult> ViewUserActivity(int id)
        {
            var updateReq = new UserActivityFilterRequest {PageSize=999, PageNumber=1, UserActivityId = id };
            var response = await _caliphAPIHelper.PostAsync<UserActivityFilterRequest, ResponseData<List<UserActivity>>>(updateReq, "/api/v1/user-activity/get-by-filter");
            var data = response.Data==null? new UserActivity() : response.Data.FirstOrDefault();
            var dealActivity = AutoMapHelper.Map< UserActivity, DealActivity>(data);
            dealActivity.ClientDealActivityId = data.UserActivityId;
                 ;
            var editActivityvm = AutoMapHelper.Map<DealActivity, EditActivityViewModel>(dealActivity);


            var filter = new DealFilterRequest { PageNumber = 1, PageSize = 9999, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var dealresponseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");
            editActivityvm.Deals = dealresponseData.Data;
            editActivityvm.Activities = await _masterService.GetSalesActivityPointAsync();
            editActivityvm.UserActivityId = data.UserActivityId;
            editActivityvm.UserId = data.UserId;
            return PartialView("_EditUserActivity", editActivityvm);
        }



        public ActionResult Edit(int id)
        {
            return View();
        }


        public async Task<ActionResult> Calender()
        {
            var addActivityVM = new AddActivityViewModel();
            var filter = new DealFilterRequest { PageNumber = 1, PageSize = 9999, StatusId = (int)MasterDataEnum.Status.Active, CreatedBy = UserHelper.GetLoginUser() };
            var responseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");


            var filterClient = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var clientData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filterClient, "/api/v1/client/get-by-client-filter");

            addActivityVM.Clients = clientData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();
            addActivityVM.Deals = responseData.Data;
            addActivityVM.Activities = await _masterService.GetSalesActivityPointAsync();
            addActivityVM.AgentUsers = await _userService.GetAgentUserAsync();

            #region getActivityReviewData
            //get activity review data
            DateTime today = DateTime.Now.Date;
            int dayoftheweek = (int)today.DayOfWeek;
            int month = today.Month;
            if (today.AddDays(-dayoftheweek + 1).Day >= today.Day)
            {
                month = today.AddMonths(-1).Month;   
            }
            DateTime sDate = new DateTime(today.Year, month, today.AddDays(-dayoftheweek + 1).Day);
            DateTime endDate = sDate.AddDays(6);
            string sDateString = sDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");
            var activityReviewFilter = new ActivityReviewFilterRequest { DateInWeekFrom = sDateString, DateInWeekTo = endDateString, UserId = UserHelper.GetLoginUserViewModel().UserId };
            var activityReviewResponseData = await _caliphAPIHelper.PostAsync<ActivityReviewFilterRequest, ResponseData<List<ActivityReview>>>(activityReviewFilter, "/api/v1/activity-review/get-by-filter");
            activityReviewResponseData.Data = activityReviewResponseData.Data == null ? new List<ActivityReview>() : activityReviewResponseData.Data;
            var activitiesReview = activityReviewResponseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();
            var Planned = activitiesReview.Where(x => x.ActivityReviewTypeId == (int)MasterDataEnum.ActivityReviewType.PlannedActivity).ToList().FirstOrDefault();
            var Actual = activitiesReview.Where(x => x.ActivityReviewTypeId == (int)MasterDataEnum.ActivityReviewType.ActualActivity).ToList().FirstOrDefault();
            var Empty = new ActivityReview
            {
                ActivityReviewId = -1,
                ReviewText1 = "",
                ReviewText2 = "",
                ReviewText3 = "",
                ReviewText4 = "",
                ReviewText5 = "",
                ReviewText6 = "",
                ReviewText7 = "",
                ReviewText8 = "",
                ReviewText9 = "",
                ReviewText10 = "",
                ReviewText11 = ""
        };
            
            var ActualPlannedActivitiesReview = new List<ActivityReview>();
            if (Planned != null)
            {
                ActualPlannedActivitiesReview.Add(Planned);
            }
            else
            {
                ActualPlannedActivitiesReview.Add(Empty);
            }
            if (Actual != null)
            {
                ActualPlannedActivitiesReview.Add(Actual);
            }
            else
            {
                ActualPlannedActivitiesReview.Add(Empty);
            }

            addActivityVM.ActivityReviews = ActualPlannedActivitiesReview;
            #endregion
            return View(addActivityVM);
        }

        public async Task<JsonResult> CalendarEvents(CalendarEvent calendarEvent)
        {

            var filter = new ActivityFilterRequest { ActivityStartDate = calendarEvent.start, ActivityEndDate = calendarEvent.end, CreatedBy = calendarEvent.CreatedBy };
            var responseData = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(filter, "/api/v1/client-deal-activity/get-by-filter");

            responseData.Data = responseData.Data == null ? new List<DealActivity>() : responseData.Data;
            var activities = responseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList(); ;
            var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<DealActivity, JsonEvents>()
                  .ForMember(dest => dest.title, act => act.MapFrom(src => src.ClientName +"-"+src.ActivityPointsDesc))
                  .ForMember(dest => dest.color, act => act.MapFrom(src => src.ColorCode))
                  .ForMember(dest => dest.start, act => act.MapFrom(src => src.ActivityStartDate.HasValue ? src.ActivityStartDate.Value.ToString("yyyy-MM-dd HH:mm") : ""))
                  .ForMember(dest => dest.end, act => act.MapFrom(src => src.ActivityStartDate.HasValue ? src.ActivityEndDate.Value.ToString("yyyy-MM-dd HH:mm") : ""))
                  .ForMember(dest => dest.dealId, act => act.MapFrom(src => src.ClientDealActivityId))
                    .ForMember(dest => dest.type, act => act.MapFrom(src => "client"))
              //opt => opt.MapFrom(src => "THIS STRING")
              );
            IMapper iMapper = config.CreateMapper();

            var jsonEvents = iMapper.Map<List<DealActivity>, List<JsonEvents>>(activities);


            if (UserHelper.GetLoginUserViewModel().IsLeader)
            {
                var agentRecruitActivityFilter = new FilterAgentRecruitActivityRequest { ActivityStartDate = calendarEvent.start, ActivityEndDate = calendarEvent.end, CreatedBy = calendarEvent.CreatedBy , PageSize=9999, PageNumber=1};
                var agentActivityResponse = await _caliphAPIHelper.PostAsync<FilterAgentRecruitActivityRequest, ResponseData<List<AgentRecruimentActivity>>>(agentRecruitActivityFilter, "/api/v1/agent-activity/get-by-filter");

                agentActivityResponse.Data = agentActivityResponse.Data ?? new List<AgentRecruimentActivity>() ;
          
           var      agentActivityconfig = new MapperConfiguration(cfg =>
                      cfg.CreateMap<AgentRecruimentActivity, JsonEvents>()
                      .ForMember(dest => dest.title, act => act.MapFrom(src => src.AgentName + "-" + src.ActivityPointsDesc))
                      .ForMember(dest => dest.color, act => act.MapFrom(src => src.ColorCode))
                      .ForMember(dest => dest.start, act => act.MapFrom(src => src.ActivityStartDate.HasValue ? src.ActivityStartDate.Value.ToString("yyyy-MM-dd HH:mm") : ""))
                      .ForMember(dest => dest.end, act => act.MapFrom(src => src.ActivityStartDate.HasValue ? src.ActivityEndDate.Value.ToString("yyyy-MM-dd HH:mm") : ""))
                      .ForMember(dest => dest.dealId, act => act.MapFrom(src => src.AgentActivityId))
                      .ForMember(dest => dest.type, act =>   act.MapFrom(src => "recruitment"))
                  );
                iMapper = agentActivityconfig.CreateMapper();
                var agentRecruitmentData = iMapper.Map<List<AgentRecruimentActivity>, List<JsonEvents>>(agentActivityResponse.Data);
                jsonEvents.AddRange(agentRecruitmentData);
            }

            return Json(jsonEvents);
        }
        public async Task<JsonResult> PlannedCalendarEvents(CalendarEvent calendarEvent)
        {

            var filter = new UserActivityFilterRequest { PageNumber = 1, PageSize = 9999, ActivityStartDate = calendarEvent.start, ActivityEndDate = calendarEvent.end, UserId = int.Parse(calendarEvent.CreatedBy) };
            var responseData = await _caliphAPIHelper.PostAsync<UserActivityFilterRequest, ResponseData<List<UserActivity>>>(filter, "/api/v1/user-activity/get-by-filter");

            responseData.Data = responseData.Data == null ? new List<UserActivity>() : responseData.Data;
            var activities = responseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList(); ;
            var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<UserActivity, JsonEvents>()
                  .ForMember(dest => dest.title, act => act.MapFrom(src => src.ActivityPointsDesc))
                  .ForMember(dest => dest.color, act => act.MapFrom(src => src.ColorCode))
                  .ForMember(dest => dest.start, act => act.MapFrom(src => src.ActivityStartDate.HasValue ? src.ActivityStartDate.Value.ToString("yyyy-MM-dd HH:mm") : ""))
                  .ForMember(dest => dest.end, act => act.MapFrom(src => src.ActivityStartDate.HasValue ? src.ActivityEndDate.Value.ToString("yyyy-MM-dd HH:mm") : ""))
                  .ForMember(dest => dest.dealId, act => act.MapFrom(src => src.UserActivityId))
              );
            IMapper iMapper = config.CreateMapper();
            var jsonEvents = iMapper.Map<List<UserActivity>, List<JsonEvents>>(activities);
            return Json(jsonEvents);
        }

        public async Task<JsonResult> PlannedCalendarReviewEvents(int CreatedBy, string ssdate, string ssendDate)
        {
            var activityReviewFilter = new ActivityReviewFilterRequest { DateInWeekFrom = ssdate, DateInWeekTo = ssendDate, UserId = CreatedBy, ActivityReviewTypeId = (int)MasterDataEnum.ActivityReviewType.PlannedActivity };
            var activityReviewResponseData = await _caliphAPIHelper.PostAsync<ActivityReviewFilterRequest, ResponseData<List<ActivityReview>>>(activityReviewFilter, "/api/v1/activity-review/get-by-filter");
            activityReviewResponseData.Data = activityReviewResponseData.Data == null ? new List<ActivityReview>() : activityReviewResponseData.Data;
            var activitiesReview = activityReviewResponseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();

            var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<ActivityReview, JsonActivitiesReviewEvents>()
              );
            IMapper iMapper = config.CreateMapper();
            var jsonEvents = iMapper.Map<List<ActivityReview>, List<JsonActivitiesReviewEvents>>(activitiesReview);
            return Json(jsonEvents);
        }

        public async Task<JsonResult> ActualCalendarReviewEvents(int CreatedBy, string ssdate, string ssendDate)
        {
            var activityReviewFilter = new ActivityReviewFilterRequest { DateInWeekFrom = ssdate, DateInWeekTo = ssendDate, UserId = CreatedBy, ActivityReviewTypeId = (int)MasterDataEnum.ActivityReviewType.ActualActivity };
            var activityReviewResponseData = await _caliphAPIHelper.PostAsync<ActivityReviewFilterRequest, ResponseData<List<ActivityReview>>>(activityReviewFilter, "/api/v1/activity-review/get-by-filter");
            activityReviewResponseData.Data = activityReviewResponseData.Data == null ? new List<ActivityReview>() : activityReviewResponseData.Data;
            var activitiesReview = activityReviewResponseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();

            var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<ActivityReview, JsonActivitiesReviewEvents>()
              );
            IMapper iMapper = config.CreateMapper();
            var jsonEvents = iMapper.Map<List<ActivityReview>, List<JsonActivitiesReviewEvents>>(activitiesReview);
            return Json(jsonEvents);
        }


        public async Task<string> GetDealList(int clientid)
        {
            var filter = new DealFilterRequest { PageNumber = 1, PageSize = 9999, StatusId = (int)MasterDataEnum.Status.Active, ClientId = clientid };
            var responseDeal = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");
            return JsonConvert.SerializeObject(responseDeal.Data);
        }


    }
}