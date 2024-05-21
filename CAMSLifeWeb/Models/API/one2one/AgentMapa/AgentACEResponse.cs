using System;

namespace CaliphWeb.Models
{
    public class AgentACEResponse
    {
        public string agent_id { get; set; }
        public string agent_name { get; set; }
        public DateTime date { get; set; }
        public decimal ace { get; set; }
    }
}