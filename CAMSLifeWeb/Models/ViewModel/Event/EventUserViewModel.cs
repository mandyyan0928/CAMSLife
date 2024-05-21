using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EventUserViewModel
    {
        public int UserEventId { get; set; }

        public int EventDateId { get; set; }

        public int AttendanceId { get; set; }

        public int QuizScoreId { get; set; }

        public int CPDPoint { get; set; }

        public bool IsEmailSent { get; set; }

        public string Remarks { get; set; }

        public string UpdatedBy { get; set; }
    }

    public class ManualAttendanceViewModel
    {
        public ManualAttendanceViewModel()
        {
            Events = new List<EventListResponse>();
            Agents = new List<AgentUser>();
            QuizScores = new List<MasterData>();

        }
        public List<EventListResponse> Events { get; set; }

        public List<AgentUser> Agents { get; set; }

        public List<MasterData> QuizScores { get; set; }

    }


    public class EventDateSelect
    {

        public int  EventDateId { get; set; }
        public string EventDate { get; set; }
    }
}