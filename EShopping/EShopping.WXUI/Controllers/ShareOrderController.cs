using EShopping.BusinessService.SelectProduct;
using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class ShareOrderController : BaseController
    {
        //
        // GET: /ShareOrder/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShareList(int index=1,int pageSize=10)
        {
            var list = LoadData(index, pageSize);
            ViewBag.PageIndex = index;
            ViewBag.PageSize = pageSize;
             return View(list);
        }


        public JsonResult LoadJsonData(int index = 1, int pageSize = 10)
        {
            var list = LoadData(1, 10);
            return Json(list);
        }
        public List<QueryShareInfoListDTO> LoadData(int index , int pageSize )
        {
         return ShareService.ShareList(1, 10, Convert.ToInt32(User.Identity.Name));
        }


        public ActionResult BuyProductHistoryList(int spellbuyproductId)
        {
            var list = ProductService.LoadAttendUsers(spellbuyproductId,1,10);
            return View(list);
        }

        /// <summary>
        /// 赞一个
        /// </summary>
        /// <param name="shareId"></param>
        /// <returns></returns>
        public ActionResult SendUpCount(int shareId, int pageIndex = 1, int pageSize = 10)
        {
            ProductService.SendUpCount(UserId,shareId);
            return RedirectToAction("ShareList");
        }
	}
}