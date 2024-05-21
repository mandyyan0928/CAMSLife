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
    public class AddStaffViewModel
    {
        public int RoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string SubMenuIds { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class AddStaffViewModel2
    {
        public AddStaffViewModel2()
        {
            Data = new List<AddStaffViewModel>();
        }
        public List<AddStaffViewModel> Data { get; set; }

    }
}


