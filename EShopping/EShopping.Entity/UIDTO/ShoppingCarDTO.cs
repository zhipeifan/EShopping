using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.UIDTO
{
    public class ShoppingCarDTO
    {
        public ProductDTO product { get; set; }

        public string UserName { get; set; }

        public int BuyNum { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsBuyAll { get; set; }

        public bool IsChecked { get; set; }

        /// <summary>
        /// 操作类型
        /// -1=减少一件
        /// 1=添加一件
        /// 0=包尾
        /// </summary>
        public int OperationType { get; set; }
    }
}
