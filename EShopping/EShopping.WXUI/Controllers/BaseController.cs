﻿using EShopping.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using EShopping.Entity.UIDTO;
using EShopping.Entity.Response.DTO;
using EShopping.BusinessService.SelectProduct;
using System.Web.Security;
using Senparc.Weixin.MP.AdvancedAPIs;
using System.Configuration;
using Senparc.Weixin.MP;
using System.Web.Mvc.Filters;
using EShopping.BusinessService.ShoppingCar;
using EShopping.Entity.UIDTO.Enum;
using System.Net;
using EShopping.Common;

namespace EShopping.WXUI.Controllers
{
    public class BaseController : Controller
    {
        private static string appId = ConfigurationManager.AppSettings["WechatAppId"];
        private static string appSecret = ConfigurationManager.AppSettings["WechatAppSecret"];
        private static string hostName = ConfigurationManager.AppSettings["UploadPrefix"];

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int UserId
        {
            get
            {
                string id = User.Identity.Name;
                if (string.IsNullOrEmpty(id))
                    return 0;
                return Convert.ToInt32(id);

            }
        }

        public UserDTO UserInfo
        {
            get
            {
                if(User.Identity.IsAuthenticated)
                { 
                string strUserData = ((FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket.UserData;
                var _user= Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(strUserData);
                //_user.userName = "15105149197";
                return _user;
                }
                else
                {
                    return new UserDTO();
                }
            }
        }


        public Dictionary<string, ShoppingCarDTO> LoadShoppingCar()
        {

            if (Session["ShoppingCar"] != null)
            {
                var list = (Dictionary<string, ShoppingCarDTO>)Session["ShoppingCar"];
                return list;
            }
            else
            {
                var _ShoppingCar = new Dictionary<string, ShoppingCarDTO>();

                if (UserInfo != null && !string.IsNullOrEmpty(UserInfo.userName))
                {
                   var list = ShoppingCarService.LoadShoppingList(UserInfo.userName);
                   //var list = ShoppingCarService.LoadShoppingList("15105149197");
                    if(list!=null&&list.Count>0)
                    {
                        list.ForEach(x =>
                        {
                            ShoppingCarDTO dto = new ShoppingCarDTO
                            {
                                BuyNum = x.buyCount,
                                product = x.productVO,
                                TotalPrice = x.buyCount * x.productVO.productPrice
                            };
                            _ShoppingCar.Add(InintKey(x.productVO.Id, x.productVO.spellbuyproductId), dto);
                        });
                    }
                }
                
                Session["ShoppingCar"] = _ShoppingCar;
                return _ShoppingCar;
            }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            LoadUserInfo();

            if (Session != null)
            {
                LoadShoppingCar();
            }
            ViewBag.SelectEnum = (int)FloolterMenu.Index;
            ViewBag.ShoppingCarCount = LoadShoppingCar().Count;
            if (ViewBag.PageIndex == null)
                ViewBag.PageIndex = 1;
            if (ViewBag.PageSize == null)
                ViewBag.PageSize = 10;
        }

        public ShoppingCarDTO InitShoppingCarDTO(int id, int spellBuyProductId)
        {
            var item = ProductService.LoadProductDetail(id,spellBuyProductId);
            return new ShoppingCarDTO
                  {
                      product = item,
                      BuyNum = 1
                  };
        }

        public string InintKey(int id, int spellBuyProductId)
        {
            return string.Join("_", new List<int> { id, spellBuyProductId });
        }
       
        public void ChangeFlooterEnum(FloolterMenu enumType=FloolterMenu.Index)
        {
            ViewBag.SelectEnum = (int)enumType;
        }


        #region "操作购物车，通知服务器"
        public void OperatShoppingCar(int id, int spellBuyProductId,bool isDelete)
        {
            var ShoppingCar = LoadShoppingCar();
            var key = InintKey(id,spellBuyProductId);
            if (isDelete)
            {
                //操作购物车，修改购买商品数量
                ShoppingCarService.OperatShoppingProduct(new ShoppingUIDTO
                {
                    BuyCount = 1,
                    SpellbuyproductId = spellBuyProductId,
                    ShoppingOperatType = ShoppingOperatTypeEnum.Delete
                }, UserInfo == null ? "" : UserInfo.userName);
            }
            if (ShoppingCar.ContainsKey(key))
            {
                ShoppingCarService.OperatShoppingProduct(new ShoppingUIDTO
                {
                    BuyCount = ShoppingCar[key].BuyNum,
                    SpellbuyproductId = spellBuyProductId,
                    ShoppingOperatType = ShoppingOperatTypeEnum.Add
                }, UserInfo == null ? "" : UserInfo.userName);
            }
        }

        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="id"></param>
        /// <param name="spellBuyProductId"></param>
        /// <returns></returns>
        public int ShoppingCarList(int id, int spellBuyProductId)
        {
            string key = InintKey(id, spellBuyProductId);
            var ShoppingCar = LoadShoppingCar();
            if (ShoppingCar == null)
            {
                ShoppingCar = new Dictionary<string, ShoppingCarDTO>();
                ShoppingCar.Add(key, InitShoppingCarDTO(id, spellBuyProductId));
                //调用购物车，添加商品到购物车
                OperatShoppingCar(id, spellBuyProductId, false);
            }
            else
            {
                if (ShoppingCar.ContainsKey(key))
                {
                    var _item = ShoppingCar[key];
                    _item.BuyNum++;

                    OperatShoppingCar(id, spellBuyProductId, true);
                }
                else
                {
                    ShoppingCar.Add(key, InitShoppingCarDTO(id, spellBuyProductId));
                    //操作购物车，修改购买商品数量
                    OperatShoppingCar(id, spellBuyProductId, false);
                }
            }
            Session["ShoppingCar"] = ShoppingCar;
            return ShoppingCar.Count;
        }

        public void LoadShoppingCarFromServer()
        {

        }

        #endregion

        #region "Login"
         [AllowAnonymous]
        public UserDTO LoadUserInfo()
        {
            //return new UserDTO();
            UserDTO userInfo = null;
            var isLogin = User.Identity.IsAuthenticated;
            //ApplicationLog.DebugInfo("IsAuthenticated:" + isLogin);
            if (!isLogin)
            {
              GetWeChatUserName("", "");
               //  WeChatLogin("", "");
                //string strUserData = ((FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket.UserData;
                //userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(strUserData);
            }
            else
            {
                string strUserData = ((FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket.UserData;
                userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(strUserData);
            }


            return userInfo;


      
        }


        [AllowAnonymous]
        public ActionResult WeChatLogin(string code, string state)
        {
            string url = Request.Url.OriginalString;
            if (string.IsNullOrEmpty(code))
                return Redirect(OAuthApi.GetAuthorizeUrl(appId, url, "LOGIN", OAuthScope.snsapi_userinfo));

            var openIdResponse = OAuthApi.GetAccessToken(appId, appSecret, code);
            var wechatUser = OAuthApi.GetUserInfo(openIdResponse.access_token, openIdResponse.openid);

            var userinfo = new UserDTO
            {
                weixinOpenId = wechatUser.openid,
                faceImg = wechatUser.headimgurl,
                sex = wechatUser.sex.ToString(),
                nickName = wechatUser.nickname
            };

            var usre = LoginService.LoginUser(userinfo);
            string _userInfo = Newtonsoft.Json.JsonConvert.SerializeObject(userinfo);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (
                1,
                userinfo.userId.ToString(),
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                false,
                _userInfo
                );
            FormsAuthentication.SetAuthCookie(userinfo.userId.ToString(),true,FormsAuthentication.FormsCookiePath);
            string enyTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enyTicket);
            cookie.HttpOnly = true;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;

            System.Web.HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
           // System.Web.HttpContext.Current.Response.

            return RedirectToAction(url);
        }

        public ActionResult GetWeChatUserName(string code, string state)
        {
            UserDTO userinfo = new UserDTO
            {
                userId = 14,
                userName ="test1",
                faceImg = "http://c.hiphotos.baidu.com/image/h%3D300/sign=9fbf2f521838534393cf8121a312b01f/e1fe9925bc315c609e3db7d185b1cb1349547760.jpg",
                 weixinOpenId="888888"
            };

             userinfo = LoginService.LoginUser(userinfo);
          //  userinfo = LoginService.LoadUserInfo(14);
          // Session["LoginSession"] = usre;

            string _userInfo = Newtonsoft.Json.JsonConvert.SerializeObject(userinfo);
         //   FormsAuthentication.SetAuthCookie(userinfo.userId.ToString(), true, FormsAuthentication.FormsCookiePath);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (
                1,
                userinfo.userId.ToString(),
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                false,
                _userInfo,
                FormsAuthentication.FormsCookiePath
                );

           // FormsIdentity identity = new FormsIdentity(ticket);
            string enyTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enyTicket);
            cookie.HttpOnly = true;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Expires = ticket.Expiration;

            System.Web.HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

            return RedirectToAction(Request.Url.OriginalString);
        }


        
        #endregion



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