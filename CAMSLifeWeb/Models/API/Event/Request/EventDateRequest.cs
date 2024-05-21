using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Request
{
    public class EventDateRequest
    {
        public int EventId { get; set; }

        public int EventDateId { get; set; }

        public DateTime EventDateFrom { get; set; }

        public DateTime EventDateTo { get; set; }

        public DateTime RegClosingDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }

    public class EventDateFilterRequest
    {
        public int EventId { get; set; }

        public int EventDateId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

    }
}