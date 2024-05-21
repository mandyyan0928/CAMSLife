using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel
{
    public class AddClientViewModel
    {
        public List<MasterData> Source { get; set; }
        public List<MasterData> AgeRange { get; set; }
        public List<MasterData> AnnualIncome { get; set; }
        public List<MasterData> Occupation { get; set; }
        public List<MasterData> MaritalStatus { get; set; }
        public List<MasterData> LengthOfTimeKnown { get; set; }
        public List<MasterData> HowWellKnown { get; set; }
        public List<MasterData> HowOftenSeenInAYear { get; set; }
        public List<MasterData> CouldApproachOnBusiness { get; set; }
        public List<MasterData> AbilityToProvideReferrals { get; set; }
        public int RefLeadsId { get; set; }
        public string ClientName { get; set; }
        public string HP { get; set; }
        public int AgentLeadId { get; set; }

    }
   
}