namespace CaliphWeb.Models.API.Deal
{
    public class EditDealRequest
    {
        public int ClientDealId { get; set; }
        public int DealTitleId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }


}