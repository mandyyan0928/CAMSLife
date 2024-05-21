using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Deal;
using CaliphWeb.Models.API.Deal.Request;
using CaliphWeb.Models.Data.Client.Request;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    [Authorize]
    public class DealController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public DealController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }
        // GET: Deal
        public async Task<ActionResult> List(int? clientId)
        {

            var vm = new DealListViewModel();
            vm.Title = await _masterService.GetMasterDatasAsync(MasterDataEnum.MasterData.DealTitle);
            vm.Status = await _masterService.GetDealStatusAsync();


            var filter = new DealFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetLoginUser() };
            if (clientId.HasValue)
            {
                filter.ClientId = clientId.Value;
                var clientReq = new GetClientRequest { ClientId=clientId.Value };
                var clientRes = await _caliphAPIHelper.PostAsync<GetClientRequest, ResponseData<Client>>(clientReq, "/api/v1/client/get-by-client-id");
                if (clientRes.Data != null)
                    vm.ClientName = clientRes.Data.Name;
                    
            }
            var responseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");

            vm.DealData.Deals = responseData.Data.Where(x=>x.StatusId!= (int)MasterDataEnum.Status.Inactive).ToList();
            vm.DealData.Paging.ItemCount = responseData.ItemCount;
            vm.DealData.Paging.PageSize = filter.PageSize;
            vm.DealData.Paging.CurrentPage = filter.PageNumber;

            foreach (var deal in vm.DealData.Deals)
            {
                var activityFilter = new ActivityFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetDefaultSearchUser(), ClientDealId = deal.ClientDealId };
                var activityResponse = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(activityFilter, "/api/v1/client-deal-activity/get-by-filter");
                deal.Activities = activityResponse.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();
                if (deal.Activities.Count() > 0)
                {
                    var nextActvity = deal.Activities.OrderByDescending(p => p.ActivityStartDate).First();
                    deal.NextActivityDate = nextActvity.ActivityStartDate;
                    deal.NextActivityDesc = nextActvity.ActivityPointsDesc;
                }
            }
            var addActivityVM = new AddActivityViewModel();
            var dealFiler = new DealFilterRequest { PageNumber = 1, PageSize = 9999, StatusId = (int)MasterDataEnum.Status.Active, CreatedBy =UserHelper.GetLoginUser() };
            var dealDdata = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(dealFiler, "/api/v1/client-deal/get-by-filter");

            var filterClient = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var clientData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filterClient, "/api/v1/client/get-by-client-filter");
            addActivityVM.Clients = clientData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();

           
            addActivityVM.Deals = dealDdata.Data;
            addActivityVM.Activities = await _masterService.GetSalesActivityPointAsync();
            vm.AddActivityViewModel = addActivityVM;
            vm.Clients = addActivityVM.Clients;
                vm.Users = await _userService.GetAgentUserAsync();
            return View(vm);
        }



        [HttpPost]
        public async Task<ActionResult> List(DealFilterRequest filter)
        {
            var dealData = new DealData();
            var responseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");

            dealData.Deals = responseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList(); ;
            
            dealData.Paging.ItemCount = responseData.ItemCount;
            dealData.Paging.PageSize = filter.PageSize;
            dealData.Paging.CurrentPage = filter.PageNumber;
            foreach (var deal in dealData.Deals)
            {
                var activityFilter = new ActivityFilterRequest { PageNumber = 1, PageSize = 10,  ClientDealId= deal.ClientDealId };
                var activityResponse = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(activityFilter, "/api/v1/client-deal-activity/get-by-filter");
                deal.Activities = activityResponse.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList(); ;
                var nextActvity = deal.Activities.FirstOrDefault();
                if (nextActvity != null)
                {
                    deal.NextActivityDate = nextActvity.ActivityStartDate;
                    deal.NextActivityDesc = nextActvity.ActivityPointsDesc;
                }
            }
            return PartialView("_DealListTable", dealData);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateStatus(int status, int id)
        {

            var postUrl = "";
            if (status == (int)MasterDataEnum.Status.Closed)
                postUrl = "/api/v1/client-deal/update-closed";
            else if (status == (int)MasterDataEnum.Status.Lost)
                postUrl = "/api/v1/client-deal/update-lost";
            else
                return Json(new ResponseData<string> { StatusMsg="Invalid Status" });


            var updateReq = new UpdateDealRequest { ClientDealId=id };
            updateReq.UpdatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<UpdateDealRequest, ResponseData<string>>(updateReq, postUrl);
            return Json(response);
        }

        public async Task<ActionResult> Add()
        {
            var addDealVM = new AddDealViewModel();
            var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy=UserHelper.GetDefaultSearchUser() };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");
            addDealVM.Clients = responseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();
            addDealVM.Titles = await _masterService.GetMasterDatasAsync(MasterDataEnum.MasterData.DealTitle);

            return View(addDealVM);
        }
        [HttpPost]
        public async Task<JsonResult> Add(FormCollection formCollection)
        {
            var addClient = FormCollectionMapper.FormToModel<AddDealRequest>(formCollection);
            addClient.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<AddDealRequest, ResponseData<string>>(addClient, "/api/v1/client-deal/add");
            return Json(response);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var editDealVM = new EditDealViewModel();
            var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");
            editDealVM.Clients = responseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();
            editDealVM.Titles = await _masterService.GetMasterDatasAsync(MasterDataEnum.MasterData.DealTitle);

            var dealReq = new GetDealRequest { ClientDealId=id };
                var dealRes= await _caliphAPIHelper.PostAsync<GetDealRequest, ResponseData<Deal>>(dealReq, "/api/v1/client-deal/get-by-deal-id");

            editDealVM.Deal = dealRes.Data;
            editDealVM.AgentUsers = await _userService.GetAgentUserAsync();
            return View(editDealVM);
        }


        [HttpPost]
        public async Task<JsonResult> Edit(FormCollection formCollection)
        {
            var updateDealReq = FormCollectionMapper.FormToModel<EditDealRequest>(formCollection);
            updateDealReq.UpdatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<EditDealRequest, ResponseData<string>>(updateDealReq, "/api/v1/client-deal/update");
            return Json(response);
        }

        public ActionResult Referral()
        {
            return View();
        }
    }
}