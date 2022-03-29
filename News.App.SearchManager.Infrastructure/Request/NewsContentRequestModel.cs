using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure.Request
{
    public class NewsContentRequestModel
    {
        public string ProviderNewsId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string XmlPath { get; set; }
        public int DisplayOrder { get; set; }
        public string ChannelCategoryMapId { get; set; }
    }
}
