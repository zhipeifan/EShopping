using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class QueryPublishingListResponse
    {
        public QueryProductListByTypeResponseData responseData { get; set; }
    }

    public class QueryPublishingListResponseData
    {
        public List<ProductDTO> productVOs { get; set; }
    }
}
