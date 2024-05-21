using System;

namespace CaliphWeb.Models
{
    public class AgentACERequest
    {
        public string agent_id { get; set; }
        public string type { get; set; }
        public DateTime date_from { get; set; }
        public DateTime date_to { get; set; }
    }
}