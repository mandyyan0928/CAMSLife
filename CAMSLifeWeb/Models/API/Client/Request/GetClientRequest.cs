using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.Data.Client.Request
{
    public class GetClientRequest
    {
        public int ClientId { get; set; }
        public string CreatedBy { get; set; }
    }
}