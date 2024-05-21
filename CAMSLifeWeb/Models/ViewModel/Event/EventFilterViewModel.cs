using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EventFilterViewModel
    {
        public EventFilterViewModel()
        {
            PageNumber = 1;
            PageSize = 10;

        }
        public int? EventId { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }

        public string EventName { get; set; }

        public int? EventTypeId { get; set; }

        public int? EventHostId { get; set; }

        public int? UserEventUserId { get; set; }
        public int? UserEventStatusId { get; set; }
      
        public int? AttendanceId { get; set; }

        public int? StatusId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? EventDateFrom { get; set; }

        public DateTime? EventDateTo { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int? EventDateId { get; set; }

        public int? UserEventId { get; set; }

        public int? PaymentChannelId { get; set; }

        public string UserEventPaymentRefNo { get; set; }
    }
}