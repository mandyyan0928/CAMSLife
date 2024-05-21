using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Client.Request
{
    public class AddClientPolicyRequest
    {
        public int ClientId { get; set; }
        public string CompanyDesc { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyTypeDesc { get; set; }
        public decimal SumAssured { get; set; }
        public decimal Premium { get; set; }
        public decimal CriticaIIllnessVal { get; set; }
        public decimal PersonalAccidentVal { get; set; }
        public decimal MedicalCardVal { get; set; }
        public string CoverageTerms { get; set; }
        public string DateInForced { get; set; }
        public string CreatedBy { get; set; }
    }
}