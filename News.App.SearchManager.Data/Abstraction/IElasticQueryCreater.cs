using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Data.Abstraction
{
    public interface IElasticQueryCreater
    {
        Task<ISearchResponse<Infrastructure.Model.News>> SearchAsync(Infrastructure.Request.SearchRequest request);
    }
}
