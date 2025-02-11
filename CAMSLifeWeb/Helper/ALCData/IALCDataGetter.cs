using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaliphWeb.Models;
using CaliphWeb.Models.API.one2one;
using PullAgentInfo;

namespace CaliphWeb.Helper.ALCData
{
    public interface IALCDataGetter
    {
        Task<List<AgentHierarchyResponse>> GetAgentHierarchyAsync(AgentHierarchyRequest source);
        Task<List<AgentACEResponse>> GetDailyAFYCAsync(AgentACERequest source);
        Task<List<AgentPolicyResponse>> GetPolicyDataAsync(AgentPolicyRequest source);
        Task<List<AgentMapaResponse>> GetMapaAsync(AgentMapaRequest source);
        Task<List<AgentListResponse>> GetAgentListAsync(AgentListRequest source);
    }
}
