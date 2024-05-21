using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Client.Request
{
    public class UpdateRelationshipRequest
    {
        public int ClientId { get; set; }
        public string FilingDate { get; set; }
        public int LengthOfTimeKnownId { get; set; }
        public int HowWellKnownId { get; set; }
        public int HowOftenSeenInAYearId { get; set; }
        public int CouldApproachOnBusinessId { get; set; }
        public int AbilityToProvideRefId { get; set; }
        public string UpdatedBy { get; set; }
    }
}