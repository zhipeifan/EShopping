using EShopping.BusinessService.SelectProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class SayController : BaseController
    {
        //
        // GET: /XiaoBian/
        public ActionResult Index()
        {
            var data = ShareService.LoadSayList();
            return View(data);
        }
	}
}