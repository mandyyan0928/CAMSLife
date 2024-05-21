using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Agent
{
    public class AddAgentRequest
    {
        
            public string Username { get; set; }
            public string DisplayName { get; set; }
            public string Fullname { get; set; }
            public string PW { get; set; }
            public int RoleId { get; set; }
            public string UplineUsername { get; set; }
            public string IcNo { get; set; }
            public int ContactNo { get; set; }
            public string Email { get; set; }
            public DateTime JoinDate { get; set; }
            public string CreatedBy { get; set; }
    }

}