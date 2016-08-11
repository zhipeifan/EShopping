using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
   public class QueryShoppingCartListResponse
    {
       public List<QueryShoppingCarProduct> responseData { get; set; }
    }

    public class QueryShoppingCarProduct{

        public long createTime{get;set;}

        public int buyCount { get; set; }

        public ProductDTO productVO { get; set; }
    }
}
