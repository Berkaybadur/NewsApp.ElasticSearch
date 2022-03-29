using News.App.SearchManager.Data.Abstraction;
using News.App.SearchManager.Infrastructure.Model.Elastic;
using News.App.SearchManager.Infrastructure.Request;
using News.App.SearchManager.Infrastructure.Response;
using News.App.SearchManager.Manager.Abstraction;
using News.App.SearchManager.Manager.IndexCreater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Manager
{
    public class NewsManager : INewsManager
    {
        private readonly IElasticQueryCreater _elasticQuery;
        private readonly IElasticSearchManager _elasticSearchManager;
        private readonly IElasticIndexCreater<NewsContentElasticModel> _NewsContentIndexCreater;
        public NewsManager(
            IElasticQueryCreater elasticQuery,
            IElasticIndexCreater<NewsContentElasticModel> NewsContentIndexCreater,
            IElasticSearchManager elasticSearchManager
            )
        {
            _elasticQuery = elasticQuery;
            _NewsContentIndexCreater = NewsContentIndexCreater;
            _elasticSearchManager = elasticSearchManager;
        }
        public bool CreateIndexAsync(string indexName)
        {
            var hasCreated = _elasticSearchManager.CreateIndex(indexName, _NewsContentIndexCreater.CreateAnalysis(), _NewsContentIndexCreater.CreatePropertiesDescriptor());

            return hasCreated;
        }
        public async void AddAsync(AddNewsRequest request)
        {
            var esModel = request.Newsies.Select(x => new NewsContentElasticModel
            {
                ProviderNewsId = x.ProviderNewsId,
                DisplayOrder = x.DisplayOrder,
                Language = x.Language,
                Title = x.Title,
                XmlPath = x.XmlPath,
                Description = x.Description,
                Link = x.Link,
                ChannelCategoryMapId = x.ChannelCategoryMapId
            }).ToList();

            await _elasticSearchManager.AddAsync(request.IndexName, esModel);
        }

        public async Task<NewsListResponse> SearchAsync(SearchRequest searchRequest)
        {
            var response = await _elasticQuery.SearchAsync(searchRequest);


            if (!response.IsValid)
            {
                //log or throw exception or return default model
            }
            var result = response.Documents.Select(v =>
               new NewsResponse
               {
                   ProviderNewsId = v.ProviderNewsId,
                   DisplayOrder = v.DisplayOrder,
                   Language = v.Language,
                   Title = v.Title,
                   XmlPath = v.XmlPath,
                   Description = v.Description,
                   Link = v.Link,
                   ChannelCategoryMapId = v.ChannelCategoryMapId
               });
            var model = new NewsListResponse { SearchResult = result, TotalCount = response.Total };

            return model;
        }    
    }
}
