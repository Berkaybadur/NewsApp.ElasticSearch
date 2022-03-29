using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Manager.ElasticSearchOptions.Configurations
{
    public interface IElasticSearchConfigration
    {
        /// <summary>
        ///
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        ///
        /// </summary>
        string AuthUserName { get; }

        /// <summary>
        ///
        /// </summary>
        string AuthPassWord { get; }
    }
}
