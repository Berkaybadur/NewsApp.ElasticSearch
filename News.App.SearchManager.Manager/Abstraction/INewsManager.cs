using Nest;
using News.App.SearchManager.Infrastructure.Request;
using News.App.SearchManager.Infrastructure.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Manager.Abstraction
{
   public interface INewsManager
    {
        Task<NewsListResponse> SearchAsync(Infrastructure.Request.SearchRequest searchRequest);
        bool CreateIndexAsync(string indexName);
        void AddAsync(AddNewsRequest request);
    }
}
