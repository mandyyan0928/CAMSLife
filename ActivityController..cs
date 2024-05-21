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
		
		 public ActionResult List()
        {
            return View();
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
		
		
	}
}