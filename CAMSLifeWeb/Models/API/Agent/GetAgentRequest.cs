using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Agent
{
    public class GetAgentRequest
    {
        public int? RoleId { get; set; }
        public string UplineUserId { get; set; }
    }

    public class AgentUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Fullname { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int UplineUserId { get; set; }
        public string UplineUsername { get; set; }
        public string UplineDisplayName { get; set; }
    }
}