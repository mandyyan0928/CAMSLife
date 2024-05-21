using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Deal;
using CaliphWeb.Models.Data;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    //Deal & Activity Search bar
    public class ActivityListViewModel
    {
        public ActivityListViewModel()
        {
            Deals = new List<Deal>();
            Activities = new List<ActivityPoint>();
            ActivityData = new ActivityData();
        }

        public List<Deal> Deals { get; set; }
        public List<ActivityPoint> Activities { get; set; }

        public ActivityData ActivityData { get; set; }
        public string ClientName { get; set; }


    }


    //ActivityData List Paging
    public class ActivityData
    {
        public ActivityData()
        {
            Activities = new List<DealActivity>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<DealActivity> Activities { get; set; }
        public Paging Paging { get; set; }
    }
}