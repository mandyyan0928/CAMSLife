using System;
using System.Collections.Generic;

namespace CaliphWeb.Models.API.Announcement.Request
{
    public class AnnouncementAdd
    {
        public string Title { get; set; }
        public int AnnouncementTypeId { get; set; }
        public List<int> UserIdList { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }
}