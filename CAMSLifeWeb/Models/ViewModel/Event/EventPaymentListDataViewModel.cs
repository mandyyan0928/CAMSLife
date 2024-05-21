using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EventPaymentListDataViewModel
    {
        public EventPaymentListDataViewModel()
        {
            EventPaymentList = new List<EventPaymentListResponse>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<EventPaymentListResponse> EventPaymentList { get; set; }
        public Paging Paging { get; set; }
    }
}