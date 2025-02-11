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
using CaliphWeb.ViewModel.Data;
using PullAgentInfo;

public class SunlifeDataGetter :  IALCDataGetter
{
    private readonly IALCApiHelper _ALCAPIhelper;
    private readonly string _hierarchyEndpoint = "/alc/api/agency/advisors/hierarchy";
    private readonly string _policyEndpoint = "/alc/api/agency/policies/selling";
    private readonly string _dailyAFYCEndpoint = "/alc/api/agency/proddailies/afyc/daily";
    private readonly string _monthlyAFYCEndpoint = "/alc/api/agency/proddailies/afyc/monthly";
    private readonly string _MAPAEndpoint = "/alc/api/agency/proddailies/afyc/monthly";
    private readonly string _advisorListEndpoint = "/alc/api/agency/advisors";
    public SunlifeDataGetter(IRestHelper restHelper, IALCApiHelper aLCApiHelper)
    {
        this._ALCAPIhelper = aLCApiHelper;
    }


    public async Task<List<AgentHierarchyResponse>> GetAgentHierarchyAsync(AgentHierarchyRequest source)
    {
        var sunlifeRequest = SunlifeMapHelper.MapHierarchyRequest(source);
        var sunlifeResponse = await _ALCAPIhelper.GetDataAsync<SunlifeHierarcyRequest, SunlifeAPIList<SunlifeHierarchyResponse>>(sunlifeRequest, this._hierarchyEndpoint, new SunlifeAPIList<SunlifeHierarchyResponse>());
        if (sunlifeResponse == null || sunlifeResponse.subList == null)
            return new List<AgentHierarchyResponse>();
        else
            return SunlifeMapHelper.MapHierarchyResponse(sunlifeResponse.subList);
    }

    public async Task<List<AgentPolicyResponse>> GetPolicyDataAsync(AgentPolicyRequest source)
    {
        var sunlifeRequest = SunlifeMapHelper.MapPolicyRequest(source);
        var responseData = await _ALCAPIhelper.GetDataAsync<SunlifePolicyRequest, SunlifeAPIList<SunlifePolicyResponse>>(sunlifeRequest, this._policyEndpoint, new SunlifeAPIList<SunlifePolicyResponse>());
        if (responseData == null || responseData.subList == null)
            return new List<AgentPolicyResponse>();
        else
            return SunlifeMapHelper.MapPolicyResponse(responseData.subList);
    }

    public async Task<List<AgentMapaResponse>> GetMapaAsync(AgentMapaRequest source)
    {
        var sunlifeRequest = SunlifeMapHelper.MapMAPARequest(source);
        var responseData = await _ALCAPIhelper.GetDataAsync<SunlifeMAPARequest, SunlifeAPIList<SunlifeMAPAResponse>>(sunlifeRequest, this._MAPAEndpoint, new SunlifeAPIList<SunlifeMAPAResponse>());
        if (responseData == null || responseData.subList == null)
            return new List<AgentMapaResponse>();
        else
            return SunlifeMapHelper.MapMAPAResponse(responseData.subList);
    }

    public async Task<List<AgentACEResponse>> GetDailyAFYCAsync(AgentACERequest source)
    {
        var sunlifeRequest = SunlifeMapHelper.MapAFYCRequest(source);
        var responseData = await _ALCAPIhelper.GetDataAsync<SunlifeDailyAFYCRequest, SunlifeAPIList<SunlifeDailyAFYCResponse>>(sunlifeRequest, this._dailyAFYCEndpoint, new SunlifeAPIList<SunlifeDailyAFYCResponse>());
        if (responseData == null || responseData.subList == null)
            return new List<AgentACEResponse>();
        else
            return SunlifeMapHelper.MapAFYCResponse(responseData.subList);
    }

    public async Task<List<AgentListResponse>> GetAgentListAsync(AgentListRequest source)
    {
        var sunlifeRequest = SunlifeMapHelper.MapAdvisorListRequest(source);
        var responseData = await _ALCAPIhelper.GetDataAsync<SunlifeAdvisorListRequest, SunlifeAPIList<SunlifeAdvisorListResponse>>(sunlifeRequest, this._advisorListEndpoint, new SunlifeAPIList<SunlifeAdvisorListResponse>());
        if (responseData == null || responseData.subList == null)
            return new List<AgentListResponse>();
        else
            return SunlifeMapHelper.MapAdvisorListResponse(responseData.subList);
    }
}
