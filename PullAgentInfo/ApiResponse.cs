using System;
using System.Text.Json.Serialization;

namespace PullAgentInfo
{
    public class ApiResponse
    {
        [JsonPropertyName("agent_id")]
        public string Agent_Id { get; set; }

        [JsonPropertyName("agent_name")]
        public string Agent_Name { get; set; }

        [JsonPropertyName("join_date")]
        public DateTime Join_Date { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("upline_agent_id")]
        public string Upline_Agent_Id { get; set; }

        [JsonPropertyName("upline_agent_name")]
        public string Upline_Agent_Name { get; set; }

        [JsonPropertyName("agent_branch")]
        public string Agent_Branch { get; set; }

        [JsonPropertyName("agent_type")]
        public string Agent_Type { get; set; }

        [JsonPropertyName("ic")]
        public string IC { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
