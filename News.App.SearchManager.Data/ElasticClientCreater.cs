using Nest;
using News.App.SearchManager.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Data
{
    public class ElasticClientCreater : IElasticClientCreater
    {

        public IElasticClient GetClient(Uri endPoint)
        {
            var setting = new ConnectionSettings(endPoint);

#if DEBUG
            setting.EnableDebugMode();
            setting.PrettyJson();
#endif
            return new ElasticClient(setting);
        }
    }
}
