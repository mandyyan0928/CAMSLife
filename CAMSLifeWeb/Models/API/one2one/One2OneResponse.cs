using System;
using System.Collections.Generic;

namespace CaliphWeb.Models.API.one2one
{
    public class One2OneResponse<T>
    {
        public One2OneResponse()
        {
            data = new List<T>();
        }
        public object error { get; set; }
        public object description { get; set; }
        public List<T> data { get; set; }
    }
    public class AgentHierarchyRequest
    {
        public string agent_id { get; set; }
        public string generation { get; set; }
    }
    public class AgentHierarchyResponse
    {
        public string agent_id { get; set; }
        public string agent_name { get; set; }
        public DateTime join_date { get; set; }
        public string role { get; set; }
        public string upline_agent_id { get; set; }
        public string upline_agent_name { get; set; }
        public string ic { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public int generation { get; set; }
    }


}