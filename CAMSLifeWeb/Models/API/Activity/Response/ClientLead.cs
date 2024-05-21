using System;

namespace CaliphWeb.Models.API
{
    public class ClientLead
    {
        public int ClientLeadId { get; set; }
        public int ClientDealActivityId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}