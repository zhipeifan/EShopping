using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    //public class SubmitOrderRequest
    //{
    //    public string payload;

    //}

    public class OrderRequest
    {
        public int userId;
        public string ipAddress { get; set; }
        public List<BuyProductVOs> buyProductVOs;

        public string appId { get; set; }

        /// <summary>
        /// JSAPI--公众号支付、NATIVE--原生扫码支付、APP--app支付
        /// </summary>
        public string tradeType{get;set;}

        public string openId { get; set; }

    }

    public class BuyProductVOs
    {
        public int spellbuyproductId;
        public int buyCount;
        public bool isBuyAll;
    }
}
