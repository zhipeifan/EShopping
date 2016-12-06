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
using WeChatPayCommon;
using WeChatPayCommon.PayCommon;
using EShopping.BusinessService.SelectProduct;

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
                //foreach (var item in list)
                //{
                //    item.TotalPrice = item.product.productPrice * item.BuyNum;
                //    if (item.product.Id == id && item.product.spellbuyproductId == spellbuyProductId)
                //        item.IsChecked = true;
                //}
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
                    if (products.ContainsKey(InitKey(x.product.Id, x.product.spellbuyproductId)))
                    {
                        x.product = products[InitKey(x.product.Id, x.product.spellbuyproductId)].product;
                    }
                });

                return View("ShoppingList", dto);
            }
            string content = "";
            if(selectedProduct!=null&&selectedProduct.Count>0)
            {
                List<BuyProductVOs> shoppingProducts = new List<BuyProductVOs>();
                selectedProduct.ForEach(x =>
                {
                    string key=InitKey(x.product.Id,x.product.spellbuyproductId);
                    if(!products.ContainsKey(key))
                        return;

                    var item=products[key];

                    if(string.IsNullOrEmpty(content))
                    {
                        content=item.product.productName;
                    }
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
                   var responseData= ShoppingCarService.CreateOrder(UserId, GetAddressIP(), shoppingProducts,appId,TradeTypeEnum.NATIVE,UserInfo.weixinOpenId);
                   var prepayDto = PayHelper.UnifiedOrder(UserInfo.weixinOpenId, responseData.orderCode, GetAddressIP(), responseData.needPayMoney, content);
                   prepayDto.PayType = 2;
                   return View("DoRecharge", prepayDto);
                }
                else
                {
                    //todo 做提示
                    return RedirectToAction("ShoppingList");
                }

            }

           return null;
        }

        public ActionResult PayOrder()
        {
            decimal money = decimal.MinValue;
            if(!string.IsNullOrEmpty(Request.Form["inum"]))
            {
               decimal.TryParse(Request.Form["inum"],out money);
            }

            if (!string.IsNullOrEmpty(Request.Form["moneyTxt"]))
            {
                decimal.TryParse(Request.Form["moneyTxt"], out money);
            }

            if (money <= 0)
                return View("PayFor");

            var prepayDto = PayHelper.UnifiedOrder(UserInfo.weixinOpenId, "KLZ"+DateTime.Now.ToString("yyyyMMddHHmmss"), GetAddressIP(), money,"充值卡乐哲"+money+"元");
            prepayDto.PayType = 1;
            return View("DoRecharge", prepayDto);
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
                    string key = InitKey(x.product.Id, x.product.spellbuyproductId);
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
            string key = InitKey(id, spellBuyProductId);
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

          //  OperatShoppingCar(id,spellBuyProductId,true);

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



        #region "购物车相关"

        /// <summary>
        ///  添加商品到购物车
        /// </summary>
        /// <param name="id"></param>
        /// <param name="spellBuyProductId"></param>
        public int AddProductItemToCar(int id, int spellBuyProductId)
        {
            string key = InitKey(id, spellBuyProductId);
            var carDicItems = LoadShoppingCar();
            if (carDicItems.ContainsKey(key))
            {
                carDicItems[key].BuyNum++;
                carDicItems[key].TotalPrice = carDicItems[key].product.singlePrice * carDicItems[key].BuyNum;
            }
            else
            {
                var item = ProductService.LoadProductDetail(id, spellBuyProductId);
                if (item != null)
                {
                    carDicItems.Add(key, new ShoppingCarDTO
                    {
                        IsChecked = true,
                        BuyNum = 1,
                        product = item,
                        TotalPrice = item.singlePrice,
                        UserName = UserInfo.userName
                    });
                }
            }

            return carDicItems.Keys.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="spellBuyProductId"></param>
        /// <param name="BuyNum"></param>
        /// <param name="operateTpye"></param>
        public void SetProuctItemNum(int id, int spellBuyProductId,int BuyNum,int operateTpye)
        {
            string key = InitKey(id,spellBuyProductId);

            var carDics = LoadShoppingCar();

            if(carDics.ContainsKey(key))
            {
                var item = carDics[key];
                
                int stockNum=item.product.spellbuyLimit-item.product.spellbuyCount;
                switch (operateTpye)
                {
                    case -1://减少一件
                        if (item.BuyNum - 1 >= 0)
                        {
                            item.BuyNum--;
                        }
                        break;
                    case 1://增加一件
                        if (item.BuyNum + 1 <= stockNum)
                        {
                            item.BuyNum++;
                        }
                        break;
                    case 2://包尾
                        item.BuyNum = stockNum;
                        item.IsBuyAll = true;
                        break;
                    case -2://不包尾
                        item.BuyNum = 1;
                        item.IsBuyAll = false;
                        break;
                    default:

                        if(BuyNum>stockNum)
                        {
                            item.BuyNum = stockNum;
                        }
                        else
                        {
                            item.BuyNum = BuyNum;
                        }
                        break;
                }

                item.TotalPrice = item.product.singlePrice * item.BuyNum;
                item.product.spellbuyCount = item.product.spellbuyCount + item.BuyNum;

                carDics[key] = item;
            }           
        }





       

     
        #endregion
    }
}