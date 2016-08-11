using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class HandleShoppingCartRequest
    {

        public string payload { get; set; }

        public string token { get; set; }

    }

    public class ShoppingProductOperat
    {

        public string userName { get; set; }

        public int updateOrDelete { get; set; }

        public List<ShoppingProduct> handleShoppingCartVOs { get; set; }
    }

    public class ShoppingProduct
    {
        public int spellbuyproductId { get; set; }

        public int buyCount { get; set; }
    }
}
