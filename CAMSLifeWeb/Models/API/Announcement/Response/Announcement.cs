using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Announcement.Response
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public int AnnouncementTypeId { get; set; }
        public string AnnouncementTypeDesc { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public List<AnnouncementUser> UserList { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        //public string Description { get; set; }
        //public DateTime Date { get; set; }
         
    }


    public class AnnouncementUser
    {
        public int AnnouncementId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}