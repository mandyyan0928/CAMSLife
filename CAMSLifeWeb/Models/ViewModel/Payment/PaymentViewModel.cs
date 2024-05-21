using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class PaymentViewModel
    {
        public PaymentViewModel()
        {
            PaymentChannels = new List<MasterData>();
        }

        public int UserEventId { get; set; }

        public int UserEventPaymentId { get; set; }

        public string UserEventPaymentRefNo { get; set; }

        public string Amount { get; set; }

        public string MobiApiKey { get; set; }

        public string CallbackUrl { get; set; }

        public string MobiApiId { get; set; }

        public bool IsSuccess { get; set; }

        public string PaymentRefId { get; set; }

        public string PaymentResponse { get; set; }

        public int PayementStatusId { get; set; }

        public List<MasterData> PaymentChannels { get; set; }

        public string EventName { get; set; }

        public string EventType { get; set; }

        public string EventHost { get; set; }

        public string EventChannel { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
    }
     
}