using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Response
{
    public class EventAttendanceListResponse
    {
        public int UserEventId { get; set; }

        public int UserId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public DateTime RegClosingDate { get; set; }
        public DateTime JoinDate { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public int EventDateId { get; set; }

        public int AttendanceId { get; set; }

        public string AttendanceDesc { get; set; }
        public int? UserEventStatusId { get; set; }
        public string UserEventStatusDesc { get; set; }
        public int PaymentChannelId { get; set; }

        public string PaymentChannelDesc { get; set; }

        public int QuizScoreId { get; set; }

        public string QuizScoreDesc { get; set; }

        public string Remarks { get; set; }

        public int CPDPoint { get; set; }

        public bool IsEmailSent { get; set; }

        public int StatusId { get; set; }

        public string StatusDesc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}