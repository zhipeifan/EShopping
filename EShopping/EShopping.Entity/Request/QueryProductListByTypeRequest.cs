using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{

    public class QueryProductListByType
    {
        public int productTypeId { get; set; }

        public int prefecture { get; set; }

        public int searchParam { get; set; }

        public int currentPage { get; set; }

        public int pageSize { get; set; }
    }
}
