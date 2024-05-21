using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Request
{
    public class EventAttachmentRequest
    {
        public int EventAttachmentId { get; set; }

        public int EventId { get; set; }

        public string EventAttachmentName { get; set; }

        public string EventAttachmentPath { get; set; }

        public string Remarks { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}