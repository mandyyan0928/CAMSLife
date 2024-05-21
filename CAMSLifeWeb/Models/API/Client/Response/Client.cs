using CaliphWeb.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel
{
    public class Client
    {
        public int TotalDeals { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string ContactNo { get; set; }
        public string ICNo { get; set; }
        public string EmailAdd { get; set; }
        public string CurrentAddress { get; set; }
        public string EducationDesc { get; set; }
        public string IncomeYearDesc { get; set; }
        public string OtherSourceofIncomeDesc { get; set; }
        public DateTime? FilingDate { get; set; }
        public DateTime? KIVDate { get; set; }
        public DateTime? DOB { get; set; }
        public int SourceId { get; set; }
        public string SourceDesc { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public int AnnualIncomeId { get; set; }
        public string AnnualIncomeDesc { get; set; }
        public int AgeId { get; set; }
        public string AgeDesc { get; set; }
        public int OccupationId { get; set; }
        public string OccupationDesc { get; set; }
        public int MaritalId { get; set; }
        public string MaritalDesc { get; set; }
        public int? LengthOfTimeKnownId { get; set; }
        public string LengthOfTimeKnownDesc { get; set; }
        public int? HowOftenSeenInAYearId { get; set; }
        public string HowOftenSeenInAYearDesc { get; set; }
        public int? HowWellKnownId { get; set; }
        public string HowWellKnownDesc { get; set; }
        public int? CouldApproachOnBusinessId { get; set; }
        public string CouldApproachOnBusinessDesc { get; set; }
        public int? AbilityToProvideRefId { get; set; }
        public string AbilityToProvideRefDesc { get; set; }
        public int? GenderId { get; set; }
        public string GenderDesc { get; set; }
        public string CareerDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<DealActivity> ActivityHistory { get; set; }
    }
}