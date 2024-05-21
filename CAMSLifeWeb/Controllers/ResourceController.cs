using CaliphWeb.Services.Helper;
using CaliphWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaliphWeb.Core;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.ViewModel.Data;
using System.Threading.Tasks;
using CaliphWeb.Models.API.Announcement.Request;
using CaliphWeb.Models.API.Announcement.Response;
using CaliphWeb.Models.API.Resource.Response;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Helper;
using CaliphWeb.Models.API.Resource;
using DocumentFormat.OpenXml.Bibliography;

namespace CaliphWeb.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public ResourceController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }

        // GET: Resource
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> List()
        {
            var filter = new ResourceGet
            {
                PageSize = 10,
                PageNumber = 1,
            };

            var response = await _caliphAPIHelper.PostAsync<ResourceGet, ResponseData<List<Resource>>>(filter, "/api/v1/resource/get-by-filter");

            return View(response.Data);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Add(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<ResourceAdd>(formCollection);
            model.CreatedBy = UserHelper.GetLoginUser();

            var response = await _caliphAPIHelper.PostAsync<ResourceAdd, ResponseData<string>>(model, "/api/v1/resource/add");

            return Json(response);
        }


        public async Task<ActionResult> Edit(int id)
        {
            var filter = new ResourceGet
            {
                PageSize = 10,
                PageNumber = 1,
                ResourceId = id
            };

            var response = await _caliphAPIHelper.PostAsync<ResourceGet, ResponseData<List<Resource>>>(filter, "/api/v1/resource/get-by-filter");

            return View(response.Data.First());
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Edit(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<ResourceEdit>(formCollection);
            model.UpdatedBy = UserHelper.GetLoginUser();

            var response = await _caliphAPIHelper.PostAsync<ResourceEdit, ResponseData<string>>(model, "/api/v1/resource/update");

            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var model = new ResourceEdit { ResourceId = id };
            model.UpdatedBy = UserHelper.GetLoginUser();

            var response = await _caliphAPIHelper.PostAsync<ResourceEdit, ResponseData<string>>(model, "/api/v1/resource/delete");

            return Json(response);
        }

    }
}
