using CaliphWeb.Core;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaliphWeb.Services
{
    public interface IUserService
    {
        Task<List<AgentUser>> GetAgentUserAsync();
        Task<ResponseData<UserViewModel>> GetUserAsync(LoginViewModel loginvm);
    }
}