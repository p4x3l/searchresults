using System.Collections.Generic;

namespace searchresults_api.Models
{
    public class SearchResultResponse
    {
        public string SearchType { get; set; }

        public IEnumerable<SearchCounter> SearchResults { get; set; }
    }
}
