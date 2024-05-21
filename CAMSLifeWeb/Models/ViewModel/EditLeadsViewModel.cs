using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Deal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class EditLeadsViewModel
    {
        public EditLeadsViewModel()
        {
            Deals = new List<Deal>();
        }
        public ClientLead    ClientLead { get; set; }
        public List<Deal> Deals { get; set; }

        public int ClientDealId { get; set; }
    }
}