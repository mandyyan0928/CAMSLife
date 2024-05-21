using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.User
{
    public class MainMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
    }
}