using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.Data;
using System.Collections.Generic;

namespace CaliphWeb.Models.ViewModel
{
    public class EventListDataViewModel
    {
        public EventListDataViewModel()
        {
            EventList = new List<EventListResponse>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<EventListResponse> EventList { get; set; }
        public Paging Paging { get; set; }
    }
}