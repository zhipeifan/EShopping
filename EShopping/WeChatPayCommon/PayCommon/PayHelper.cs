using EShopping.Entity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WX_TennisAssociation.Common;
using WXPay;

namespace WeChatPayCommon.PayCommon
{
    public class PayHelper
    {
        public static WechatPayVO UnifiedOrder(string openId, string orderCode, string ip, decimal money)
        {

            money = 1;

            string prepay_id = "";
            try
            {
                WXpayUtil wXpayUtil = new WXpayUtil();
                string paySignKey = WXPayConfig.APIKEY;
                string AppSecret = WXPayConfig.APPSECRET;
                string mch_id = WXPayConfig.MCHID;
                string appId = WXPayConfig.APPID;

                WeixinApiDispatch wxApiDispatch = new WeixinApiDispatch();
                string accessToken = wxApiDispatch.GetAccessToken(appId, AppSecret);

                System.Diagnostics.Debug.WriteLine("accessToken值: ");
                System.Diagnostics.Debug.WriteLine(accessToken);

                string strOpenid = openId;
                UserJson userJson = wxApiDispatch.GetUserDetail(accessToken, strOpenid, "zh_CN");

                UnifiedOrder order = new UnifiedOrder();
                order.appid = appId;
                order.attach = "vinson";
                order.body = "1" + "卡币";
                order.device_info = "";
                order.mch_id = mch_id;
                order.nonce_str = WXpayUtil.getNoncestr();
                order.notify_url = "http://www.kalezhe.com/ShoppingCar/pay";
                order.openid = userJson.openid;
                order.out_trade_no = orderCode;
                order.trade_type = "JSAPI";
                order.spbill_create_ip = ip;
                order.total_fee =Convert.ToInt32(money * 100);

                prepay_id = wXpayUtil.getPrepay_id(order, paySignKey);

               string timeStamp = WXpayUtil.getTimestamp();
                string nonceStr = WXpayUtil.getNoncestr();

                SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
                sParams.Add("appId", appId);
                sParams.Add("timeStamp", timeStamp);
                sParams.Add("nonceStr", nonceStr);
                sParams.Add("package", "prepay_id=" + prepay_id);
                sParams.Add("signType", "MD5");
               // paySign = wXpayUtil.getsign(sParams, paySignKey);

                WechatPayVO payDto = new WechatPayVO();

                payDto.partnerId = WXPayConfig.MCHID;
                payDto.nonceStr = WXpayUtil.getNoncestr();
                payDto.sign = wXpayUtil.getsign(sParams, paySignKey);
                payDto.timestamp = timeStamp;
                payDto.prepayId = prepay_id;
                payDto.Package = "prepay_id=" + prepay_id;

                payDto.AppId = appId;
                payDto.OrderCode = orderCode;


                //var ticket = AccessTokenContainer.GetJsApiTicket(prepayDto.AppId);
                //var signature = JSSDKHelper.GetSignature(ticket, prepayDto.nonceStr, prepayDto.timestamp, Request.Url.AbsoluteUri);
                //prepayDto.Signature = signature;


                return payDto;
            }
            catch (Exception ex)
            {
               // Response.Write(ex.ToString());
               // return View();
            }

            return null;
        }



        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
    }
}
