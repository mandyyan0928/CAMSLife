using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Request
{
    public class UpdateEventRequest
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public int EventTypeId { get; set; }

        public int EventHostId { get; set; }

        public int EventChannelId { get; set; }

        public string EventChannelLocation { get; set; }

        public string Remarks { get; set; }

        public int PaxLimit { get; set; }

        public int CPDPoint { get; set; }

        public decimal EventFees { get; set; }

        public int AttendantTypeId { get; set; }

        public string UpdatedBy { get; set; }

        public List<int> EventRoleIds { get; set; }
    }
}