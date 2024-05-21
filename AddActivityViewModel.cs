using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class AddActivityViewModel
    {
		
		
        public AddActivityViewModel()
        {
            Deal = new List<ClientModel>();
            Activity = new List<MasterData>();
        }
        public List<ClientModel> Deal { get; set; }
        public List<MasterData> Activity { get; set; }
		public List<MasterData> Activity { get; set; }
    }
}


