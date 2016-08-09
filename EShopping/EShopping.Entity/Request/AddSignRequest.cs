using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
   public class AddSignRequest
    {
       public int userId { get; set; }

       public int signIntegral { get; set; }

       public int signCount { get; set; }

       public long signLastTime { get; set; }

       public int redPacket { get; set; }
    }
}
