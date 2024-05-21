using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Resource
{
    public class ResourceEdit
    {
        public int ResourceId { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string UpdatedBy { get; set; }
    }
}