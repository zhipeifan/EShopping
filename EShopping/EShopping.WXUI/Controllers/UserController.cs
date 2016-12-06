using EShopping.BusinessService.SelectProduct;
using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EShopping.WXUI.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取当前用户登录信息
        /// </summary>
        /// <returns></returns>
        public UserDTO GetUser()
        {
            UserDTO userInfo = null;
            if(!User.Identity.IsAuthenticated)
            {
                string strUserData = ((FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket.UserData;
                userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(strUserData);
                if (userInfo == null || userInfo.userId == 0)
                {
                    userInfo = LoginService.LoginUser(new UserDTO
                    {
                        faceImg = userInfo.faceImg,
                        nickName = userInfo.nickName,
                        weixinOpenId = userInfo.weixinOpenId
                    });
                }
            }

            return userInfo;
        }
	}
}