using System;

namespace CaliphWeb.Models.API.UserActivity.Request
{
    public class EditUserActivityRequest {
        public int UserActivityId { get; set; }
        public int? UserId { get; set; }
        public int ActivityPointId { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }

    }
}