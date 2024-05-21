using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class UserFilterViewModel
    {
        public string Username { get; set; }

        public int? RoleId { get; set; }

        public int? UplineUserId { get; set; }

        public int? StatusId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string CreatedBy { get; set; }
    }
}