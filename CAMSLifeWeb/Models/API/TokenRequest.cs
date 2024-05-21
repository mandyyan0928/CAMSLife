using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel
{
    public class TokenRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string grant_type { get; set; }
    }
}