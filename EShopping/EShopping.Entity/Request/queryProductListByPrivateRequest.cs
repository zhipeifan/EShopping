using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    /// <summary>
    /// 秘密团
    /// </summary>
   public class QueryProductListByPrivateRequest
    {

       public int userId { get; set; }

       /// <summary>
       ///  秘密团/闺蜜团：1；基友团：2
       /// </summary>
       public int privateType1Or2 { get; set; }

       public int currentPage { get; set; }

       public int pageSize { get; set; }

    }
}
