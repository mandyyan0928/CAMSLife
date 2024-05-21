namespace CaliphWeb.Models.API
{
    public class UpdateLeadRequest
    {
        public int ClientLeadId { get; set; }
        public object ClientId { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class GetLeadRequest
    {
        public int? ClientLeadId { get; set; }
        public int? ClientDealActivityId { get; set; }
        public int? StatusId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }



   







}