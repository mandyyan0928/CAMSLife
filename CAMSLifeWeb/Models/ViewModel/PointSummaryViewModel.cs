using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class PointSummaryViewModel
    {
        public PointSummaryViewModel()
        {
            AgentUsers = new List<AgentUser>();
        }
        public  PointSummary PointSummaryTable { get; set; }
        public List<AgentUser> AgentUsers { get; set; }
    }

    public class PointSummaryTable {
        public PointSummaryTable()
        {
            PointSummaryData = new List<PointSummaryData>();
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<PointSummaryData> PointSummaryData { get; set; }

    }

    public class PointSummaryData {

        public PointSummaryData()
        {
            PointSummaries = new List<PointSummary>();
        }
        public int ActivityPointId { get; set; }
        public string ActivityPointDesc { get; set; }
        public int Point { get; set; }
        public int Sorting { get; set; }
        public List<PointSummary> PointSummaries { get; set; }
    }

    public class PointSummary
    {
        public DateTime Date { get; set; }
        public int PointReceive { get; set; }
        public int ClientDealActivityId { get; set; }
        public string ClientDealActivityDesc { get; set; }
    }

    public class PointSummaryPostFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; }
    }



    public class PointSummaryPostDetailsFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; }

        public int? ActivityId { get; set; }
    }

 
}