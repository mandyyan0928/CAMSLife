using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Report
{
    public class GetRevertKIVRequest
    {
    
            public object ClientId { get; set; }
            public string Name { get; set; }
            public DateTime? KIVDateFrom { get; set; }
            public DateTime? KIVDateTo { get; set; }
            public int PageSize { get; set; }
            public int PageNumber { get; set; }
            public string CreatedBy { get; set; }


    }


}