using CaliphWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class ActivityPoint
    {
        public int ActivityPointId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public string ColorCode { get; set; }
        public int StatusId { get; set; }

        public List<PointSummary> PointSummaries { get; set; }
    }

  
}