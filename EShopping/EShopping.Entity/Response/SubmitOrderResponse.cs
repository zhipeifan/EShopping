using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class SubmitOrderResponse
    {
        public ResponseStatus Status { get; set; }

        public SubmitOrderDTO responseData { get; set; }
    }

    public class SubmitOrderDTO
    {
        public decimal needPayMoney { get; set; }

        public string orderCode { get; set; }

        public WechatPayVO wechatPayVO { get; set; }
    }

    public class WechatPayVO
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        public string partnerId { get; set; }

        /// <summary>
        /// 预付款Id
        /// </summary>
        public string prepayId { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string nonceStr { get; set; }

        public string AppId { get; set; }

        public string OrderCode { get; set; }

        public string Package { get; set; }

        public int PayType { get; set; }

    }
}


