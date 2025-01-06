using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaliphWeb.Models;
using CaliphWeb.Models.API.one2one;

namespace CaliphWeb.Helper.ALCData
{
    public interface IALCDataGetter
    {
        Task<List<AgentHierarchyResponse>> GetAgentHierarchyAsync(AgentHierarchyRequest req);
        Task<List<AgentPolicyRequest>> GetPolicyDataAsync(AgentPolicyResponse req);
        Task<List<AgentMapaRequest>> GetAgentMAPAaAsync(AgentMapaResponse req);
        Task<List<AgentACERequest>> GetDailyAFYCAsync(AgentACEResponse req);
       
    }
}
