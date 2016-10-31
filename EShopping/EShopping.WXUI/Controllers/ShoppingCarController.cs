using EShopping.BusinessService.ShoppingCar;
using EShopping.Common.Enums;
using EShopping.Entity.Request;
using EShopping.Entity.Response;
using EShopping.Entity.UIDTO;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.TenPayLibV3;
using System.Xml.Linq;
using Senparc.Weixin.MP.CommonAPIs;

namespace EShopping.WXUI.Controllers
{
    public class ShoppingCarController : BaseController
    {
        private static string appId = ConfigurationManager.AppSettings["WechatAppId"];
        private static string appSecret = ConfigurationManager.AppSettings["WechatAppSecret"];
        private static string hostName = ConfigurationManager.AppSettings["UploadPrefix"];

        public ShoppingCarController()
        {
            ViewBag.SelectEnum = (int)FloolterMenu.ShoppingCar;
        }
        //
        // GET: /ShoppingCar/
        public ActionResult ShoppingList(int id=0,int spellbuyProductId=0)
        {
            List<ShoppingCarDTO> list = new List<ShoppingCarDTO>();
            var products = LoadShoppingCar();
            if (products.Values != null && products.Values.Count > 0)
            {
                list = products.Values.ToList();
                foreach (var item in list)
                {
                    item.TotalPrice = item.product.productPrice * item.BuyNum;
                    if (item.product.Id == id && item.product.spellbuyproductId == spellbuyProductId)
                        item.IsChecked = true;
                }
            }

            ViewBag.ShoppingCarCount = products.Count();
            return View(list);
        }

        [HttpPost]
        public ActionResult CreateShoppingOrder(List<ShoppingCarDTO> dto)
        {
            if(!User.Identity.IsAuthenticated)
            {
                WeChatLogin("","");
            }

            var products = LoadShoppingCar();

            var selectedProduct = dto.Where(x => x.IsChecked).ToList();

            if(selectedProduct.Count==0)
            {
                ModelState.AddModelError("ShoppingCarErro","亲，至少选择一个商品结算哦！");

                dto.ForEach(x =>
                {
                    if(products.ContainsKey(InintKey(x.product.Id, x.product.spellbuyproductId)))
                    {
                        x.product = products[InintKey(x.product.Id, x.product.spellbuyproductId)].product;
                    }
                });

                return View("ShoppingList", dto);
            }
            if(selectedProduct!=null&&selectedProduct.Count>0)
            {
                List<BuyProductVOs> shoppingProducts = new List<BuyProductVOs>();
                selectedProduct.ForEach(x =>
                {
                    string key=InintKey(x.product.Id,x.product.spellbuyproductId);
                    if(!products.ContainsKey(key))
                        return;

                    var item=products[key];
                    BuyProductVOs _productdto = new BuyProductVOs
                    {
                        buyCount = x.BuyNum,
                        isBuyAll = item.IsBuyAll,
                        spellbuyproductId = item.product.spellbuyproductId
                    };

                    shoppingProducts.Add(_productdto);
                });

                if (shoppingProducts != null && shoppingProducts.Count>0)
                {
                   var responseData= ShoppingCarService.CreateOrder(UserId, GetAddressIP(), shoppingProducts);
                   return View("RechargePayFor", responseData);
                }
                else
                {
                    //todo 做提示
                    return RedirectToAction("ShoppingList");
                }

            }

           return null;
        }

        [HttpPost]
        public ActionResult ChangeShoppingList(List<ShoppingCarDTO> dto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                WeChatLogin("", "");
            }

            var products = LoadShoppingCar();
            if (products != null && products.Count>0)
            {
                dto.ForEach(x =>
                {
                    string key = InintKey(x.product.Id, x.product.spellbuyproductId);
                    if (!products.ContainsKey(key))
                        return;

                    if (x.BuyNum < 0)
                        x.BuyNum = 0;

                    var item = products[key];
                    switch (x.OperationType)
                    {
                        case -1:
                            if (item.BuyNum - 1 > 0)
                            {
                                item.BuyNum--;
                                item.product.spellbuyCount--;
                            }
                            break;
                        case 1:
                            if (item.BuyNum + 1 <= item.product.spellbuyLimit)
                            {
                                item.BuyNum++;
                                item.product.spellbuyCount++;
                            }
                            break;
                        case 2:
                            item.BuyNum = (item.product.productLimit - item.product.spellbuyCount) < 0 ? 0 : item.product.productLimit - item.product.spellbuyCount + 1;
                            item.product.spellbuyCount = item.product.productLimit;
                            x.IsBuyAll = true;
                            break;
                        case -2:
                            item.product.spellbuyCount = item.product.spellbuyCount - item.BuyNum + 1;
                            item.BuyNum = 1;
                            x.IsBuyAll = false;
                            break;
                        default:
                            if (item.product.productLimit - item.product.spellbuyCount <= x.BuyNum)
                            {
                                x.BuyNum = item.product.productLimit - item.product.spellbuyCount;
                            }
                            item.product.spellbuyCount = item.product.spellbuyCount + x.BuyNum - item.BuyNum;
                            item.BuyNum = x.BuyNum;
                            break;
                    }

                    item.IsBuyAll = x.IsBuyAll;
                    item.IsChecked = x.IsChecked;
                    if (item.product.spellbuyCount > item.product.productLimit)
                    {
                        item.product.productLimit = item.product.spellbuyCount;
                    }
                });
            }

            Session["ShoppingCar"] = products;

            return RedirectToAction("ShoppingList");
        }

        public ActionResult AddProductNum(int id,int spellBuyProductId,int num)
        {
            var products = LoadShoppingCar();
            string key = InintKey(id,spellBuyProductId);
            if (products.ContainsKey(key))
            { 
                switch(num)
                {
                    case -1:
                        products[key].BuyNum--;
                        break;
                    case 1:
                        products[key].BuyNum++;
                        break;
                    case 0:
                        products.Remove(key);
                        break;
                    default:
                        products[key].BuyNum = num;
                        break;
                }
            }

            Session["ShoppingCar"] = products;

            OperatShoppingCar(id,spellBuyProductId,true);

            return RedirectToAction("ShoppingList", "ShoppingCar");
        }

        public ActionResult PayFor()
        {



            return View();
        }

        public ActionResult PaySuccess()
        {
            return View();
        }

        public ActionResult DoRecharge(SubmitOrderDTO order, string code = "", string state = "")
        {
            if(order!=null&&order.needPayMoney>0)
            {
                var userId = Guid.Parse(User.Identity.Name);
               // var user = UserManager.GetUserById(userId);

                ViewBag.WechatPay = order.needPayMoney;

                try
                {
                    if (string.IsNullOrEmpty(code))
                    {
                        return Redirect(OAuthApi.GetAuthorizeUrl(appId, hostName + Url.Action("PayOrder", new { orderId = order.orderCode }), "PAY", OAuthScope.snsapi_userinfo));
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


        public ActionResult WeChatPay()
        {
            return View();
        }
	}
}