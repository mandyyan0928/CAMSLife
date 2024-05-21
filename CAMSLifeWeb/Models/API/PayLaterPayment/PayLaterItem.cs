using Newtonsoft.Json;

namespace CaliphWeb.ViewModel
{
    public class PayLaterItem
    {
        public int mch_id { get; set; }
        public string confirm_url { get; set; }
        public string cancel_url { get; set; }
        public string notify_url { get; set; }
        public string currency { get; set; }
        public string order_no { get; set; }
        public int amount { get; set; }
        public string ip { get; set; }
        public string sign { get; set; }
    }


    public class PayLaterPaymentResult<T>
    {
        public string info { get; set; }
        public string data { get; set; }
    }
    public class PayLaterItemForSeach
    {
        public int mch_id { get; set; }
        public string transaction_id { get; set; }
        public string order_id { get; set; }
        public string sign { get; set; }
    }
}