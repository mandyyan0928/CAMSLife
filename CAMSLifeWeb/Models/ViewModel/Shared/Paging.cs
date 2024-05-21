using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.Data
{
    public class Paging
    {
        public int ItemCount { get; set; }
        public int  CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}