using Nest;
using News.App.SearchManager.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Data
{
    public class ElasticQueryCreater : IElasticQueryCreater
    {
        private readonly IElasticClientCreater _elasticClient;

        public ElasticQueryCreater(IElasticClientCreater elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<ISearchResponse<Infrastructure.Model.News>> SearchAsync(Infrastructure.Request.SearchRequest request)
        {
            var client = _elasticClient.GetClient(new Uri(request.EndPoint));

            var response = await client.SearchAsync<Infrastructure.Model.News>(s =>
            {
                s.Index(request.Index);
                s.Size(request.Size);
                s.From(0);
                s.Query(q =>
                            q.Bool(t => t.Must(q => q
                                                .MultiMatch(m => m.Query(request.Query)
                                                     .Analyzer("search_analyzer")
                                                     .Type(TextQueryType.BestFields)
                                                     .TieBreaker(0.2)
                                                     .Operator(Operator.And))
                                        )
                            ));
                return s;
            });

            return response;
        }

        #region private



        #endregion private
    }
}
