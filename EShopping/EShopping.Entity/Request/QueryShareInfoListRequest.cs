using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class QueryShareInfoListRequest
    {
        public int pageSize { get; set; }


        public int currentPage { get; set; }

        /// <summary>
        /// /1：详情中的 查看晒单。2：我的晒单
        /// </summary>
        public int byDetailOrMy { get; set; }

        public int productId { get; set; }

        public int userId { get; set; }

        public bool isShowAll { get; set; }
    }
}
