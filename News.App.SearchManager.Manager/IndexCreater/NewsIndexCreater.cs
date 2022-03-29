using Nest;
using News.App.SearchManager.Infrastructure.Model.Elastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Manager.IndexCreater
{
   public class NewsIndexCreater : IElasticIndexCreater<NewsContentElasticModel>
    {
        public AnalysisDescriptor CreateAnalysis(int? languageId)
        {
            var analyzer = new AnalysisDescriptor();

            return analyzer;
        }

        public PropertiesDescriptor<NewsContentElasticModel> CreatePropertiesDescriptor()
        {
            var desc = new PropertiesDescriptor<NewsContentElasticModel>();

            return desc;
        }
    }
}
