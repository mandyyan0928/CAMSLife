using System;
using System.Collections.Generic;
using AutoMapper;
using CaliphWeb.Models;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.ViewModel.Data;
using DocumentFormat.OpenXml.Drawing;
using PullAgentInfo;

namespace CaliphWeb.Helper.Mapper
{
    public class SunlifeMapHelper {
        public static List<AgentHierarchyResponse> MapHierarchyResponse(List<SunlifeHierarchyResponse> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap< SunlifeHierarchyResponse, AgentHierarchyResponse>()
                .ForMember(dest => dest.agent_id, opt => opt.MapFrom(src => src.advisorCode))
            .ForMember(dest => dest.agent_name, opt => opt.MapFrom(src => $"{src.firstName} {src.lastName}"))
            .ForMember(dest => dest.join_date, opt => opt.MapFrom(src => src.recruitDate))
            .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.roleCode))
            .ForMember(dest => dest.upline_agent_id, opt => opt.MapFrom(src => src.leaderAdvisorCode))
            .ForMember(dest => dest.upline_agent_name, opt => opt.Ignore());
            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<List<SunlifeHierarchyResponse>, List< AgentHierarchyResponse> >(source);
            return result;
        }

        public static List<AgentPolicyResponse> MapPolicyResponse(List<SunlifePolicyResponse> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<SunlifePolicyResponse, AgentPolicyResponse>()
                .ForMember(dest => dest.leader_code, opt => opt.MapFrom(src => src.leaderAdvisorCode))
            .ForMember(dest => dest.leader_name, opt => opt.MapFrom(src => src.leaderAdvisorRoleCode))
            .ForMember(dest => dest.selling_agent_code, opt => opt.MapFrom(src => src.sellingAdvisorCode))
            .ForMember(dest => dest.selling_agent_name, opt => opt.MapFrom(src => src.sellingAdvisorName))
            .ForMember(dest => dest.service_agent_code, opt => opt.MapFrom(src => src.serviceAdvisorCode))
            .ForMember(dest => dest.service_agent_name, opt => opt.MapFrom(src => src.serviceAdvisorName))
            .ForMember(dest => dest.spinoff_leader_code, opt => opt.MapFrom(src => src.spinOffAdvisorCode))
            .ForMember(dest => dest.spinoff_leader_name, opt => opt.MapFrom(src => src.spinOffAdvisorFullName))
            .ForMember(dest => dest.certificate_no, opt => opt.MapFrom(src => src.policyNumber))
            .ForMember(dest => dest.certificate_status, opt => opt.MapFrom(src => src.policyStatus))
            .ForMember(dest => dest.product_name, opt => opt.MapFrom(src => src.productCode))
            .ForMember(dest => dest.issue_date, opt => opt.MapFrom(src => src.issueDate))
            .ForMember(dest => dest.summission_date, opt => opt.MapFrom(src => src.commencementDate))
            .ForMember(dest => dest.due_date, opt => opt.MapFrom(src => src.dueDate))
            .ForMember(dest => dest.contribution_amount, opt => opt.MapFrom(src => src.instalmentPremiumAmount))
            .ForMember(dest => dest.status_updated_date, opt => opt.MapFrom(src => src.lastInstalmentDate))
            .ForMember(dest => dest.pay_mode, opt => opt.MapFrom(src => src.paymentTerm))
            .ForMember(dest => dest.certificate_status, opt => opt.MapFrom(src => src.policyStatus));

            });



            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<List<SunlifePolicyResponse>, List<AgentPolicyResponse>>(source);
            
            return result;
        }

        public static List<AgentMapaResponse> MapMAPAResponse(List<SunlifeMAPAResponse> source)
        {



            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<SunlifeMAPAResponse, AgentMapaResponse>()
                  .ForMember(dest => dest.agent_id, opt => opt.MapFrom(src => src.advisorCode))
            .ForMember(dest => dest.agent_name, opt => opt.MapFrom(src => src.advisorName))
            .ForMember(dest => dest.month, opt => opt.MapFrom(src => src.recruitmentMonth))
            .ForMember(dest => dest.year, opt => opt.MapFrom(src => src.recruitmentYear))
            .ForMember(dest => dest.ace_mtd, opt => opt.MapFrom(src => src.afycMtd))
            .ForMember(dest => dest.ace_ytd, opt => opt.MapFrom(src => src.afycYtd))
            .ForMember(dest => dest.case_mtd, opt => opt.MapFrom(src => src.casesMtd))
            .ForMember(dest => dest.case_ytd, opt => opt.MapFrom(src => src.casesYtd))
            .ForMember(dest => dest.manpower, opt => opt.MapFrom(src => src.manpowerYtd))
            .ForMember(dest => dest.recruit_mtd, opt => opt.MapFrom(src => src.recruitMtd))
            .ForMember(dest => dest.recruit_ytd, opt => opt.MapFrom(src => src.recruitYtd))
            .ForMember(dest => dest.active_agent_mtd, opt => opt.MapFrom(src => src.activeAdvisorMtd))
            .ForMember(dest => dest.active_agent_ytd, opt => opt.MapFrom(src => src.activeAdvisorYtd));
            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<List<SunlifeMAPAResponse>, List<AgentMapaResponse>>(source);
            return result;
        }

        public static List<AgentACEResponse> MapAFYCResponse(List<SunlifeDailyAFYCResponse> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<SunlifeDailyAFYCResponse, AgentACEResponse>()
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.date))
            .ForMember(dest => dest.ace, opt => opt.MapFrom(src => src.afyc))
            .ForMember(dest => dest.agent_id, opt => opt.MapFrom(src => src.advisorCode))
            .ForMember(dest => dest.agent_name, opt => opt.MapFrom(src => src.advisorName));

            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<List<SunlifeDailyAFYCResponse>, List<AgentACEResponse>>(source);
            return result;
        }


        public static SunlifeHierarcyRequest MapHierarchyRequest(AgentHierarchyRequest source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<AgentHierarchyRequest, SunlifeHierarcyRequest>()
                .ForMember(dest => dest.advisorCode, opt => opt.MapFrom(src => src.agent_id))
            .ForMember(dest => dest.level, opt => opt.MapFrom(src =>src.generation));
            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<AgentHierarchyRequest, SunlifeHierarcyRequest>(source);
            return result;
        }

        public static SunlifeMAPARequest MapMAPARequest(AgentMapaRequest source)
        {
          
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<AgentMapaRequest, SunlifeMAPARequest>()
                .ForMember(dest => dest.advisorCode, opt => opt.MapFrom(src => src.agent_id))
            .ForMember(dest => dest.startMonth, opt => opt.MapFrom(src => src.start_month))
            .ForMember(dest => dest.startYear, opt => opt.MapFrom(src => src.start_year))
            .ForMember(dest => dest.endMonth, opt => opt.MapFrom(src => src.end_month))
            .ForMember(dest => dest.endYear, opt => opt.MapFrom(src => src.end_year))
            ;

            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<AgentMapaRequest, SunlifeMAPARequest>(source);
            
                result.level =  GetLevel(source.type);
            return result;
        }

        public static SunlifePolicyRequest MapPolicyRequest(AgentPolicyRequest source)
        {

            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<AgentPolicyRequest, SunlifePolicyRequest>()
                .ForMember(dest => dest.advisorCode, opt => opt.MapFrom(src => src.agent_id))
            .ForMember(dest => dest.startDate, opt => opt.MapFrom(src => src.date_from))
            .ForMember(dest => dest.endDate, opt => opt.MapFrom(src => src.date_to));

            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<AgentPolicyRequest, SunlifePolicyRequest>(source);
            return result;
        }

        public static SunlifeDailyAFYCRequest MapAFYCRequest(AgentACERequest source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<AgentACERequest, SunlifeDailyAFYCRequest>()
            .ForMember(dest => dest.advisorCode, opt => opt.MapFrom(src => src.agent_id))
            .ForMember(dest => dest.startDate, opt => opt.MapFrom(src => src.date_from))
            .ForMember(dest => dest.endDate, opt => opt.MapFrom(src => src.date_to));

            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<AgentACERequest, SunlifeDailyAFYCRequest>(source);
            return result;
        }

        private static int? GetLevel(string type)
        {
            if (type == MasterDataEnum.one2oneRelationType.WHOLE_GROUP)
                return null;

            return type == MasterDataEnum.one2oneRelationType.PERSONAL ? 0 :
                   type == MasterDataEnum.one2oneRelationType.DIRECT_GROUP ? 1 :
                   type == MasterDataEnum.one2oneRelationType.G1 ? 2 :
                   type == MasterDataEnum.one2oneRelationType.G2 ? 3 :
                   type == MasterDataEnum.one2oneRelationType.G3 ? 4 :
                   throw new ArgumentOutOfRangeException(nameof(type), $"Unhandled type: {type}");
        }

        public static SunlifeAdvisorListRequest MapAdvisorListRequest(AgentListRequest source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<AgentListRequest, SunlifeAdvisorListRequest>()
                .ForMember(dest => dest.recruitOn, opt => opt.MapFrom(src => src.Date));
            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<AgentListRequest, SunlifeAdvisorListRequest>(source);
            result.itemsPerPage = 99999;
            return result;
        }

        public static List<AgentListResponse> MapAdvisorListResponse(List<SunlifeAdvisorListResponse> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<SunlifeAdvisorListResponse, AgentListResponse>()
                .ForMember(dest => dest.Agent_Id, opt => opt.MapFrom(src => src.advisorCode))
            .ForMember(dest => dest.Agent_Name, opt => opt.MapFrom(src => $"{src.firstName} {src.lastName}"))
            .ForMember(dest => dest.Join_Date, opt => opt.MapFrom(src => src.recruitDate))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.roleCode))
            .ForMember(dest => dest.Upline_Agent_Id, opt => opt.MapFrom(src => src.leaderAdvisorCode))
            .ForMember(dest => dest.Upline_Agent_Name, opt => opt.MapFrom(src => src.leaderAdvisorCode))
            .ForMember(dest => dest.Agent_Branch, opt => opt.MapFrom(src => src.costCenterCode))
            .ForMember(dest => dest.Agent_Type, opt => opt.MapFrom(src => src.mtaNumber))
            .ForMember(dest => dest.IC, opt => opt.MapFrom(src => src.otherIc))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.liamNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email));
            });

            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<List<SunlifeAdvisorListResponse>, List<AgentListResponse>>(source);
            return result;
        }

    }
}