using Newtonsoft.Json;

namespace CaliphWeb.ViewModel
{
    public class StripePaymentItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }


    public class StripePaymentResult
    {
        public string Id { get; set; }
        public int PaymentChannel { get; set; }
    }
}