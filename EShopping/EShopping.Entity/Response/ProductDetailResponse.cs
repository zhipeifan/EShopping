using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class ProductDetailResponse
    {
        public ResponseStatus Status { get; set; }

        public ProductDTO ResponseData { get; set; }
    }
}
