using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel.Data
{
    public class AddDepartmentRequest
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public List<int> SubMenuIdList { get; set; }
        public string CreatedBy { get; set; }
    }




}

