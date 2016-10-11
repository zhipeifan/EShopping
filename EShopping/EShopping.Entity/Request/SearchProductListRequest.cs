using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class SearchProductListRequest
    {
        public string payload { get; set; }
    }

    public class SearchReqeustDTO
    {

        public string keyword { get; set; }

        public int currentPage { get; set; }

        public int pageSize { get; set; }
    }
}
