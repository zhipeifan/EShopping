using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class QueryRegionRequest
    {
    }

    public class QueryRegionResponse
    {
        public List<ProvoinceDTO> provinceVOs { get; set; }
    }
}
