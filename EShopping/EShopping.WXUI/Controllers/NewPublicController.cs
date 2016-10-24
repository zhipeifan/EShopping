using EShopping.BusinessService.SelectProduct;
using EShopping.Common.Enums;
using EShopping.Entity.Response.DTO;
using EShopping.Entity.UIDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class NewPublicController : BaseController
    {
        public NewPublicController()
        {
            ViewBag.SelectEnum = (int)FloolterMenu.NewPublic;
        }
        //
        // GET: /NewPublic/
        public ActionResult List(int PageIndex=1,int PageSize=10)
        {
            var list = ProductService.LoadNewPublic(1, 10);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;
            NewPublicListDTO data = new NewPublicListDTO
            {
                Products = list
            };

            return View(data);
        }

        public JsonResult NewPubHistory(int PageIndex=1, int PageSize = 10)
        {
            var list = ProductService.LoadNewPublic(1, 10);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = PageSize;

            return Json(list);

        }

        public ActionResult Detail()
        {
            return View(new  ProductDTO());
        }
	}
}