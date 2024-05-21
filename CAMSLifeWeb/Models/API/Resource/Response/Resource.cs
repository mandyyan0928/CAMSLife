using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Resource.Response
{
    public class Resource
    {
        public int ResourceId { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
    }
}