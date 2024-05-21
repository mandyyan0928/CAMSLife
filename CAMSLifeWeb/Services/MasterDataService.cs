using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Models.API;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CaliphWeb.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        public MasterDataService(ICaliphAPIHelper caliphAPIHelper)
        {
            _caliphAPIHelper = caliphAPIHelper;
        }

        public async Task<List<MasterData>> GetMasterDatasAsync(MasterDataEnum.MasterData masterid)
        {
            var req = new MasterDataRequest { MasterId = (int)masterid };
            var response = await _caliphAPIHelper.PostAsync<MasterDataRequest, ResponseData<List<MasterData>>>(req, "/api/v1/master/get-by-masterid");
            return response.Data;
        }
        public async Task<List<MasterData>> GetClientStatusAsync()
        {
            var response = await _caliphAPIHelper.PostAsync< ResponseData<List<MasterData>>>("/api/v1/master/get-client-status");
            return response.Data;
        }
        public async Task<List<MasterData>> GetDealStatusAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-deal-status");
            return response.Data;
        }
        public async Task<List<MasterData>> GetDealActivityStatusAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-activity");
            return response.Data;
        }
        public async Task<List<ActivityPoint>> GetActivityPointAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<ActivityPoint>>>("/api/v1/master/get-activity");
            return response.Data;
        }
        public async Task<List<ActivityPoint>> GetSalesActivityPointAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<ActivityPoint>>>("/api/v1/master/get-activity");
            var returnData = response.Data.Where(x => new[] {
               (int)MasterDataEnum.SalesActivityPoint.ApproachInPerson,
               (int)MasterDataEnum.SalesActivityPoint.ApptSecured,
                   (int)MasterDataEnum.SalesActivityPoint.ClosingInterview,
                    (int)MasterDataEnum.SalesActivityPoint.Salesappointments,
                    (int)MasterDataEnum.SalesActivityPoint.SalesCall,
                    (int)MasterDataEnum.SalesActivityPoint.Servicing_Followup,
                       (int)MasterDataEnum.SalesActivityPoint.RefLeads,
                    (int)MasterDataEnum.SalesActivityPoint.Survey,
                    (int)MasterDataEnum.SalesActivityPoint.Sales,
            }.Contains(x.ActivityPointId)).ToList();
            return returnData;
        }
        public async Task<List<ActivityPoint>> GetAgentRecruitmentActivityPointAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<ActivityPoint>>>("/api/v1/master/get-activity");
            var returnData = response.Data.Where(x => new[] {
               (int)MasterDataEnum.SalesActivityPoint.RecruitmentCall,
               (int)MasterDataEnum.SalesActivityPoint.RecruitmentApproach,
                   (int)MasterDataEnum.SalesActivityPoint.InitialInterview,
                    (int)MasterDataEnum.SalesActivityPoint.CareerPresentation,
                    (int)MasterDataEnum.SalesActivityPoint.VIPInterview,
                    (int)MasterDataEnum.SalesActivityPoint.JobSampling,
                       (int)MasterDataEnum.SalesActivityPoint.RefLeads,
                       (int)MasterDataEnum.SalesActivityPoint.AgentContracted,
                    (int)MasterDataEnum.SalesActivityPoint.FinalInterview,
                    (int)MasterDataEnum.SalesActivityPoint.Training,
                    (int)MasterDataEnum.SalesActivityPoint.JoinWorkField,
                    (int)MasterDataEnum.SalesActivityPoint.Coaching_One2One,
                    (int)MasterDataEnum.SalesActivityPoint.MiniBOP,
            }.Contains(x.ActivityPointId)).ToList();
            return returnData;
        }
        public async Task<List<MasterData>> GetAnnouncementTypeAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-announcement-type");
            return response.Data;
        }

        public async Task<List<MasterData>> GetEventTypeAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-event-type");
            return response.Data;
        }

        public async Task<List<MasterData>> GetEventHostAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-event-host");
            return response.Data;
        }

        public async Task<List<MasterData>> GetEventChannelAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-event-channel");
            return response.Data;
        }

        public async Task<List<MasterData>> GetEventAttendantTypeAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-attendant-type");
            return response.Data;
        }

        public async Task<List<MasterData>> GetEventQuizScoreAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-quiz-score");
            return response.Data;
        }

        public async Task<List<MasterData>> GetUserEventStatusAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-user-event-status");
            return response.Data;
        }

        public async Task<List<MasterData>> GetPaymentChannelAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-payment-channel");
            return response.Data;
        }

        public async Task<List<MasterData>> GetEventPaymentStatusAsync()
        {
            var response = await _caliphAPIHelper.PostAsync<ResponseData<List<MasterData>>>("/api/v1/master/get-payment-status");
            return response.Data;
        }
    }
}