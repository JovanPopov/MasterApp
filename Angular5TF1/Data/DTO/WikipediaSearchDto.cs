using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular5TF1.Data.DTO
{
    public class WikipediaSearchDto
    {
        public class Continue
        {
            public int sroffset { get; set; }
            public string @continue { get; set; }
        }

        public class Searchinfo
        {
            public int totalhits { get; set; }
        }

        public class Search
        {
            public int ns { get; set; }
            public string title { get; set; }
            public int pageid { get; set; }
            public int size { get; set; }
            public int wordcount { get; set; }
            public string snippet { get; set; }
            public DateTime timestamp { get; set; }
        }

        public class Query
        {
            public Searchinfo searchinfo { get; set; }
            public List<Search> search { get; set; }
        }

        public class RootObject
        {
            public string batchcomplete { get; set; }
            public Continue @continue { get; set; }
            public Query query { get; set; }
        }

    }
}
