using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Client.Response
{
    public class ClientFamily
    {
        public int ClientFamilyId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public int RelationId { get; set; }
        public string RelationDesc { get; set; }
        public DateTime? DOB { get; set; }
        public int GenderId { get; set; }
        public string GenderDesc { get; set; }
        public string HobbyDesc { get; set; }
        public string HPDesc { get; set; }
        public string SchoolDesc { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}