using EShopping.BusinessService.ShoppingCar;
using EShopping.Entity.Request;
using EShopping.Entity.UIDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class ShoppingCarController : BaseController
    {
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
                    ShoppingCarService.CreateOrder(UserId, GetAddressIP(), shoppingProducts);
                    return RedirectToAction("PayFor");
                }
                else
                {
                    //todo 做提示
                    return RedirectToAction("ShoppingList");
                }

            }

           return null;
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

            return RedirectToAction("ShoppingList");
        }

        public ActionResult PayFor()
        {
            return View();
        }

        public ActionResult PaySuccess()
        {
            return View();
        }
	}
}