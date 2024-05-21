namespace CaliphWeb.Models.API.Client.Request
{
    public class DeleteClientFamilyRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public int ClientFamilyId { get; set; }
    
        public string UpdatedBy { get; set; }


    }
}