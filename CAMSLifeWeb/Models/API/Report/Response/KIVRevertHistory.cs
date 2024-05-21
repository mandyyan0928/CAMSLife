using System;

namespace CaliphWeb.Models.API.Report
{
    public class KIVRevertHistory
    {
        public int ClientKIVHistoryId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public DateTime? FilingDate { get; set; }
        public DateTime? KIVDate { get; set; }
        public DateTime? RevertDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }



}