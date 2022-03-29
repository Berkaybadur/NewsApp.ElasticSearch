using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure.Response
{
  public class NewsListResponse
    {
        public IEnumerable<NewsResponse> SearchResult { get; set; }

        public long TotalCount { get; set; }
    }
}
