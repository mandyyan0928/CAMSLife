using System;

namespace CaliphWeb.ViewModel.Data
{
    public class UpdateActivityRequest
    {
        public int ClientDealActivityId { get; set; }
        public int ActivityPointId { get; set; }
        public DateTime ActivityStartDate => EditActivityStartDate;
        public DateTime ActivityEndDate => EditActivityEndDate;
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime EditActivityStartDate { get; set; }
        public DateTime EditActivityEndDate { get; set; }
        public string EventId { get; set; }
        public string Email { get; set; }
        public bool GoogleLinked { get; set; }
    }




}

