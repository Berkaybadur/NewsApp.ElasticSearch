using Elasticsearch.Net;
using Nest;
using News.App.SearchManager.Infrastructure.Model.Elastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Manager.Abstraction
{
    public interface IElasticSearchManager
    {
        /// <summary>
        /// The delete index.
        /// </summary>
        /// <param name="indexName"> The index name. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        bool DeleteIndex(string indexName);
        Task<ISearchResponse<T>> SearchAsync<T, TKey>(string indexName, SearchDescriptor<T> query) where T : ElasticEntity<TKey>;

        Health HealtCheck();
        bool CreateIndex<T>(string indexName, AnalysisDescriptor analysis, PropertiesDescriptor<T> properties, int replicaCount = 2) where T : class, new();
        Task AddAsync<T>(string indexName, List<T> model) where T : class;

    }
}
