using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Deal
{
    public class DealFilterRequest
    {
        public int? ClientId { get; set; }
        public int? StatusId { get; set; }
        public int? DealTitleId { get; set; }
        public int? ActivityPointId { get; set; }
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string CreatedBy { get; set; }
    }
}