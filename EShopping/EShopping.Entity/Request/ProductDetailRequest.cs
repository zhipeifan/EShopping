using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
   public class ProductDetailRequest
    {
       public string payload { get; set; }
    }

   public class payload
   {
       public int productId { get; set; }

       public int spellbuyproductId { get; set; }

       public int userId { get; set; }
   }
}
