using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class QueryMyBuyListResponse
    {
        public QueryBuyListDTO responseData { get; set; }
    }

    public class QueryBuyListDTO
    {
        public List<BuyProductDTO> productVOs { get; set; }

    }
}
