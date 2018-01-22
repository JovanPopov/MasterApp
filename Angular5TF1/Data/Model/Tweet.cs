using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular5TF1.Data.Model
{
    public class Tweet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public SearchTerm SearchTerm { get; set; }
    }
}
