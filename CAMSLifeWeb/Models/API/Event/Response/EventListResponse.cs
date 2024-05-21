using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Response
{
    public class EventListResponse
    {
        public EventListResponse()
        {
            EventDateList = new List<EventDateListResponse>();
        }

        public int EventId { get; set; }

        public string EventName { get; set; }

        public int EventTypeId { get; set; }

        public string EventTypeDesc { get; set; }

        public int EventHostId { get; set; }

        public string EventHostDesc { get; set; }

        public int EventChannelId { get; set; }

        public string EventChannelDesc { get; set; }

        public string EventChannelLocation { get; set; }

        public decimal EventFees { get; set; }

        public string Remarks { get; set; }

        public int PaxLimit { get; set; }

        public int CPDPoint { get; set; }

        public int AttendantTypeId { get; set; }

        public string AttendantTypeDesc { get; set; }

        public int StatusId { get; set; }

        public string StatusDesc { get; set; }

        public int? UserEventStatusId { get; set; }

        public string UserEventStatusDesc { get; set; }

        public int EventDateId { get; set; }

        public DateTime EventDateFrom { get; set; }

        public DateTime EventDateTo { get; set; }

        public DateTime RegClosingDate { get; set; }

        public int? UserEventId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<EventDateListResponse> EventDateList { get; set; }
        public List<EventRoleListResponse> EventRoleList { get; set; }
    }
}