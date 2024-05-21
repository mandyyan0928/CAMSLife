namespace CaliphWeb.Models.API
{
    public class ClientLeadFilter
    {
        public int? ClientLeadId { get; set; }
        public int? clientDealActivityId { get; set; }
        public int? StatusId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

}