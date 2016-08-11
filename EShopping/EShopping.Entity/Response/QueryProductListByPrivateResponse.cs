using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class QueryProductListByPrivateResponse
    {
    //public class ResponseStatus responseStatus{get;set;}

        public PrivateResponseStatus responseStatus { get; set; }

        public QueryProductListByPrivateResponseData responseData { get; set; } 
    }

    public class QueryProductListByPrivateResponseData
    {
        public string systemTime { get; set; }
        public List<ProductDTO> productVOs { get; set; }
    }
}
