using CaliphWeb.Models.API.Agent;
using System.Collections.Generic;

using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.API.Report;
using CaliphWeb.Models.Data;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class AttendanceReportViewModel
    {
        public AttendanceReportViewModel()
        {
            AttendanceReport = new AttendanceReport();
            Users = new List<AgentUser>();
        }
        public List<AgentUser> Users { get; set; }
        public List<MasterData> AttendanceStatuses { get; set; }
        public List<MasterData> EventStatuses { get; set; }
        public List<MasterData> PaymentChannel { get; set; }
        public AttendanceReport  AttendanceReport { get; set; }
    }

    public class AttendanceReport
    {
        public AttendanceReport()
        {
            AttendanceList = new List<EventAttendanceListResponse>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<EventAttendanceListResponse> AttendanceList { get; set; }
        public Paging Paging { get; set; }
    }
}