using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure.Request
{
    public class AddNewsRequest
    {
        public string IndexName { get; set; }
        public List<NewsContentRequestModel> Newsies { get; set; }

    }
}
