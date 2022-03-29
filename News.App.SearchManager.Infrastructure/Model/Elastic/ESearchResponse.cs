using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.App.SearchManager.Infrastructure.Model.Elastic
{
    public class ESearchResponse<T>
    {
        public bool IsValid { get; set; }
        public object Result { get; set; }
    }
}
