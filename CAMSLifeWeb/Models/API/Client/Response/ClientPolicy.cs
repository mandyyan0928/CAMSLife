using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Client.Response
{
    public class ClientPolicy
    {
        public int ClientPolicyId { get; set; }
        public int ClientId { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string CompanyDesc { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyTypeDesc { get; set; }
        public double SumAssured { get; set; }
        public double Premium { get; set; }
        public double CriticaIIllnessVal { get; set; }
        public double PersonalAccidentVal { get; set; }
        public double MedicalCardVal { get; set; }
        public string CoverageTerms { get; set; }

        public DateTime? DateInForced { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}