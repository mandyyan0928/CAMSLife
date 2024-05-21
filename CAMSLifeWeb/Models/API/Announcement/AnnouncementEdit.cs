using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Announcement.Request
{
    public class AnnouncementEdit
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public int AnnouncementTypeId { get; set; }
        public List<int> UserIdList { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }
}