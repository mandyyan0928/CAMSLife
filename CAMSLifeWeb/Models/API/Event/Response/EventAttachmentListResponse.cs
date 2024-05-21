using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Response
{
    public class EventAttachmentListResponse
    {
        public int EventId { get; set; }

        public int EventAttachmentId { get; set; }

        public string EventAttachmentName { get; set; }

        public string EventAttachmentPath { get; set; }

        public string Remarks { get; set; }

        public string CreatedBy { get; set; }
    }
}