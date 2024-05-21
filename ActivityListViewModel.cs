using CaliphWeb.Models.API.Activity;
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
        public AddActivityViewModel()
        {
            DealData = new DealData();
            ActivityData = new ActivityData();
        }
		
        public DealData  DealData  { get; set; }
        public ActivityData ActivityData { get; set; }
		public string ClientName { get; set; }
        
    }

	
	//ActivityData List Paging
    public class ActivityData
    {
        public ActivityData()
        {
            Activities = new List<Activity>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<Activity> Activities { get; set; }
        public Paging Paging { get; set; }
    }
}