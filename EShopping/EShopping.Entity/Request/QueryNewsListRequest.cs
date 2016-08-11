using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    /// <summary>
    /// 查询最新活动列表(小编说)
    /// </summary>
    public class QueryNewsListRequest
    {
        /// <summary>
        ///  0:小编说 ; 1:最新活动
        /// </summary>
        public int type { get; set; }
    }

}
