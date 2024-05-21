using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel.Data
{
    public class AddActivityRequest
    {

        public string ClientDealId { get; set; }
        public string ActivityPointId { get; set; }
        public string ActivityStartDate { get; set; }
        public string ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
    }
} 

