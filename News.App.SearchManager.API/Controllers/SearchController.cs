using Microsoft.AspNetCore.Mvc;
using News.App.SearchManager.Infrastructure.Request;
using News.App.SearchManager.Infrastructure.Response;
using News.App.SearchManager.Manager.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.App.SearchManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly INewsManager _newsManager;

        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="NewsManager"></param>
        public SearchController(INewsManager NewsManager)
        {
            _newsManager = NewsManager;
        }
        /// <summary>
        /// Generic Search for News
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet("News")]
        public async Task<NewsListResponse> Get([FromQuery] SearchRequest searchRequest)
        {
            return await _newsManager.SearchAsync(searchRequest);
        }
    }
}
