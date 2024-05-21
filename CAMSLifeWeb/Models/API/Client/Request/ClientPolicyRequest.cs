using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Client.Request
{
    public class ClientPolicyRequest
    {
        public int ClientId { get; set; }
        public int? StatusId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}