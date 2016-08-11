using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class AttendUserDTO
    {
        public long id { get; set; }

        // 购买总消费
        public double moneyCount { get; set; }

        // 购买总次数
        public int buyCount { get; set; }

        public long userId { get; set; }
        public string userName { get; set; }
        public string ipAddress { get; set; }

        public string faceImg { get; set; }

        public long buyTime { get; set; }
    }
}
