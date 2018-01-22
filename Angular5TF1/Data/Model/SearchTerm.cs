using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular5TF1.Data.Model
{
    public class SearchTerm
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public DateTime SearchDate { get; set; }

        public Wikipedia Wikipedia { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
