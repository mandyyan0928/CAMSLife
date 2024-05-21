using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.API.Report;
using CaliphWeb.Models.Data;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class RevertKIVReportViewModel
    {
        public RevertKIVReportViewModel()
        {
            Clients = new List<Client>();
            RevertKIVData = new RevertKIVData();
            Users = new List<AgentUser>();
        }
        public List<Client> Clients { get; set; }
        public List<AgentUser> Users { get; set; }

        public RevertKIVData  RevertKIVData { get; set; }
    }

    public class RevertKIVData
    {
        public RevertKIVData()
        {
            KIVRevertHistories = new List<KIVRevertHistory>();
            Paging = new Paging { PageSize = 10, CurrentPage = 1 };
        }
        public List<KIVRevertHistory> KIVRevertHistories { get; set; }
        public Paging Paging { get; set; }
    }

   
}