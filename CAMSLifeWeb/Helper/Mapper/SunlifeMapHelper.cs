using System.Collections.Generic;
using AutoMapper;
using CaliphWeb.Models.API.one2one;

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

       

    }
}