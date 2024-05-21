using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class DealActivity
    {
   

        public int ClientDealActivityId { get; set; }
        public int ClientDealId { get; set; }
        public int DealTitleId { get; set; }
        public string DealTitleDesc { get; set; }
        public int ClientDealStatusId { get; set; }
        public int ClientId { get; set; }
        public int ClientStatusId { get; set; }
        public string ClientStatusDesc { get; set; }
        public string ClientName { get; set; }
        public string ContactNo { get; set; }
        public int ActivityPointId { get; set; }
        public string ActivityPointsDesc { get; set; }
        public int Points { get; set; }
        public int PointSetting { get; set; }
        public string ColorCode { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string EventId { get; set; }
        public string Email { get; set; }
        public bool GoogleLinked { get; set; }
    }
}