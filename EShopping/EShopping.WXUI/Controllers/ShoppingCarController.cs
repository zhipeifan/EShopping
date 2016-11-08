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
using WXPay;
using WX_TennisAssociation.Common;

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
            ViewBag.SelectEnum = (int)FloolterMenu.MyShopping;


            return View();
        }

        public ActionResult PaySuccess()
        {
            return View();
        }

        public ActionResult DoRecharge(SubmitOrderDTO border, string code = "", string state = "")
        {
            if (border != null && border.needPayMoney > 0)
            {
                // var userId = Guid.Parse(User.Identity.Name);
                // var user = UserManager.GetUserById(userId);

                ViewBag.WechatPay = border.needPayMoney;


                string prepay_id = "";
                string timeStamp = "";
                string nonceStr = "";
                string paySign = "";

                try
                {
                    WXpayUtil wXpayUtil = new WXpayUtil();
                    string paySignKey = ConfigurationManager.AppSettings["paySignKey"].ToString();
                    string AppSecret = ConfigurationManager.AppSettings["secret"].ToString();
                    string mch_id = ConfigurationManager.AppSettings["mch_id"].ToString();
                    appId = ConfigurationManager.AppSettings["AppId"].ToString();

                    WeixinApiDispatch wxApiDispatch = new WeixinApiDispatch();
                    string accessToken = wxApiDispatch.GetAccessToken(appId, AppSecret);

                    System.Diagnostics.Debug.WriteLine("accessToken值: ");
                    System.Diagnostics.Debug.WriteLine(accessToken);

                    string strOpenid = UserInfo.weixinOpenId;
                    UserJson userJson = wxApiDispatch.GetUserDetail(accessToken, strOpenid, "zh_CN");

                    UnifiedOrder order = new UnifiedOrder();
                    order.appid = appId;
                    order.attach = "vinson";
                    order.body = "12" + "拍币";
                    order.device_info = "";
                    order.mch_id = mch_id;
                    order.nonce_str = WXpayUtil.getNoncestr();
                    order.notify_url = "http://abelxu19.imwork.net/jsapi/pay.aspx";
                    order.openid = userJson.openid;
                    order.out_trade_no = border.orderCode;
                    order.trade_type = "JSAPI";
                    order.spbill_create_ip = GetAddressIP();
                    order.total_fee = Convert.ToInt32(border.needPayMoney * 100);

                    prepay_id = wXpayUtil.getPrepay_id(order, paySignKey);
                    timeStamp = WXpayUtil.getTimestamp();
                    nonceStr = WXpayUtil.getNoncestr();

                    SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
                    sParams.Add("appId", appId);
                    sParams.Add("timeStamp", timeStamp);
                    sParams.Add("nonceStr", nonceStr);
                    sParams.Add("package", "prepay_id=" + prepay_id);
                    sParams.Add("signType", "MD5");
                    paySign = wXpayUtil.getsign(sParams, paySignKey);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                    return View();
                }

                Response.Redirect("http://abelxu19.imwork.net/jsapi/pay.aspx?showwxpaytitle=1&appId=" + appId +
                    "&timeStamp=" + timeStamp +
                    "&nonceStr=" + nonceStr +
                    "&prepay_id=" + prepay_id +
                    "&signType=MD5&paySign=" + paySign +
                    "&OrderID=" + border.orderCode);
            }
            return View();
        }
        

        public ActionResult OrderPayFor(string orderCode, string code = "", string state = "")
        {
            return View();
        }


        public ActionResult WeChatPay()
        {
            return View();
        }
	}
}