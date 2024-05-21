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
using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{

    [Authorize]
    public class AccessRightController : Controller
    {

        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public AccessRightController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }


        public async Task<ActionResult> AddDepartment()
        {
            var responseData = await _caliphAPIHelper.PostAsync<ResponseData<AddDepartmentViewModel>>("/api/v1/role-menu/get-all-menu");


            return View(responseData.Data);
        }

        public async Task<ActionResult> AddStaff()
        
        {
            var addStaffViewModel = new AddStaffViewModel2();

            var filterDepartment = new DepartmentRequest { PageNumber = 1, PageSize = 999 };
            var responseData = await _caliphAPIHelper.PostAsync<DepartmentRequest,ResponseData<List< AddStaffViewModel>>>(filterDepartment,"/api/v1/role-menu/get-by-filter");
            addStaffViewModel.Data = responseData.Data;

            return View(addStaffViewModel);
        }


        
        [HttpPost]
        public async Task<JsonResult> AddDepartment(FormCollection formCollection)
        {
            var addDepartment = FormCollectionMapper.FormToModel<AddDepartmentRequest>(formCollection);

            addDepartment.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<AddDepartmentRequest, ResponseData<string>>(addDepartment, "/api/v1/role-menu/add");
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddStaff(FormCollection formCollection)
        {
            var addStaff = FormCollectionMapper.FormToModel<AddStaffRequest>(formCollection);

            addStaff.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<AddStaffRequest, ResponseData<string>>(addStaff, "/api/v1/system-user/add");
            return Json(response);
        }

    }
}