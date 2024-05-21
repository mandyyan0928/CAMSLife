using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Client.Request
{
    public class UpdateBasicInfoRequest
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string ICNo { get; set; }
        public string ContactNo { get; set; }
        public string EmailAdd { get; set; }
        public string DOB { get; set; }
        public int SourceId { get; set; }
        public int AnnualIncomeId { get; set; }
        public int AgeId { get; set; }
        public int OccupationId { get; set; }
        public int MaritalId { get; set; }
        public string OtherSourceofIncomeDesc { get; set; }
        public int GenderId { get; set; }
        public string CurrentAddress { get; set; }
        public string EducationDesc { get; set; }
        public string CareerDesc { get; set; }
        public string UpdatedBy { get; set; }
    }
}