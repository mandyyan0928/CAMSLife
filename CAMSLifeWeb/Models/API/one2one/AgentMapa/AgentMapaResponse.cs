using System;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.one2one
{


    public class AgentMapaResponse
    {
        public string agent_id { get; set; }
        public string agent_name { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string ace_mtd { get; set; }
        public string ace_ytd { get; set; }
        public int case_mtd { get; set; }
        public int case_ytd { get; set; }
        public int manpower { get; set; }
        public int recruit_mtd { get; set; }
        public int recruit_ytd { get; set; }
        public int active_agent_mtd { get; set; }
        public int active_agent_ytd { get; set; }
    }

}