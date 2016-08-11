using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class QueryMyBuyListRequest
    {
        public int userId { get; set; }

        public int status { get; set; }

        public int currentPage { get; set; }

        public int pageSize { get; set; }
    }
}
