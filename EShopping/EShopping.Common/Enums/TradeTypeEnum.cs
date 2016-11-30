using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Common.Enums
{
    /// <summary>
    /// 支付平台
    /// </summary>
    public enum TradeTypeEnum
    {
        /// <summary>
        /// 公众号支付
        /// </summary>
        JSAPI,
        /// <summary>
        /// 原生扫码支付
        /// </summary>
        NATIVE,
        /// <summary>
        /// app支付

        /// </summary>
         APP
    }
}
