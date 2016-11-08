using EShopping.Entity.Response;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using System.Xml.Linq;

namespace EShopping.WXUI.Controllers
{
    public class TestShoppingCarController : Controller
    {
        private static string appId = ConfigurationManager.AppSettings["WechatAppId"];
        private static string appSecret = ConfigurationManager.AppSettings["WechatAppSecret"];
        private static string hostName = ConfigurationManager.AppSettings["UploadPrefix"];

        //
        // GET: /TestShoppingCar/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DoRecharge(SubmitOrderDTO order, string code = "", string state = "")
        {
            if (order != null && order.needPayMoney > 0)
            {
                // var userId = Guid.Parse(User.Identity.Name);
                // var user = UserManager.GetUserById(userId);

                ViewBag.WechatPay = order.needPayMoney;

                try
                {
                    if (string.IsNullOrEmpty(code))
                    {
                        var url = OAuthApi.GetAuthorizeUrl(appId, hostName + Url.Action("DoRecharge", new { orderId = order.orderCode }), "PAY", OAuthScope.snsapi_userinfo);
                        return Redirect(url);
                    }

                    var openIdResponse = OAuthApi.GetAccessToken(appId, appSecret, code);
                    var openIdResponseString = openIdResponse.ConvertEntityToXmlString();
                    var openId = openIdResponse.openid;

                    var mchId = ConfigurationManager.AppSettings["WechatMchId"];
                    var partnerKey = ConfigurationManager.AppSettings["WechatPartnerKey"];

                    RequestHandler commonPaymentRequest = new RequestHandler(null);
                    commonPaymentRequest.Init();
                    commonPaymentRequest.SetParameter("appid", appId);
                    commonPaymentRequest.SetParameter("mch_id", mchId);
                    commonPaymentRequest.SetParameter("device_info", "WEB");
                    commonPaymentRequest.SetParameter("nonce_str", TenPayV3Util.GetNoncestr());
                    commonPaymentRequest.SetParameter("body", "美国进口（Starbucks）星巴克咖啡豆 ");
                    commonPaymentRequest.SetParameter("attach", string.Format("couponPay={0}|accountPay={1}", 0, 0));
                    commonPaymentRequest.SetParameter("out_trade_no", order.orderCode);
                    // commonPaymentRequest.SetParameter("total_fee", ((int)(order.needPayMoney * 100)).ToString());
                    commonPaymentRequest.SetParameter("total_fee", "1");
                    commonPaymentRequest.SetParameter("spbill_create_ip", Request.UserHostAddress);
                    commonPaymentRequest.SetParameter("notify_url", hostName + Url.Action("WechatPayCallback"));
                    commonPaymentRequest.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString());
                    commonPaymentRequest.SetParameter("openid", openId);

                    string sign = commonPaymentRequest.CreateMd5Sign("key", partnerKey);
                    commonPaymentRequest.SetParameter("sign", sign);

                    string data = commonPaymentRequest.ParseXML();

                    System.IO.File.AppendAllText(Server.MapPath("~/DoRecharge.txt"), data);

                    var unifiedOrderResponseString = TenPayV3.Unifiedorder(data);

                    var res = XDocument.Parse(unifiedOrderResponseString);
                    var prepayId = res.Element("xml").Element("prepay_id").Value;

                    var timeStamp = JSSDKHelper.GetTimestamp();
                    var nonceStr = JSSDKHelper.GetNoncestr();
                    var ticket = AccessTokenContainer.GetJsApiTicket(appId);
                    var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timeStamp, Request.Url.AbsoluteUri);

                    RequestHandler paySignReqHandler = new RequestHandler(null);
                    paySignReqHandler.SetParameter("appId", appId);
                    paySignReqHandler.SetParameter("timeStamp", timeStamp);
                    paySignReqHandler.SetParameter("nonceStr", nonceStr);
                    paySignReqHandler.SetParameter("package", string.Format("prepay_id={0}", prepayId));
                    paySignReqHandler.SetParameter("signType", "MD5");
                    var paySign = paySignReqHandler.CreateMd5Sign("key", partnerKey);

                    ViewBag.AppId = appId;
                    ViewBag.TimeStamp = timeStamp;
                    ViewBag.NonceStr = nonceStr;
                    ViewBag.Ticket = ticket;
                    ViewBag.Signature = signature;
                    ViewBag.PrepayId = prepayId;
                    ViewBag.PaySign = paySign;
                    ViewBag.UnifiedOrderResponseString = unifiedOrderResponseString;
                    ViewBag.OpenIdResponseString = openIdResponseString;
                }
                catch
                {
                    // Hmmm.... don't know what to do now. The code above sucks.
                }
            }
            return View();
        }
	}
}