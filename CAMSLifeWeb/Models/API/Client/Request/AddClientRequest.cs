using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel.Data
{
    public class AddClientRequest
    {

        public string Name { get; set; }
        public string NickName { get; set; }
        public string ICNo { get; set; }
        public string ContactNo { get; set; }
        public string EmailAdd { get; set; }
        public int SourceId { get; set; }
        public int AnnualIncomeId { get; set; }
        public int AgeId { get; set; }
        public int OccupationId { get; set; }
        public int MaritalId { get; set; }
        public int LengthOfTimeKnownId { get; set; }
        public int HowWellKnownId { get; set; }
        public int HowOftenSeenInAYearId { get; set; }
        public int CouldApproachOnBusinessId { get; set; }
        public int AbilityToProvideRefId { get; set; }
        public int GenderId { get; set; }
        public string EducationDesc { get; set; }
        public string IncomeYearDesc { get; set; }
        public string OtherSourceofIncomeDesc { get; set; }
        public string CareerDesc { get; set; }
        public DateTime? FilingDate { get; set; }
        public string CreatedBy { get; set; }
    }
}