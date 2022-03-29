using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure.Model
{
    public class ElasticBase
    {
        public string EndPoint { get; set; }

        public string Index { get; set; }
    }
}
