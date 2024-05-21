namespace CaliphWeb.Models.API.Deal
{
    public class UpdateActivitySatusRequest
    {
        public int ClientDealActivityId { get; set; }
        public string UpdatedBy { get; set; }
    }



    public class GetActivityRequest
    {
        public int ClientDealActivityId { get; set; }
        public string CreatedBy { get; set; }
    }

}