using News.App.SearchManager.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure.Model
{
    public class FeatureFilter
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<FilterValue> FilterValues { get; set; }
        public FilterType Type { get; set; }
    }

    public class FilterValue
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long Total { get; set; }
    }
}
