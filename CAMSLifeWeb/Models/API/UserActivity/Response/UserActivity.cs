using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.UserActivity.Response
{
    public class UserActivity
    {
        public int UserActivityId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int ActivityPointId { get; set; }
        public string ActivityPointsDesc { get; set; }
        public int PointSetting { get; set; }
        public string ColorCode { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}