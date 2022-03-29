using Microsoft.AspNetCore.Mvc;
using News.App.SearchManager.Infrastructure.Request;
using News.App.SearchManager.Manager.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.App.SearchManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ElasticController : ControllerBase
    {
        private readonly INewsManager _newsManager;
        private readonly IElasticSearchManager _elasticSearchManager;
        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="NewsManager"></param>
        /// <param name="elasticSearchManager"></param>
        public ElasticController(INewsManager NewsManager, IElasticSearchManager elasticSearchManager)
        {
            _newsManager = NewsManager;
            _elasticSearchManager = elasticSearchManager;
        }

        /// <summary>
        /// CreateIndexAsync
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateIndexAsync([FromQuery] string indexName)
        {
            var result = _newsManager.CreateIndexAsync(indexName);
            return StatusCode(result ? 201 : 500);
        }

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public ActionResult AddAsync([FromBody] AddNewsRequest request)
        {
            _newsManager.AddAsync(request);

            return Ok();
        }

        [HttpGet("health-check")]
        public ActionResult HealtCheck()
        {
            var status = _elasticSearchManager.HealtCheck();

            return Ok();
        }
    }
}
