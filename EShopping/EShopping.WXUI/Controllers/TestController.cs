using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            //test commit
            string s = "ss";
            return View();
        }
	}
}