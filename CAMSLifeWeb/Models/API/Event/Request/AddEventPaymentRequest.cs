using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Request
{
    public class EventPaymentRequest
    {
        public int UserEventPaymentId { get; set; }

        public string UserEventPaymentRefNo { get; set; }

        public int UserEventId { get; set; }

        public int PaymentChannelId { get; set; }

        public int PayementStatusId { get; set; }

        public string PaymentRefId { get; set; }

        public string PaymentResponse { get; set; }

        public string Remarks { get; set; }

        public decimal Price { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}