using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular5TF1.Data.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string event_id { get; set; }
        public string eventname { get; set; }
        public string thumb_url { get; set; }
        public string thumb_url_large { get; set; }
        public string banner_url { get; set; }
        public int start_time { get; set; }
        public string start_time_display { get; set; }
        public string location { get; set; }
        public string full_address { get; set; }
        public string label { get; set; }
        public int featured { get; set; }
        public string event_url { get; set; }
        public string share_url { get; set; }
        public string object_type { get; set; }
        public string end_url { get; set; }
        public SearchTerm SearchTerm { get; set; }
    }
}
