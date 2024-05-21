using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class UserListResponse
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public string PW { get; set; }

        public string Fullname { get; set; }

        public string RoleCode { get; set; }

        public string RoleName { get; set; }

        public int RoleId { get; set; }

        public int StatusId { get; set; }

        public string StatusDesc { get; set; }

        public int UplineUserId { get; set; }

        public string UplineUsername { get; set; }

        public string UplineDisplayName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

      
        
    }
}