using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class AddDealViewModel
    {
        public AddDealViewModel()
        {
            Clients = new List<Client>();
            Titles = new List<MasterData>();
        }
        public List<Client> Clients { get; set; }
        public List<MasterData> Titles { get; set; }
    }
}