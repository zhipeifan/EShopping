using EShopping.BusinessService.SelectProduct;
using EShopping.BusinessService.ShoppingCar;
using EShopping.Entity.UIDTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class UserCenterController : BaseController
    {
        //
        // GET: /UserCenter/
        public ActionResult Index(int id)
        {
            var user = LoginService.LoadUserInfo(id);
            var levelConfig = LoginService.QueryPrivateLevelConfig(id);
            if (levelConfig != null)
            {
                user.Experience = Convert.ToDouble(levelConfig.currentExpense);
                user.Level = levelConfig.currentLevel;
                user.nextLevelNeedExpense = Convert.ToInt32(levelConfig.nextLevelNeedExpense);
            }
            user.userId = id;
            return View(user);
        }

        public ActionResult ShoppingWinnedList(int id ,int pageIndex = 1, int pageSize = 10)
        {
            var list = ShoppingCarService.LoadBuyList(id, BuyTypeEnum.Winned, pageIndex, pageSize);
            ViewBag.UserId = id;
            return View(list);
        }

        public ActionResult ShoppingWinnedListPartial(int id = 0, int pageIndex = 1, int pageSize = 10)
        {
            if (id == 0)
                id = UserId;
            var list = ShoppingCarService.LoadBuyList(id, BuyTypeEnum.Winned, pageIndex, pageSize);
            return PartialView(list);
        }

        /// <summary>
        /// 历史购买记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingHistoryList(int id, int pageIndex = 1, int pageSize = 10)
        {
            var list = ShoppingCarService.LoadBuyList(id, BuyTypeEnum.All, pageIndex, pageSize);
            ViewBag.UserId = id;
            return View(list);
        }

        /// <summary>
        /// 历史购买记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingHistoryListPartial(int id, int pageIndex = 1, int pageSize = 10)
        {
            var list = ShoppingCarService.LoadBuyList(id, BuyTypeEnum.All, pageIndex, pageSize);
            ViewBag.UserId = id;
            return PartialView(list);
        }

        /// <summary>
        ///我的晒单
        /// </summary>
        /// <returns></returns>
        public ActionResult MyShareOrderList(int id, int pageIndex = 1, int pageSize = 10)
        {
            var list = ShareService.ShareList(pageIndex, pageSize, id, 2);
            ViewBag.UserId = id;
            return View(list);
        }

        /// <summary>
        ///我的晒单
        /// </summary>
        /// <returns></returns>
        public ActionResult MyShareOrderListPartial(int id, int pageIndex = 1, int pageSize = 10)
        {
            var list = ShareService.ShareList(pageIndex, pageSize, id, 2);
            ViewBag.UserId = id;
            return PartialView(list);
        }
	}
}