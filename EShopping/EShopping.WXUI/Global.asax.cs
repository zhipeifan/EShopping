using EShopping.BusinessService.SelectProduct;
using EShopping.BusinessService.ShoppingCar;
using EShopping.Common;
using EShopping.Entity.Response.DTO;
using EShopping.Entity.UIDTO;
using EShopping.Entity.UIDTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace EShopping.WXUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ApplicationLog.Error("system error", Server.GetLastError()
                                      .GetBaseException().Message);

        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                if (Session["ShoppingCar"] != null)
                {
                    var newShoppingCarItems = (Dictionary<string, ShoppingCarDTO>)Session["ShoppingCar"];
                    if (newShoppingCarItems != null && newShoppingCarItems.Count > 0)
                    {
                        var items = newShoppingCarItems.Values.ToList();
                        var userName = items.FirstOrDefault().UserName;
                        if (string.IsNullOrEmpty(userName))
                            return;

                        var list = ShoppingCarService.LoadShoppingList(userName);
                        if(list!=null&&list.Count>0)
                        {
                            var deleteProducts = list.Select(x => new ShoppingUIDTO
                            {
                                BuyCount = x.buyCount,
                                SpellbuyproductId = x.productVO.spellbuyproductId,
                                ShoppingOperatType = ShoppingOperatTypeEnum.Delete
                            }).ToList();
                            ShoppingCarService.BatchModifyShoppingCar(deleteProducts, userName, ShoppingOperatTypeEnum.Delete);//删除原有购物车商品
                        }

                        var addProducts = items.Select(x => new ShoppingUIDTO
                        {
                            BuyCount = x.BuyNum,
                            SpellbuyproductId = x.product.spellbuyproductId,
                            ShoppingOperatType = ShoppingOperatTypeEnum.Add
                        }).ToList();
                        ShoppingCarService.BatchModifyShoppingCar(addProducts, userName, ShoppingOperatTypeEnum.Add);//同步购物车到服务器
                    }
                }
               
            }catch(Exception ex)
            {
                ApplicationLog.Error("Session过期同步数据到服务器 Error",ex.Message);
            }
        }


        /// <summary>
        /// 获取当前用户登录信息
        /// </summary>
        /// <returns></returns>
        public UserDTO GetUser()
        {
            UserDTO userInfo = null;
            if (!User.Identity.IsAuthenticated)
            {
                string strUserData = ((FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket.UserData;
                userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(strUserData);
            }

            return userInfo;
        }
    }
}
