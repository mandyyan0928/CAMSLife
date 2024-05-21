using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class ActivityReviewFilterRequest
    {
        public ActivityReviewFilterRequest()
        {
            PageNumber = 1; PageSize = 9999;
        }
        public int? ActivityReviewId { get; set; }
        public int? ActivityReviewTypeId { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public string DateInWeekFrom { get; set; }
        public string DateInWeekTo { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

}