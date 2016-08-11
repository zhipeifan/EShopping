using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    /// <summary>
    /// 金榜排行
    /// </summary>
    public class QueryUserByIntegralTopResponse
    {
        public List<UserDTO> responseData { get; set; }
    }
}
