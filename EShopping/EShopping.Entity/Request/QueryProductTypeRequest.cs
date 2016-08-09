using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    /// <summary>
    /// 获取商品类型
    /// </summary>
    public class QueryProductTypeRequest
    {
    }

    public class QueryProductTypeResponse
    {
        public List<ProductTypeDTO> responseData { get; set; }
    }
}
