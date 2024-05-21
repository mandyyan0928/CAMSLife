using System;

namespace CaliphWeb.Models
{
    public class ClientModel {
        public int TotalDeals { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string SourceDesc { get; set; }
        public string AgeDesc { get; set; }
        public string OccupationDesc { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public DateTime? KIVDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}