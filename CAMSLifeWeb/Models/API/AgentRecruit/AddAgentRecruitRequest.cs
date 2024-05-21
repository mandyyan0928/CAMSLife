using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.AgentRecruit
{
    public class AddAgentRecruitRequest
    {

        public string Name { get; set; }
        public string ContactNo { get; set; }
        public int EducationBgId { get; set; }
        public int AgeId { get; set; }
        public string EmailAdd { get; set; }
        public long AnnualIncomeId { get; set; }
        public long OccupationId { get; set; }
        public long MaritalId { get; set; }
        public long TypeId { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }


    public class UpdateAgentRecruitRequest
    {
        public int AgentRecruitId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public int EducationBgId { get; set; }
        public int AgeId { get; set; }
        public string EmailAdd { get; set; }
        public long AnnualIncomeId { get; set; }
        public long OccupationId { get; set; }
        public long MaritalId { get; set; }
        public long TypeId { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }


    public class UpdateAgentRecruitStatusRequest
    {
        public int AgentRecruitId { get; set; }
        public int StatusId { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class FiltertAgentRecruitRequest
    {
        public int? AgentRecruitId { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
    }
    public class AddAgentRecruitTrackRequest
    {
        public int AgentRecruitId { get; set; }
        public string TrackRemarks { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateAgentRecruitTrackRequest
    {
        public int AgentRecruitTrackId { get; set; }
        public string TrackRemarks { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class FilterAgentRecruitTrackRequest
    {
        public object AgentRecruitTrackId { get; set; }
        public int AgentRecruitId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
    public class AddAgentRecruitActivityRequest
    {
        public int AgentId { get; set; }
        public int ActivityPointId { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }
    public class UpdateAgentRecruitActivityRequest
    {
        public int AgentActivityId { get; set; }
        public int ActivityPointId { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class FilterAgentRecruitActivityRequest
    {
        public int? AgentActivityId { get; set; }
        public int? AgentId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GetAgentActivityRequest
    {
        public int AgentActivityId { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class AddAgentRecruitmentLeadRequest
    {
        public int AgentActivityId { get; set; }
        public int AgentId { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GetAgentRecruitmentLeadRequest
    {
        public int AgentLeadId { get; set; }
    }

    public class FilterAgentRecruitmentLeadRequest
    {
        public int? AgentLeadId { get; set; }
        public int? AgentActivityId { get; set; }
        public int? StatusId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; } 
          public string CreatedBy { get; set; }
    }


    public class UpdateAgentRecruitmentLeadRequest
    {
        public int AgentLeadId { get; set; }
        public int AgentId { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }


    public class DeleteAgentRecruitmentLeadRequest
    {
        public int AgentLeadId { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class UpdateAgentRecruitmentStatusToLeadRequest
    {
        public int AgentLeadId { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UpdateAgentRecruitmentStatusToConvertRequest
    {
        public int AgentLeadId { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class AgentRecruiment
    {
        public int AgentRecruitId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public int EducationBgId { get; set; }
        public string EducationBgDesc { get; set; }
        public int AgeId { get; set; }
        public string AgeDesc { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string EmailAdd { get; set; }
        public long AnnualIncomeId { get; set; }
        public string AnnualIncomeDesc { get; set; }
        public long OccupationId { get; set; }
        public string OccupationDesc { get; set; }
        public long MaritalId { get; set; }
        public string MaritalDesc { get; set; }
        public long TypeId { get; set; }
        public string TypeDesc { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<AgentRecruimentActivity> Activities { get; internal set; }
        public DateTime? NextActivityDate { get; internal set; }
        public string NextActivityDesc { get; internal set; }
    }

    public class FilterAgentRecruitmentResponse
    {
        public List<AgentRecruiment> data { get; set; }
        public int ItemCount { get; set; }
        public string StatusCode { get; set; }
        public string StatusMsg { get; set; }
    }






    public class AgentRecruitmentTrack
    {
        public int AgentRecruitTrackId { get; set; }
        public int AgentRecruitId { get; set; }
        public string TrackRemarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
 




    public class AgentRecruimentActivity
    {
        public int AgentActivityId { get; set; }
        public int AgentId { get; set; }
        public int AgentStatusId { get; set; }
        public string AgentStatusDesc { get; set; }
        public string AgentName { get; set; }
        public int ActivityPointId { get; set; }
        public string ActivityPointsDesc { get; set; }
        public int Points { get; set; }
        public int PointSetting { get; set; }
        public string ColorCode { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

  


    public class AgentRecruitmentLead
    {
        public int AgentActivityId { get; set; }
        public int AgentLeadId { get; set; }
        public int AgentId { get; set; }
        public string AgentRecruitName { get; set; }

        public string Name { get; set; }
        public string HP { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }



 


}