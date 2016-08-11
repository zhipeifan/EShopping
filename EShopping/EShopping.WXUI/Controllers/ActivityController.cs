using EShopping.BusinessService.SelectProduct;
using EShopping.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class ActivityController : BaseController
    {
        //
        // GET: /Activity/
        /// <summary>
        /// 最新活动
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var activitys = ActivityService.LoadActivity(SearchActivityTypeEnum.NewActivity);
            return View(activitys);
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ActivityDetail(int id)
        {
            var dto = ActivityService.LoadActivityDetial(id);

            var recomandProducts = ProductService.LoadRecommandProductList();
            ViewBag.RecommandProducs = recomandProducts;
            return View(dto);
        }
	}
}