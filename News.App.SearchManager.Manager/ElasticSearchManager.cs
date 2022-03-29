using Elasticsearch.Net;
using Nest;
using News.App.SearchManager.Infrastructure.Model.Elastic;
using News.App.SearchManager.Manager.Abstraction;
using News.App.SearchManager.Manager.ElasticSearchOptions.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Manager
{
    public class ElasticSearchManager : IElasticSearchManager
    {
        #region Fields

        /// <summary>
        /// The client.
        /// </summary>
        private readonly IElasticClient _elasticClient;
        private readonly IElasticSearchConfigration _elasticSearchConfigration;
        #endregion

        #region CTor

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticSearchManager"/> class.
        /// </summary>
        /// <param name="serverUrl"> The server url. </param>
        /// <param name="elasticSearchRules"
        public ElasticSearchManager(IElasticSearchConfigration elasticSearchConfigration)
        //IElasticSearchRules elasticSearchRules)
        {
            _elasticSearchConfigration = elasticSearchConfigration;
            _elasticClient = GetClient();
        }

        private ElasticClient GetClient()
        {
            var str = _elasticSearchConfigration.ConnectionString;

            var connectionString = new ConnectionSettings(new Uri(str));

            if (!string.IsNullOrEmpty(_elasticSearchConfigration.AuthUserName) && !string.IsNullOrEmpty(_elasticSearchConfigration.AuthPassWord))
                connectionString.BasicAuthentication(_elasticSearchConfigration.AuthUserName, _elasticSearchConfigration.AuthPassWord);

#if DEBUG
            connectionString.EnableDebugMode();
            connectionString.PrettyJson();
#endif

            return new ElasticClient(connectionString);
        }

        #endregion

        #region IElasticSearchManager Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">mapping Model</typeparam>
        /// <param name="indexName"> İndex Name</param>
        /// <param name="analysis"> Analysis for the mapping</param>
        /// <param name="properties"> Mapping model properties </param>
        /// <param name="replicaCount">index replica count default =2</param>
        /// <returns></returns>
        public bool CreateIndex<T>(string indexName, AnalysisDescriptor analysis, PropertiesDescriptor<T> properties, int replicaCount = 1) where T : class, new()
        {


            return _elasticClient.Indices.Exists(indexName.ToLower()).Exists
                           || _elasticClient.Indices.Create(indexName.ToLower(),
                           v => CreateMap<T>(indexName.ToLower(), analysis, properties, replicaCount)).IsValid;
        }

        /// <summary>
        /// Customization index setting and mapping analyzer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexName"></param>
        /// <param name="analysis"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private CreateIndexDescriptor CreateMap<T>(string indexName, AnalysisDescriptor analysis, PropertiesDescriptor<T> properties, int replicaCount = 2) where T : class, new()
        {
            var desc = new CreateIndexDescriptor(indexName.ToLower());
            desc.Settings(v =>
                            v.NumberOfReplicas(replicaCount)
                             .Setting(UpdatableIndexSettings.MaxNGramDiff, 12)
                             .Analysis(g => analysis)

                          )
                .Map<T>(v => v.Properties(v => properties).AutoMap());

            return desc;
        }

        /// <summary>
        /// The delete index.
        /// </summary>
        /// <param name="indexName"> The index name. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool DeleteIndex(string indexName)
        {
            return !_elasticClient.Indices.Exists(indexName.ToLower()).Exists || this._elasticClient.Indices.Delete(indexName.ToLower()).IsValid;
        }

        public async Task<ISearchResponse<T>> SearchAsync<T, TKey>(string indexName, SearchDescriptor<T> query) where T : ElasticEntity<TKey>
        {
            query.Index(indexName);
            var response = await _elasticClient.SearchAsync<T>(query);
            return response;
        }

        public Health HealtCheck()
        {
            return _elasticClient.Cluster.Health().Status;
        }

        public async Task AddAsync<T>(string indexName, List<T> model) where T : class
        {
            var result = await _elasticClient.BulkAsync(b => b.Index(indexName.ToLower()).IndexMany<T>(model));
            if (result.ServerError == null) return;
            throw new Exception($"Insert Docuemnt failed at index {indexName} :" + result.ServerError.Error.Reason);
        }

        #endregion
    }
}
