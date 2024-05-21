using Caliph.Library.Helper;
using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Models.API;
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
using System.Web.Security;

namespace CaliphWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;

        public UserController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
        }

        public async Task<ActionResult> List()
        {
            var userListViewModel = new UserListViewModel
            {
            };

            var userFilterViewModel = new UserFilterViewModel
            {
                PageNumber = 1,
                PageSize = 10
            };

            var responseData = await _caliphAPIHelper.PostAsync<UserFilterViewModel, ResponseData<List<UserListResponse>>>(userFilterViewModel, "/api/v1/system-user/get-by-filter");
            userListViewModel.UserList.UserList = responseData.Data;
            userListViewModel.UserList.Paging.ItemCount = responseData.ItemCount;
            userListViewModel.UserList.Paging.PageSize = userFilterViewModel.PageSize;
            userListViewModel.UserList.Paging.CurrentPage = userFilterViewModel.PageNumber;

            return View(userListViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(UserFilterViewModel userFilterViewModel)
        {
            var responseData = await _caliphAPIHelper.PostAsync<UserFilterViewModel, ResponseData<List<UserListResponse>>>(userFilterViewModel, "/api/v1/system-user/get-by-filter");
            var userListDataViewModel = new UserListDataViewModel();
            userListDataViewModel.UserList = responseData.Data;
            userListDataViewModel.Paging.ItemCount = responseData.ItemCount;
            userListDataViewModel.Paging.PageSize = userFilterViewModel.PageSize;
            userListDataViewModel.Paging.CurrentPage = userFilterViewModel.PageNumber;
            return PartialView("_UserListTable", userListDataViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatus(string username, int status)
        {
            UserRequest userRequest = new UserRequest()
            {
                Username = username,
                StatusId = status,
                UpdatedBy = UserHelper.GetLoginUser()
            };

            var response = await _caliphAPIHelper.PostAsync<UserRequest, ResponseData<string>>(userRequest, "/api/v1/system-user/update-status");
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ConvertAgent(string username,string agentId)
        {
            ConvertAgentRequest userRequest = new ConvertAgentRequest()
            {
                Username = username,
                RoleId = (int)MasterDataEnum.RoleId.Agent,
                NewUsername = agentId
            };

            var response = await _caliphAPIHelper.PostAsync<ConvertAgentRequest, ResponseData<string>>(userRequest, "/api/v1/system-user/convert-one2one-agent");
            return Json(response);
        }



        [HttpPost]
        public async Task<JsonResult> LoginAgent(string username, string pw)
        {
            var user = UserHelper.GetLoginUserViewModel();
            var key = "H6&a##5";

            UserRequest userRequest = new UserRequest()
            {
                Username = username,
                UserId = user.UserId,
                PW = HashHelper.GetHashMd5(key + pw),
            };

            var response = await _caliphAPIHelper.PostAsync<UserRequest, ResponseData<UserViewModel>>(userRequest, "/api/v1/system-user/admin-get");
            if (response == null || response.Data == null || !response.IsSuccess )
            {
                return Json(false);
            }


            var adminSession = user;

            int timeout = 30;
            var ticket = new FormsAuthenticationTicket(user.Username+" - "+ username, false, timeout);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            cookie.Secure = false;
            cookie.HttpOnly = true;

            Response.Cookies.Set(cookie);
            response.Data.AdminLoginAsAgent = true;
            response.Data.AdminSession = adminSession;
            Session["user"] = response.Data;
           


            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> UpdatePassword(string username, string password)
        {
            var key = "H6&a##5";

            UserRequest userRequest = new UserRequest()
            {
                Username = username,
                PW = HashHelper.GetHashMd5(key + password),
                UpdatedBy = UserHelper.GetLoginUser()
            };

            var response = await _caliphAPIHelper.PostAsync<UserRequest, ResponseData<string>>(userRequest, "/api/v1/system-user/update-pw");
            return Json(response);
        }
    }
}