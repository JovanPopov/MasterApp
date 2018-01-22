using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular5TF1.Data.DTO
{
    public class AlleventsDto
    {
        public class Request
        {
            public string query { get; set; }
            public int latitude { get; set; }
            public int longitude { get; set; }
            public string city { get; set; }
            public int timestamp { get; set; }
        }

        public class Venue
        {
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string full_address { get; set; }
        }

        public class Event
        {
            public string event_id { get; set; }
            public string eventname { get; set; }
            public string thumb_url { get; set; }
            public string thumb_url_large { get; set; }
            public string banner_url { get; set; }
            public int start_time { get; set; }
            public string start_time_display { get; set; }
            public string location { get; set; }
            public Venue venue { get; set; }
            public string label { get; set; }
            public int featured { get; set; }
            public string event_url { get; set; }
            public string share_url { get; set; }
            public string object_type { get; set; }
            public string end_url { get; set; }
        }

        public class RootObject
        {
            public string message { get; set; }
            public int error { get; set; }
            public Request request { get; set; }
            public int rows { get; set; }
            public List<Event> data { get; set; }
        }
    }
}
