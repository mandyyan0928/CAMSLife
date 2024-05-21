using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Announcement.Request;
using CaliphWeb.Models.API.Announcement.Response;
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
    public class AnnouncementController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public AnnouncementController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }

        // GET: Announcement
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add()
        {
            var view = new AddAnnouncementViewModel();
            view.AnnouncementTypeList = await _masterService.GetAnnouncementTypeAsync();
            var req = new GetAgentRequest { RoleId = (int)MasterDataEnum.RoleId.Agent };
            var response = await _caliphAPIHelper.PostAsync<GetAgentRequest, ResponseData<List<AgentUser>>>(req, "/api/v1/agent/get-by-filter");
            view.AgentUsers = response.Data;


            req = new GetAgentRequest { RoleId = (int)MasterDataEnum.RoleId.Leader };
              response = await _caliphAPIHelper.PostAsync<GetAgentRequest, ResponseData<List<AgentUser>>>(req, "/api/v1/agent/get-by-filter");
            if (response != null && response.Data != null)
                view.AgentUsers.AddRange(response.Data);
            return View(view);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Add(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<AnnouncementAdd>(formCollection);
            model.CreatedBy = UserHelper.GetLoginUser();


            if (model.AnnouncementTypeId == (int)MasterDataEnum.AnnouncementType.Specific)
            {
                var userId = ConvertHelper.ConvertInt( formCollection["UserIdList"]);
                model.UserIdList = new List<int>();
                model.UserIdList.Add(userId);
            }

            var response = await _caliphAPIHelper.PostAsync<AnnouncementAdd, ResponseData<string>>(model, "/api/v1/announcement/add");

            return Json(response);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var filter = new AnnouncementGet
            {
                PageSize = 10,
                PageNumber = 1,
            };
            var vm = new EditAnnouncementViewModel();

            var response = await _caliphAPIHelper.PostAsync<AnnouncementGet, ResponseData<List<Announcement>>>(filter, "/api/v1/announcement/get-by-filter");
            vm.Announcement = response.Data.FirstOrDefault(x => x.AnnouncementId == id);



            var req = new GetAgentRequest { RoleId = (int)MasterDataEnum.RoleId.Agent };
            var agentresponse = await _caliphAPIHelper.PostAsync<GetAgentRequest, ResponseData<List<AgentUser>>>(req, "/api/v1/agent/get-by-filter");
            vm.AgentUsers = agentresponse.Data;
            vm.AnnouncementTypeList = await _masterService.GetAnnouncementTypeAsync();
            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        
        public async Task<JsonResult> Edit(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<AnnouncementEdit>(formCollection);
            model.UpdatedBy = UserHelper.GetLoginUser();

            var userid = new List<int>();
            if (model.AnnouncementTypeId == (int)MasterDataEnum.AnnouncementType.Specific)
            {
                model.UserIdList = new List<int>();
                model.UserIdList.Add(ConvertHelper.ConvertInt(formCollection["UserId"]));
            }
            var response = await _caliphAPIHelper.PostAsync<AnnouncementEdit, ResponseData<string>>(model, "/api/v1/announcement/update");

            return Json(response);
        }
        
        [HttpPost]
        public async Task<JsonResult> Delete(int id )
        {
            var model = new AnnouncementEdit { AnnouncementId = id };
            model.UpdatedBy = UserHelper.GetLoginUser();

            var response = await _caliphAPIHelper.PostAsync<AnnouncementEdit, ResponseData<string>>(model, "/api/v1/announcement/delete");

            return Json(response);
        }
        
        [HttpPost]
        public async Task<ActionResult> Get(int id)
        {
            var filter = new AnnouncementGet
            {
                AnnouncementId=id,
                PageSize = 10,
                PageNumber = 1,
            };
            var vm = new EditAnnouncementViewModel();

            var response = await _caliphAPIHelper.PostAsync<AnnouncementGet, ResponseData<List<Announcement>>>(filter, "/api/v1/announcement/get-by-filter");
            vm.Announcement = response.Data.FirstOrDefault(x => x.AnnouncementId == id);

            return Json(vm);
        }
        
        public async Task<ActionResult> List()
        {
            var filter = new AnnouncementGet
            {
                PageSize = 10,
                PageNumber = 1,
            };

            var response = await _caliphAPIHelper.PostAsync<AnnouncementGet, ResponseData<List<Announcement>>>(filter, "/api/v1/announcement/get-by-filter");

            return View(response.Data);
        }
    }
}