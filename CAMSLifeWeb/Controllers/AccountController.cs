using Caliph.Library.Helper;
using CaliphWeb.Core;
using CaliphWeb.Core.Helper;
using CaliphWeb.Helper;
using CaliphWeb.Models.API;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CaliphWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IALCApiHelper _one2oneAPIHelper;


        public AccountController(IUserService userService, ICaliphAPIHelper caliphAPIHelper,IALCApiHelper one2OneApiHelper)
        {
            this._userService = userService;
            this._caliphAPIHelper = caliphAPIHelper;
            _one2oneAPIHelper = one2OneApiHelper;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        public ActionResult ChangePassword()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> ChangePasswordAsync(string oPassword, string password)
        {
            var user = UserHelper.GetLoginUserViewModel();

            var login = new LoginViewModel { Username=user.Username };
            var key = "H6&a##5";

           
            login.Password = HashHelper.GetHashMd5(key + oPassword);
            var data = await _userService.GetUserAsync(login);
            if (!data.IsSuccess)
            {
                return Json(data);
            }
            UserRequest userRequest = new UserRequest()
            {
                Username = user.Username,
                PW = HashHelper.GetHashMd5(key + password),
                UpdatedBy = UserHelper.GetLoginUser()
            };

            var response = await _caliphAPIHelper.PostAsync<UserRequest, ResponseData<string>>(userRequest, "/api/v1/system-user/update-pw");
            return Json(response);
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel login)
        
        {
            if (ModelState.IsValid)
            { //checking model state
                var key = "H6&a##5";
                login.Password = HashHelper.GetHashMd5(key + login.Password);
                var data = await _userService.GetUserAsync(login);
                if (data.IsSuccess)
                {

                    var req = new AgentHierarchyRequest { agent_id = login.Username, generation = "0" };
                    var responseData = await _one2oneAPIHelper.GetDataAsync<AgentHierarchyRequest, One2OneResponse<AgentHierarchyResponse>>(req, "/edfwebapi/alc/agenthierarchy", new One2OneResponse<AgentHierarchyResponse> { data = new List<AgentHierarchyResponse>() });

                    if (responseData != null && responseData.data != null || responseData.data.Count > 0)
                    {

                        var agent = responseData.data.Where(x => x.agent_id == login.Username).FirstOrDefault();
                        if (agent != null && agent.role == "leader")
                            data.Data.RoleId = (int)MasterDataEnum.RoleId.Leader;
                        else if (agent != null && agent.role == "agent")
                            data.Data.RoleId = (int)MasterDataEnum.RoleId.Agent;
                    }

                    int timeout = 30;
                    var ticket = new FormsAuthenticationTicket(login.Username, false, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.Secure = false;
                    cookie.HttpOnly = true;

                    Response.Cookies.Set(cookie);
                    Session["user"] = data.Data;
                    return RedirectToAction("Index", "Home");

                }

                ModelState.AddModelError("", data.StatusMsg);
                return View(login);


            }
            return View();
        }


        public ActionResult LoginV2()
        {
            return View();
        }

        public ActionResult Logout()
        {

            var user = UserHelper.GetLoginUserViewModel();
            if (user.AdminLoginAsAgent)
            {
                Session["user"] = user.AdminSession;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["user"] = null;
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            return View();
        }
    }
}