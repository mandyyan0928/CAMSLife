using CaliphWeb.Models.API.Client.Response;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EditClientViewModel
    {
        public EditClientViewModel()
        {
            ClientFamilies = new List<ClientFamily>();
            ClientPolicies = new List<ClientPolicy>();
        }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string ContactNo { get; set; }
        public string ICNo { get; set; }
        public string EmailAdd { get; set; }
        public object CurrentAddress { get; set; }
        public string EducationDesc { get; set; }
        public string IncomeYearDesc { get; set; }
        public object OtherSourceofIncomeDesc { get; set; }
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
        public int LengthOfTimeKnownId { get; set; }
        public string LengthOfTimeKnownDesc { get; set; }
        public int HowOftenSeenInAYearId { get; set; }
        public string HowOftenSeenInAYearDesc { get; set; }
        public int HowWellKnownId { get; set; }
        public string HowWellKnownDesc { get; set; }
        public int CouldApproachOnBusinessId { get; set; }
        public string CouldApproachOnBusinessDesc { get; set; }
        public int AbilityToProvideRefId { get; set; }
        public string AbilityToProvideRefDesc { get; set; }
        public int GenderId { get; set; }
        public string GenderDesc { get; set; }
        public string CareerDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }



        public List<MasterData> Source { get; set; }
        public List<MasterData> AgeRange { get; set; }
        public List<MasterData> AnnualIncome { get; set; }
        public List<MasterData> Occupation { get; set; }
        public List<MasterData> MaritalStatus { get; set; }
        public List<MasterData> LengthOfTimeKnown { get; set; }
        public List<MasterData> HowWellKnown { get; set; }
        public List<MasterData> HowOftenSeenInAYear { get; set; }
        public List<MasterData> CouldApproachOnBusiness { get; set; }
        public List<MasterData> AbilityToProvideReferrals { get; set; }
        public List<MasterData> Genders { get; set; }
        public List<MasterData> FamilyRelation { get; set; }

        public List<ClientFamily>  ClientFamilies { get; set; }
        public List<ClientPolicy>  ClientPolicies { get; set; }
    }
}