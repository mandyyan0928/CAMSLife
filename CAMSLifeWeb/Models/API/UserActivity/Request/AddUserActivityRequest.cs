using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.UserActivity.Request
{
    public class AddUserActivityRequest
    {

        public int UserId { get; set; }
        public int ActivityPointId { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }
}