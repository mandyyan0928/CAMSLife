using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Response
{
    public class EventPaymentListResponse
    {
        public int UserEventPaymentId { get; set; }

        public int UserEventId { get; set; }

        public string UserEventPaymentRefNo { get; set; }

        public int PaymentChannelId { get; set; }

        public string PaymentChannelDesc { get; set; }

        public string PaymentRefId { get; set; }

        public string PaymentResponse { get; set; }

        public int PayementStatusId { get; set; }

        public string PayementStatusDesc { get; set; }

        public DateTime PayementCreatedDate { get; set; }

        public string Remarks { get; set; }

        public int StatusId { get; set; }

        public string StatusDesc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}