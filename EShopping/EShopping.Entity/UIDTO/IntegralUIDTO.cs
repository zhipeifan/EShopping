using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.UIDTO
{
    public class IntegralUIDTO
    {
        /// <summary>
        /// 个人总共可用积分
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        public int UseIntegral { get; set; }
    }
}
