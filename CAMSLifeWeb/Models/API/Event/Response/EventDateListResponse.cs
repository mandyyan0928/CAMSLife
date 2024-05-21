using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Event.Response
{
    public class EventDateListResponse
    {
        public int EventId { get; set; }

        public int StatusId { get; set; }

        public string StatusDesc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int EventDateId { get; set; }

        public DateTime EventDateFrom { get; set; }

        public DateTime EventDateTo { get; set; }

        public DateTime RegClosingDate { get; set; }

        public int EventDateStatusId { get; set; }

        public int TotalAttendance { get; set; }
    }


    public class EventRoleListResponse
    {
        public int EventRoleId { get; set; }
        public int EventId { get; set; }
        public int RoleId { get; set; }
        public string EventRoleDesc { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        
    }

}