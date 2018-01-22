using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular5TF1.Data.Model
{
    public class Wikipedia
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SearchTermId { get; set; }
        public SearchTerm SearchTerm { get; set; }
    }
}
