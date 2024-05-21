using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class ActivityFilterRequest
    {
        public ActivityFilterRequest()
        {
            PageNumber = 1; PageSize = 9999;
        }
        public int? ClientId { get; set; }
        public int? ClientDealId { get; set; }
        public int? ClientDealActivityId { get; set; }

        public int? ActivityPointId { get; set; }
        public int? DealTitleId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string CreatedBy { get; set; }
    }

}