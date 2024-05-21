namespace CaliphWeb.Models.API.Client.Request
{
    public class RevertKIVRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public int ClientId { get; set; }

        public string UpdatedBy { get; set; }


    }
}