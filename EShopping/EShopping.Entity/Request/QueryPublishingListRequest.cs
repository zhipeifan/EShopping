using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class QueryPublishingListRequest
    {
        public string payload { get; set; }
    }

    public class QueryPublishingListRequestDTO
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
