using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models
{
    public class CalendarEvent
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public string startStr { get; set; }
            public string endStr { get; set; }
            public string timeZone { get; set; }
            public string CreatedBy { get; set; }

    }


    public class JsonEvents {

        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }
        public int dealId { get; set; }
        public string type { get; set; }
    }

    public class JsonActivitiesReviewEvents
    {

        public int ActivityReviewId { get; set; }
        public string ReviewText1 { get; set; }
        public string ReviewText2 { get; set; }
        public string ReviewText3 { get; set; }
        public string ReviewText4 { get; set; }
        public string ReviewText5 { get; set; }
        public string ReviewText6 { get; set; }
        public string ReviewText7 { get; set; }
        public string ReviewText8 { get; set; }
        public string ReviewText9 { get; set; }
        public string ReviewText10 { get; set; }
        public string ReviewText11 { get; set; }

    }

    public class GoogleCalender
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

    }
}