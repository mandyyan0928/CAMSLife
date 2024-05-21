using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.one2one
{
    public class AgentMapaRequest
    {
        public string agent_id { get; set; }
        public string type { get; set; }
        public int start_month { get; set; }
        public int start_year { get; set; }
        public int end_month { get; set; }
        public int end_year { get; set; }
    }
}