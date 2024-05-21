using AutoMapper;
using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models;
using CaliphWeb.Models.API;
using CaliphWeb.Models.API.AgentRecruit;
using CaliphWeb.Models.API.Client.Request;
using CaliphWeb.Models.API.Client.Response;
using CaliphWeb.Models.API.Report;
using CaliphWeb.Models.Data.Client.Request;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IMasterDataService _masterDataService;
        private readonly IUserService _userService;

        public ClientController(ICaliphAPIHelper caliphAPIHelper, IMasterDataService masterDataService, IUserService userService)
        {
            this._caliphAPIHelper = caliphAPIHelper;
            this._masterDataService = masterDataService;
            this._userService = userService;
        }
        // GET: Client
        public async Task<ActionResult> List()
        {
            var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");
            var clientlist = responseData.Data.Where(p => p.StatusId == (int)MasterDataEnum.Status.Potential || p.StatusId == (int)MasterDataEnum.Status.Confirm).ToList();

            var clientVM = new ClientListViewModel();
            clientVM.ClientData.Clients = clientlist;
            clientVM.ClientData.Paging.ItemCount = responseData.ItemCount;
            clientVM.ClientData.Paging.PageSize = filter.PageSize;
            clientVM.ClientData.Paging.CurrentPage = filter.PageNumber;
            clientVM.Status = await _masterDataService.GetClientStatusAsync();


            return View(clientVM);
        }

        [HttpPost]
        public async Task<ActionResult> List(ClientFilterRequest filter)
        {
            var clientData = new ClientData();
            filter.CreatedBy = UserHelper.GetDefaultSearchUser();
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");

            clientData.Clients = responseData.Data.Where(p => p.StatusId == (int)MasterDataEnum.Status.Potential || p.StatusId == (int)MasterDataEnum.Status.Confirm).ToList();
            clientData.Paging.ItemCount = responseData.ItemCount;
            clientData.Paging.PageSize = filter.PageSize;
            clientData.Paging.CurrentPage = filter.PageNumber;
            return PartialView("_ClientListTable", clientData);
        }


        public async Task<ActionResult> Add(int? refLeadsid, int? agentRecruitId)
        {
          
            var view = new AddClientViewModel();
            if (refLeadsid.HasValue&& refLeadsid.Value>0)
            {
                var req = new GetLeadRequest { ClientLeadId = refLeadsid.Value, PageNumber = 1, PageSize = 10 };
                var responseData = await _caliphAPIHelper.PostAsync<GetLeadRequest, ResponseData<List<ClientLead>>>(req, "/api/v1/client-lead/get-by-filter");

                if (responseData != null && responseData.Data != null && responseData.Data.Count() > 0)
                {
                    view.ClientName = responseData.Data[0].Name;
                    view.HP = responseData.Data[0].HP;
                    view.RefLeadsId = refLeadsid.Value;
                }
            }
            else  
                   if (agentRecruitId.HasValue && agentRecruitId.Value > 0)
                {
                    var req = new GetAgentRecruitmentLeadRequest { AgentLeadId = agentRecruitId.Value };
                    var responseData = await _caliphAPIHelper.PostAsync<GetAgentRecruitmentLeadRequest, ResponseData<AgentRecruitmentLead>>(req, "/api/v1/agent-lead/get-by-lead-id");

                    view.ClientName = responseData.Data.Name;
                    view.HP = responseData.Data.HP;
                    view.AgentLeadId = responseData.Data.AgentLeadId;
                }
            view.Source = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Source);
            view.AnnualIncome = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AnnualIncome);
            view.AgeRange = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AgeRange);
            view.CouldApproachOnBusiness = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.CouldApproachOnBusiness);
            view.HowOftenSeenInAYear = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowOftenSeenInAYear);
            view.HowWellKnown = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowWellKnown);
            view.LengthOfTimeKnown = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.LengthOfTimeKnown);
            view.MaritalStatus = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.MaritalStatus);
            view.Occupation = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Occupation);
            view.AbilityToProvideReferrals = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AbilityToProvideReferrals);

            return View(view);
        }

        [HttpPost]
        public async Task<JsonResult> Add(FormCollection formCollection)
        {
            var addClient = FormCollectionMapper.FormToModel<AddClientRequest>(formCollection);
            addClient.FilingDate = DateTime.Now;
            addClient.CreatedBy = UserHelper.GetLoginUser();
            var clientLeadId = formCollection.Get("ClientLeadId");
            var id = 0;
            int.TryParse(clientLeadId,out  id);
            if (id > 0)
            {
                var req = new ConvertLeadRequest { ClientLeadId = id, UpdatedBy = UserHelper.GetLoginUser() };
                var convertLeadRes = await _caliphAPIHelper.PostAsync<ConvertLeadRequest, ResponseData<string>>(req, "/api/v1/client-lead/update-convert-to-client-status");
                
            }

            var agentLeadId = formCollection.Get("AgentLeadId");
            var agentid = 0;
            int.TryParse(agentLeadId, out agentid);
            if (agentid > 0)
            {
                var req = new UpdateAgentRecruitmentStatusToConvertRequest { AgentLeadId = agentid, UpdatedBy = UserHelper.GetLoginUser() };
                var convertLeadRes = await _caliphAPIHelper.PostAsync<UpdateAgentRecruitmentStatusToConvertRequest, ResponseData<string>>(req, "/api/v1/agent-lead/update-convert-to-agent-status");

            }
            var response = await _caliphAPIHelper.PostAsync<AddClientRequest, ResponseData<string>>(addClient, "/api/v1/client/add");

            return Json(response);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var req = new GetClientRequest { ClientId = id };
            var responseData = await _caliphAPIHelper.PostAsync<GetClientRequest, ResponseData<Client>>(req, "/api/v1/client/get-by-client-id");


            var editClientVM = AutoMapHelper.Map<Client, EditClientViewModel>(responseData.Data);

            editClientVM.Source = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Source);
            editClientVM.AnnualIncome = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AnnualIncome);
            editClientVM.AgeRange = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AgeRange);
            editClientVM.CouldApproachOnBusiness = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.CouldApproachOnBusiness);
            editClientVM.HowOftenSeenInAYear = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowOftenSeenInAYear);
            editClientVM.HowWellKnown = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowWellKnown);
            editClientVM.LengthOfTimeKnown = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.LengthOfTimeKnown);
            editClientVM.MaritalStatus = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.MaritalStatus);
            editClientVM.Occupation = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Occupation);
            editClientVM.AbilityToProvideReferrals = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AbilityToProvideReferrals);
            editClientVM.Genders = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Gender);
            editClientVM.FamilyRelation = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.ClientRelationship);


            var familyReq = new ClientFamilyFilterRequest { ClientId = editClientVM.ClientId, PageNumber = 1, PageSize = 50, StatusId = 1 };
            var familyResponse = await _caliphAPIHelper.PostAsync<ClientFamilyFilterRequest, ResponseData<List<ClientFamily>>>(familyReq, "/api/v1/client-family/get-by-filter");
            if (familyResponse.IsSuccess)
                editClientVM.ClientFamilies = familyResponse.Data;


            var policyReq = new ClientPolicyRequest { ClientId = editClientVM.ClientId, PageNumber = 1, PageSize = 50, StatusId = 1 };
            var policyRes = await _caliphAPIHelper.PostAsync<ClientPolicyRequest, ResponseData<List<ClientPolicy>>>(policyReq, "/api/v1/client-policy/get-by-filter");
            if (policyRes.IsSuccess)
                editClientVM.ClientPolicies = policyRes.Data;


            return View(editClientVM);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateBasic(FormCollection formCollection)
        {
            var req = FormCollectionMapper.FormToModel<UpdateBasicInfoRequest>(formCollection);
            req.UpdatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<UpdateBasicInfoRequest, ResponseData<string>>(req, "/api/v1/client/update-basic-info");
            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateRelationship(FormCollection formCollection)
        {
            var req = FormCollectionMapper.FormToModel<UpdateRelationshipRequest>(formCollection);
            req.UpdatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<UpdateRelationshipRequest, ResponseData<string>>(req, "/api/v1/client/update-relationship-info");
            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddFamily(FormCollection formCollection)
        {
            var req = FormCollectionMapper.FormToModel<AddClientFamilyRequest>(formCollection);
            req.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<AddClientFamilyRequest, ResponseData<string>>(req, "/api/v1/client-family/add");

            var familyReq = new ClientFamilyFilterRequest { ClientId = req.ClientId, PageNumber = 1, PageSize = 50, StatusId = 1 };
            var familyResponse = await _caliphAPIHelper.PostAsync<ClientFamilyFilterRequest, ResponseData<List<ClientFamily>>>(familyReq, "/api/v1/client-family/get-by-filter");
            var clientFamilyModels = familyResponse.Data;


            return PartialView("_FamilyListTable", clientFamilyModels);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFamily(int clientFamilyId, int clientId)
        {
            var req = new DeleteClientFamilyRequest { ClientFamilyId = clientFamilyId, UpdatedBy = UserHelper.GetDefaultSearchUser() };
            var response = await _caliphAPIHelper.PostAsync<DeleteClientFamilyRequest, ResponseData<string>>(req, "/api/v1/client-family/delete");

            var familyReq = new ClientFamilyFilterRequest { ClientId = clientId, PageNumber = 1, PageSize = 50, StatusId = 1 };
            var familyResponse = await _caliphAPIHelper.PostAsync<ClientFamilyFilterRequest, ResponseData<List<ClientFamily>>>(familyReq, "/api/v1/client-family/get-by-filter");
            var clientFamilyModels = familyResponse.Data;


            return PartialView("_FamilyListTable", clientFamilyModels);
        }


        [HttpPost]
        public async Task<ActionResult> AddPolicy(FormCollection formCollection)
        {
            var req = FormCollectionMapper.FormToModel<AddClientPolicyRequest>(formCollection);
            req.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<AddClientPolicyRequest, ResponseData<string>>(req, "/api/v1/client-policy/add");


            var policyReq = new ClientPolicyRequest { ClientId = req.ClientId, PageNumber = 1, PageSize = 50, StatusId = 1 };
            var policyRes = await _caliphAPIHelper.PostAsync<ClientPolicyRequest, ResponseData<List<ClientPolicy>>>(policyReq, "/api/v1/client-policy/get-by-filter");
            var clientPolicies = policyRes.Data;

            return PartialView("_PolicyListTable", clientPolicies);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePolicy(int id, int clientId)
        {
            var req = new DeleteClientPolicyRequest { ClientPolicyId = id, UpdatedBy = UserHelper.GetDefaultSearchUser() };
            var response = await _caliphAPIHelper.PostAsync<DeleteClientPolicyRequest, ResponseData<string>>(req, "/api/v1/client-policy/delete");


            var policyReq = new ClientPolicyRequest { ClientId = clientId, PageNumber = 1, PageSize = 50, StatusId = 1 };
            var policyRes = await _caliphAPIHelper.PostAsync<ClientPolicyRequest, ResponseData<List<ClientPolicy>>>(policyReq, "/api/v1/client-policy/get-by-filter");
            var clientPolicies = policyRes.Data;

            return PartialView("_PolicyListTable", clientPolicies);
        }


        public async Task<ActionResult> Kiv()
        {
            var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy= UserHelper.GetDefaultSearchUser()  };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-kiv");
            var clientVM = new ClientListViewModel();
            clientVM.ClientData.Clients = responseData.Data;
            clientVM.ClientData.Paging.ItemCount = responseData.ItemCount;
            clientVM.ClientData.Paging.PageSize = filter.PageSize;
            clientVM.ClientData.Paging.CurrentPage = filter.PageNumber;
            clientVM.Status = await _masterDataService.GetClientStatusAsync();

            foreach (var item in clientVM.ClientData.Clients)
            {
                ActivityFilterRequest activityFilter = new ActivityFilterRequest()
                {
                    ClientId = item.ClientId,
                    PageSize = 9999
                };
                var history = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(activityFilter, "/api/v1/client-deal-activity/get-by-filter");
                item.ActivityHistory = history.Data;

            }
            return View(clientVM);
        }

        [HttpPost]
        public async Task<ActionResult> Kiv(ClientFilterRequest filter)
        {
            GetRevertKIVRequest kivFilter = new GetRevertKIVRequest()
            {
                ClientId = filter.ClientId,
                PageSize = 9999
            };
            kivFilter.CreatedBy = UserHelper.GetDefaultSearchUser();
            filter.CreatedBy = UserHelper.GetDefaultSearchUser();
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-kiv");
            var clientData = new ClientData();
            clientData.Clients = responseData.Data;
            clientData.Paging.ItemCount = responseData.ItemCount;
            clientData.Paging.PageSize = filter.PageSize;
            clientData.Paging.CurrentPage = filter.PageNumber;
            foreach (var item in clientData.Clients)
            {
                ActivityFilterRequest activityFilter = new ActivityFilterRequest()
                {
                    ClientId = item.ClientId,
                    PageSize = 9999
                };
                var history = await _caliphAPIHelper.PostAsync<ActivityFilterRequest, ResponseData<List<DealActivity>>>(activityFilter, "/api/v1/client-deal-activity/get-by-filter");
                item.ActivityHistory = history.Data;

            }
            return PartialView("_KIVListTable", clientData);
        }

        [HttpPost]
        public async Task<ActionResult> RevertKiv(int clientId)
        {
            var req = new RevertKIVRequest { ClientId = clientId, UpdatedBy = UserHelper.GetDefaultSearchUser() };
            var response = await _caliphAPIHelper.PostAsync<RevertKIVRequest, ResponseData<string>>(req, "/api/v1/client/revert-kiv");

            return Json(response);
        }

        public async Task<ActionResult> Referral()
        {
            var filter = new ReferralLeadFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy= UserHelper.GetDefaultSearchUser() };
            var responseData = await _caliphAPIHelper.PostAsync<ReferralLeadFilterRequest, ResponseData<List<Referral>>>(filter, "/api/v1/report/get-referral-leads");
            var referralViewModel = new ReferralListViewModel();
            referralViewModel.ReferralData.Referral = responseData.Data;
            referralViewModel.ReferralData.Paging.ItemCount = responseData.ItemCount;
            referralViewModel.ReferralData.Paging.PageSize = filter.PageSize;
            referralViewModel.ReferralData.Paging.CurrentPage = filter.PageNumber;
            referralViewModel.Users = await _userService.GetAgentUserAsync();
            return View(referralViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Referral(ReferralLeadFilterRequest filter)
        {
            filter.CreatedBy = UserHelper.GetDefaultSearchUser();
            var responseData = await _caliphAPIHelper.PostAsync<ReferralLeadFilterRequest, ResponseData<List<Referral>>>(filter, "/api/v1/report/get-referral-leads");
            var referralViewModel = new ReferralListViewModel();
            referralViewModel.ReferralData.Referral = responseData.Data;
            referralViewModel.ReferralData.Paging.ItemCount = responseData.ItemCount;
            referralViewModel.ReferralData.Paging.PageSize = filter.PageSize;
            referralViewModel.ReferralData.Paging.CurrentPage = filter.PageNumber;
            referralViewModel.Users = await _userService.GetAgentUserAsync();
            return PartialView("_ReferralListTable", referralViewModel.ReferralData);
        }

        //[HttpPost]
        //public async Task<ActionResult> ConvertReferral(int leadId)
        //{
        //    var req = new ConvertLeadRequest { ClientLeadId = leadId, UpdatedBy = UserHelper.GetLoginUser() };
        //    var response = await _caliphAPIHelper.PostAsync<ConvertLeadRequest, ResponseData<string>>(req, "/api/v1/client-lead/update-convert-to-client-status");

        //    return Json(response);
        //}

        public async Task<ActionResult> Archive()
        {
            string created = UserHelper.GetLoginRole() != 1 ? UserHelper.GetLoginUser() : null;
            var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 10, CreatedBy = created, StatusId=(int)MasterDataEnum.Status.Archive };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");

            var clientVM = new ClientListViewModel();
            clientVM.ClientData.Clients = responseData.Data;
            clientVM.ClientData.Paging.ItemCount = responseData.ItemCount;
            clientVM.ClientData.Paging.PageSize = filter.PageSize;
            clientVM.ClientData.Paging.CurrentPage = filter.PageNumber;
            clientVM.Status = await _masterDataService.GetClientStatusAsync();

            return View(clientVM);
        }

        [HttpPost]
        public async Task<ActionResult> Archive(ClientFilterRequest filter)
        {
            var clientData = new ClientData();
            filter.CreatedBy = UserHelper.GetDefaultSearchUser();
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");
            clientData.Clients = responseData.Data;
            clientData.Paging.ItemCount = responseData.ItemCount;
            clientData.Paging.PageSize = filter.PageSize;
            clientData.Paging.CurrentPage = filter.PageNumber;
            return PartialView("_ArchiveClientListTable", clientData);
        }

        [HttpPost]
        public async Task<ActionResult> ArchieveClient(int clientId)
        {
            var req = new ArchiveRequest { ClientId = clientId, UpdatedBy = UserHelper.GetLoginUser() };
            var response = await _caliphAPIHelper.PostAsync<ArchiveRequest, ResponseData<string>>(req, "/api/v1/client/update-archive");

            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> RevertArchive(int clientId)
        {
            var req = new UpdateStatusRequest { ClientId = clientId, UpdatedBy = UserHelper.GetLoginUser() };
            var response = await _caliphAPIHelper.PostAsync<UpdateStatusRequest, ResponseData<string>>(req, "/api/v1/client/update-confirm");

            return Json(response);
        }
    }
}