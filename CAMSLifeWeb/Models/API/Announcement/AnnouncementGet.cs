using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Announcement.Request
{
    public class AnnouncementGet
    {
        public int? AnnouncementId { get; set; }
        public int? UserId { get; set; }
        public DateTime? PublishStartDate { get; set; }
        public DateTime? PublishEndDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}