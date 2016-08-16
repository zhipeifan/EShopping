using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
   public class ExchangeIntegralRequest
    {
       public int userId{get;set;}

       public decimal exchangeMoney{get;set;}

       public int exchangeIntegral{get;set;}
    }
}
