using EShopping.BusinessService.SelectProduct;
using EShopping.Common;
using EShopping.Entity.Response.DTO;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EShopping.WXUI.Controllers
{
    public class LoginController : Controller
    {
        private static string appId = ConfigurationManager.AppSettings["WechatAppId"];
        private static string appSecret = ConfigurationManager.AppSettings["WechatAppSecret"];

        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult WeChatLogin(string code, string state)
        {


            ApplicationLog.DebugInfo("Code:" + code + ",State:" + state);
            string url = Request.Url.OriginalString;
            ApplicationLog.DebugInfo("Url:" + url);
            if (string.IsNullOrEmpty(code))
                return Redirect(OAuthApi.GetAuthorizeUrl(appId, "http://www.kalezhe.com.cn/Base/WeChatLogin", "LOGIN", OAuthScope.snsapi_userinfo));

            var openIdResponse = OAuthApi.GetAccessToken(appId, appSecret, code);
            var wechatUser = OAuthApi.GetUserInfo(openIdResponse.access_token, openIdResponse.openid);

            ApplicationLog.DebugInfo(Newtonsoft.Json.JsonConvert.SerializeObject(wechatUser));

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
            FormsAuthentication.SetAuthCookie(userinfo.userId.ToString(), true, FormsAuthentication.FormsCookiePath);
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
	}
}