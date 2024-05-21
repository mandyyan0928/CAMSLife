using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel.Data
{
    public class StaffResponse
    {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Fullname { get; set; }
            public string RoleCode { get; set; }
            public string RoleName { get; set; }
            public int RoleId { get; set; }
            public string IcNo { get; set; }
            public int ContactNo { get; set; }
            public string Email { get; set; }
            public DateTime JoinDate { get; set; }
            public DateTime LastLogin { get; set; }
            public int StatusId { get; set; }
            public string StatusDesc { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime UpdatedDate { get; set; }

    }

}