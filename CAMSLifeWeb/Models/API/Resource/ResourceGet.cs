using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Resource
{
    public class ResourceGet
    {
        public int? ResourceId { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}