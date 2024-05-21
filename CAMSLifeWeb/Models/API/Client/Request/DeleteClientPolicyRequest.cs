namespace CaliphWeb.Models.API.Client.Request
{
    public class DeleteClientPolicyRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public int ClientPolicyId { get; set; }

        public string UpdatedBy { get; set; }


    }
}