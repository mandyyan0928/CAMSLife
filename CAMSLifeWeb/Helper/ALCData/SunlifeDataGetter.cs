using System.Collections.Generic;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CaliphWeb.Core.Helper;
using CaliphWeb.Helper;
using CaliphWeb.Helper.ALCData;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.Services.Helper;

public class SunlifeDataGetter :  IALCDataGetter
{
    private readonly IALCApiHelper _ALCAPIhelper;
    private readonly string _hierarchyEndpoint = "/alc/api/agency/advisors/hierarchy?advisorCode=SLF30247&level=2";
    private readonly string _policyEndpoint = "/alc/api/agency/policies/selling";
    private readonly string _dailyAFYCEndpoint = "/alc/api/agency/proddailies/afyc/daily";
    private readonly string _monthlyAFYCEndpoint = "/alc/api/agency/proddailies/afyc/monthly";
    private readonly string _MAPAEndpoint = "/alc/api/agency/proddailies/afyc/monthly";
    public SunlifeDataGetter(IRestHelper restHelper, IALCApiHelper aLCApiHelper)
    {
        this._ALCAPIhelper = aLCApiHelper;
    }


    public async Task<List<AgentHierarchyResponse>> GetAgentHierarchyAsync(AgentHierarchyRequest req)
    {
        var sunlifeResponse = await _ALCAPIhelper.GetDataAsync<AgentHierarchyRequest, SunlifeAPIList<SunlifeHierarchyResponse>>(req, this._hierarchyEndpoint, new SunlifeAPIList<SunlifeHierarchyResponse>());
        if (sunlifeResponse == null || sunlifeResponse.subList == null)
            return new List<AgentHierarchyResponse>();
        else
            return SunlifeMapHelper.MapHierarchyResponse(sunlifeResponse.subList);
    }

    public async Task<List<AgentHierarchyResponse>> GetPolicyDataAsync(AgentHierarchyRequest req)
    {
        var responseData = await _ALCAPIhelper.GetDataAsync<AgentHierarchyRequest, One2OneResponse<AgentHierarchyResponse>>(req, this._hierarchyEndpoint, new One2OneResponse<AgentHierarchyResponse>());
        if (responseData == null || responseData.data == null)
            return new List<AgentHierarchyResponse>();
        else
            return responseData.data;
    }

    Task<List<AgentMapaRequest>> IALCDataGetter.GetAgentMAPAaAsync(AgentMapaResponse req)
    {
        throw new System.NotImplementedException();
    }

    Task<List<AgentACERequest>> IALCDataGetter.GetDailyAFYCAsync(AgentACEResponse req)
    {
        throw new System.NotImplementedException();
    }

  
    Task<List<AgentPolicyRequest>> IALCDataGetter.GetPolicyDataAsync(AgentPolicyResponse req)
    {
        throw new System.NotImplementedException();
    }

   
}
