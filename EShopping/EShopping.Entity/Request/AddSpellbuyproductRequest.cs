using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    /// <summary>
    /// 创建秘密团
    /// </summary>
    public class AddSpellbuyproductRequest
    {
        public int productId { get; set; }

        public int userId { get; set; }

        /// <summary>
        /// 秘密团/闺蜜团：1；基友团：2
        /// </summary>
        public int privateType1Or2 { get; set; }
    }
}
