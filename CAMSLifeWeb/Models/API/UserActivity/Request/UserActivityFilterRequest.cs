using System;

namespace CaliphWeb.Models.API.UserActivity.Request
{
    public class UserActivityFilterRequest
    {
        public int? UserActivityId { get; set; }
        public int?  UserId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}