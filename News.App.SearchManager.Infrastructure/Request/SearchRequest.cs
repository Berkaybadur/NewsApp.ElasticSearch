using News.App.SearchManager.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure.Request
{
    public class SearchRequest : ElasticBase
    {
        public string Query { get; set; }

        public int Size { get; set; } = 20;

    }
}
