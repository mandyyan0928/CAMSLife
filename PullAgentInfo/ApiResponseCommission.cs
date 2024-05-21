using System;
using System.Text.Json.Serialization;

namespace PullAgentInfo
{
        public class ApiResponseCommission
        {
            [JsonPropertyName("agent_id")]
            public string Agent_Id { get; set; }

            [JsonPropertyName("agent_name")]
            public string Agent_Name { get; set; }

            [JsonPropertyName("payout_date")]
            public DateTime Payout_Date { get; set; }

            [JsonPropertyName("cycle_start_date")]
            public DateTime Cycle_Start_Date { get; set; }
            [JsonPropertyName("cycle_end_date")]
            public DateTime Cycle_End_Date { get; set; }

            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }

        }

}
