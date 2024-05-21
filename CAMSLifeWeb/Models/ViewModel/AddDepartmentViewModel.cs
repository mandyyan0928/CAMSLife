using CaliphWeb.Models.API;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Deal;
using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class AddDepartmentViewModel
    {
        public List<MainMenu> Menus { get; set; }
    }

    public class MainMenu
    {
        public int MainMenuId { get; set; }
        public string Name { get; set; }
        public List<SubMenu> SubMenus { get; set; }
    }

    public class SubMenu
    {
        public int SubMenuId { get; set; }
        public string Name { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
    }
}


