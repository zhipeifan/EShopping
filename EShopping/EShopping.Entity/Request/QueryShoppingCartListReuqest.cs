using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class QueryShoppingCartListReuqest
    {
        public string payload { get; set; }

        public string token { get; set; }
    }

    public class ShoppingUserInfo
    {
        public string userName { get; set; }
    }
}
