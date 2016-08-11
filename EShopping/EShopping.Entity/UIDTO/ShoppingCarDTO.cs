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

        public int BuyNum { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsBuyAll { get; set; }

        public bool IsChecked { get; set; }
    }
}
