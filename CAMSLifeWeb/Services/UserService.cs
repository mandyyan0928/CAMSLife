using CaliphWeb.Core;
using CaliphWeb.Core.Helper;
using CaliphWeb.Helper;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CaliphWeb.Services
{
    public partial class UserService : IUserService
    {
        private readonly ICaliphAPIHelper _caliphAPIHelper;


        public UserService(ICaliphAPIHelper  caliphAPIHelper)
        {
            this._caliphAPIHelper = caliphAPIHelper;
        }


        public async Task<ResponseData<UserViewModel>> GetUserAsync(LoginViewModel loginvm)
        {
            var response = await _caliphAPIHelper.PostAsync<LoginViewModel, ResponseData<UserViewModel>>(loginvm, "/api/v1/system-user/get");
            return response;
        }

        public async Task<List<AgentUser>> GetAgentUserAsync()
        {
            var req = new GetAgentRequest();
            var loginUser = UserHelper.GetLoginUserViewModel();
            if (loginUser == null)
                return new List<AgentUser>();

            var loginAgent = new AgentUser { UserId = loginUser.UserId, Username = loginUser.Username };


            if (loginUser.IsAgent||loginUser.IsPotentialAgent)
            {

                return new List<AgentUser> { loginAgent };
            }
            if (loginUser.IsLeader)
            {
                req.UplineUserId = loginUser.UserId.ToString();
                req.RoleId = (int)MasterDataEnum.RoleId.Agent;
            }

            var response = await _caliphAPIHelper.PostAsync<GetAgentRequest, ResponseData<List<AgentUser>>>(req, "/api/v1/agent/get-by-filter");

            if (!response.Data.Any(x => x.UserId == loginAgent.UserId))
                response.Data.Add(loginAgent);
            return response.Data;
        }
           
    }
}