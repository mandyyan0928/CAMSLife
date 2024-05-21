using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.Client.Request
{
    public class AddClientFamilyRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
      
            public int ClientId { get; set; }
            public string Name { get; set; }
            public int RelationId { get; set; }
            public string DOB { get; set; }
            public int GenderId { get; set; }
            public string HobbyDesc { get; set; }
            public string HPDesc { get; set; }
            public string SchoolDesc { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }


    }
}