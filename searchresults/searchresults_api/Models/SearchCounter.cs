using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchresults_api.Models
{
    public class SearchCounter
    {
        public string SearchTerm { get; set; }

        public long? SearchCount { get; set; }
    }
}
