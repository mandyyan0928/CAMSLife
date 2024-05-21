using CaliphWeb.Models.API.Event;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EditEventViewModel
    {
        public EditEventViewModel()
        {
            EventTypeList = new List<MasterData>();
            EventHostList = new List<MasterData>();
            EventChannelList = new List<MasterData>();
            AttendantTypeList = new List<MasterData>();
            EventDates = new List<EventDateListResponse>();
            EventAttachments = new List<EventAttachmentViewModel>();
        }

        public List<MasterData> EventTypeList { get; set; }

        public List<MasterData> EventHostList { get; set; }

        public List<MasterData> EventChannelList { get; set; }

        public List<MasterData> AttendantTypeList { get; set; }

        public int EventId { get; set; }

        public string EventName { get; set; }

        public int EventTypeId { get; set; }

        public string EventTypeDesc { get; set; }

        public int EventHostId { get; set; }

        public string EventHostDesc { get; set; }

        public int EventChannelId { get; set; }

        public string EventChannelDesc { get; set; }

        public string EventChannelLocation { get; set; }

        public string Remarks { get; set; }

        public int PaxLimit { get; set; }

        public int CPDPoint { get; set; }

        public decimal EventFees { get; set; }

        public int AttendantTypeId { get; set; }

        public string AttendantTypeDesc { get; set; }

        public List<EventDateListResponse> EventDates { get; set; }
        public List<EventRoleListResponse> EventRoleList { get; set; } = new List<EventRoleListResponse>();

        public List<EventAttachmentViewModel> EventAttachments { get; set; }

        public bool IsFromCPD { get; set; }
    }



}