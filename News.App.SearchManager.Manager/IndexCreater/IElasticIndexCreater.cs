using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Manager.IndexCreater
{
    public interface IElasticIndexCreater<T> where T : class
    {
        AnalysisDescriptor CreateAnalysis(int? languageId = null);

        PropertiesDescriptor<T> CreatePropertiesDescriptor();

    }
}
