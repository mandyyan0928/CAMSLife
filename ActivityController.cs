using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        public DealController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
        }
		
		
//get client-deal/get-by-filter
public async Task<ActionResult> Add()
        {
            var addActivityVM = new AddActivityViewModel();
            var filter = new AddDealRequest { Name };
            var responseData = await _caliphAPIHelper.PostAsync<AddDealRequest, ResponseData<List<Name>>>(filter, "api/v1/client-deal/get-by-filter");
            addActivityVM.Deal = AutoMapHelper.MapList<Deal, ClientModel>(responseData.Data);

            return View(addActivityVM);
        }
		
		
		
		//get master/get-activity 
		public async Task<ActionResult> Add()
        {
            var view = new AddActivityViewModel();
            var filter = new MasterDataRequest { MasterId };
            var responseData = await _caliphAPIHelper.PostAsync<MasterDataRequest, ResponseData<List<Name>>>(filter, "/api/v1/master/get-activity");
            view.Activity = await _masterService.GetMasterDatasAsync(MasterDataEnum.MasterData.Activity);

            return View(view);
			
      
        }
		
		//submit client-deal-activity/add
		[HttpPost]
        public async Task<JsonResult> Add(FormCollection formCollection)
        {
            var addClient = FormCollectionMapper.FormToModel<AddDealRequest>(formCollection);
            addActivity.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<AddDealRequest, ResponseData<string>>(addClient, "/api/v1/client-deal/add");
            return Json(response);
        }
		
		/*****-------------------------------------------------------------------------------------------------------------------------------------------------------------------------******/
		
		// GET: Activity List
        public async Task<ActionResult> List(int? dealTitleId)
        {

            var vm = new ActivityListViewModel();
            vm.Deal = await _masterService.GetMasterDatasAsync(MasterDataEnum.MasterData.DealTitle);
            vm.Activity = await _masterService.GetMasterDatasAsync(MasterDataEnum.MasterData.Activity);

		//GetClientRequest	
           var filter = new DealFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetDefaultSearchUser() };
            if (clientId.HasValue)
            {
                filter.ClientId = clientId.Value;
                var clientReq = new GetClientRequest { ClientId=clientId.Value };
                var clientRes = await _caliphAPIHelper.PostAsync<GetClientRequest, ResponseData<Client>>(clientReq, "/api/v1/client/get-by-client-id");
                if (clientRes.Data != null)
                    vm.ClientName = clientRes.Data.Name;
                    
            }
			
		//DealFilterRequest	Deal Title Master Data
            var responseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");

            vm.DealData.Deals = responseData.Data.Where(x=>x.DealTitleId!= (int)MasterDataEnum.DealTitle.Inactive).ToList();
            vm.DealData.Paging.ItemCount = responseData.ItemCount;
            vm.DealData.Paging.PageSize = filter.PageSize;
            vm.DealData.Paging.CurrentPage = filter.PageNumber;

		//ActivityFilterRequest Activity Master Data
            foreach (var deal in vm.DealData.Deals)
            {
                var activityFilter = new ActivityFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetDefaultSearchUser() };
                var activityResponse = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<Activity>>>(activityFilter, "/api/v1/client-deal-activity/get-by-filter");
                deal.Activities = activityResponse.Data.Where(x => x.DealTitleId != (int)MasterDataEnum.Activity.Inactive).ToList();
            }
            return View(vm);
        }


		//DealFilterRequest	
        [HttpPost]
        public async Task<ActionResult> List(DealFilterRequest filter)
        {
            var dealData = new DealData();
            var responseData = await _caliphAPIHelper.PostAsync<DealFilterRequest, ResponseData<List<Deal>>>(filter, "/api/v1/client-deal/get-by-filter");

            dealData.Deals = responseData.Data.Where(x => x.DealTitleId != (int)MasterDataEnum.DealTitle.Inactive).ToList(); ;
            dealData.Paging.ItemCount = responseData.ItemCount;
            dealData.Paging.PageSize = filter.PageSize;
            dealData.Paging.CurrentPage = filter.PageNumber;
            foreach (var deal in dealData.Deals)
            {
                var activityFilter = new ActivityFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetDefaultSearchUser() };
                var activityResponse = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<Activity>>>(activityFilter, "/api/v1/client-deal-activity/get-by-filter");
                deal.Activities = activityResponse.Data.Where(x => x.DealTitleId != (int)MasterDataEnum.DealTitle.Inactive).ToList(); ;
            }
            return PartialView("_DealListTable", dealData);
        }
		
		
		
		
	}
	
}







