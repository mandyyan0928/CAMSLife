using System;
using System.Collections.Generic;
using AutoMapper;
using CaliphWeb.Models;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.ViewModel.Data;
using DocumentFormat.OpenXml.Drawing;

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
                _ = cfg.CreateMap<AgentMapaResponse, SunlifeMAPAResponse>()
                .ForMember(dest => dest.advisorCode, opt => opt.MapFrom(src => src.agent_id))
            .ForMember(dest => dest.advisorName, opt => opt.MapFrom(src => src.agent_name))
            .ForMember(dest => dest.recruitmentMonth, opt => opt.MapFrom(src => src.month))
            .ForMember(dest => dest.recruitmentYear, opt => opt.MapFrom(src => src.year))
            .ForMember(dest => dest.afycMtd, opt => opt.MapFrom(src => src.ace_mtd))
            .ForMember(dest => dest.afycYtd, opt => opt.MapFrom(src => src.ace_ytd))
            .ForMember(dest => dest.casesMtd, opt => opt.MapFrom(src => src.case_mtd))
            .ForMember(dest => dest.casesYtd, opt => opt.MapFrom(src => src.case_ytd))
            .ForMember(dest => dest.manpowerYtd, opt => opt.MapFrom(src => src.manpower))
            .ForMember(dest => dest.recruitMtd, opt => opt.MapFrom(src => src.recruit_mtd))
            .ForMember(dest => dest.recruitYtd, opt => opt.MapFrom(src => src.recruit_ytd))
            .ForMember(dest => dest.activeAdvisorMtd, opt => opt.MapFrom(src => src.active_agent_mtd))
            .ForMember(dest => dest.activeAdvisorYtd, opt => opt.MapFrom(src => src.active_agent_ytd));

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
            .ForMember(dest => dest.level, opt => opt.MapFrom(src => src.Level))
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

        private static int GetLevel(string type)
        {
            return type == MasterDataEnum.one2oneRelationType.PERSONAL ? 0 :
                   type == MasterDataEnum.one2oneRelationType.DIRECT_GROUP ? 1 :
                   type == MasterDataEnum.one2oneRelationType.G1 ? 2 :
                   type == MasterDataEnum.one2oneRelationType.G2 ? 3 :
                   type == MasterDataEnum.one2oneRelationType.G3 ? 4 :
                   throw new ArgumentOutOfRangeException(nameof(type), $"Unhandled type: {type}");
        }

    }
}