using EShopping.Entity.UIDTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.UIDTO
{
    public class ShoppingUIDTO
    {
        public int SpellbuyproductId { get; set; }

        public int BuyCount { get; set; }

        public ShoppingOperatTypeEnum ShoppingOperatType { get; set; }
    }
}
