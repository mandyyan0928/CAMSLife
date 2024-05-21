using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Request
{
    public class UpdateUserEventRequest
    {
        public int UserEventId { get; set; }

        public int? EventDateId { get; set; }

        public int AttendanceId { get; set; }

        public int QuizScoreId { get; set; }

        public int? CPDPoint { get; set; }

        public bool? IsEmailSent { get; set; }

        public string Remarks { get; set; }

        public string UpdatedBy { get; set; }
    }
}