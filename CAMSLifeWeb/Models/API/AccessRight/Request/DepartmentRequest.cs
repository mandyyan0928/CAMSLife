using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel.Data
{
    public class ClientFilterRequest
    {
      
        public int? ClientId { get; set; }
        public int? StatusId { get; set; }
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
    }
}