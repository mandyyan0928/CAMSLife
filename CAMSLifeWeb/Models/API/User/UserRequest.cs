using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class UserRequest
    {
        public UserRequest()
        {
        }
        public string Username { get; set; }

        public string DisplayName { get; set; }

        public string Fullname { get; set; }

        public int? StatusId { get; set; }
        public int? UserId { get; set; }

        public string PW { get; set; }

        public int? RoleId { get; set; }

        public string UplineUsername { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string IcNo { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }
        public DateTime JoinDate { get; set; } 

        // public string NewUsername { get; set; }
    }

    public class ConvertAgentRequest
    {
        public string Username { get; set; }


        public int? RoleId { get; set; }

        public string NewUsername { get; set; }

        // public string NewUsername { get; set; }
    }



    public class UserFilterRequest
    {
        public UserFilterRequest()
        {
            PageNumber = 1; PageSize = 9999;
        }
        public string Username { get; set; }
        public int? RoleId { get; set; }
        public string UplineUserId { get; set; }
        public string IcNo { get; set; }
        public int? StatusId { get; set; }
        public string CreatedBy { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

}