using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EventListViewModel
    {
        public EventListViewModel()
        {
            EventTypeList = new List<MasterData>();
            EventHostList = new List<MasterData>();
            EventList = new EventListDataViewModel();
        }

        public List<MasterData> EventTypeList { get; set; }

        public List<MasterData> EventHostList { get; set; }

        public EventListDataViewModel EventList { get; set; }
    }
}