using EShopping.BusinessService.SelectProduct;
using EShopping.Common.Enums;
using EShopping.Entity.Response.DTO;
using EShopping.WXUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class ProductController : BaseController
    {
        //public ProductController()
        //{
        //    ChangeFlooterEnum(FloolterMenu.AllProduct);
        //}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId">标签分类，最新，最快，最热，新手专区，大众专区等</param>
        /// <param name="tcategoryId">商品类别分类</param>
        /// <param name="sortType">排序类型，0=升序，1=降序</param>
        /// <returns></returns>
        public ActionResult List(int categoryId = 0, int areaId = 0, int tcategoryId = 0, int pageIndex=1,int pageSize=10)
        {

            ViewBag.CategoryId = categoryId;
            ViewBag.TCategoryId = tcategoryId;
            ViewBag.AreaTypeId = areaId;

             ViewBag.PageIndex = pageIndex;
             ViewBag.PageSize = pageSize;
           
           var list= ProductService.LoadProductByType(tcategoryId,categoryId,areaId,pageIndex,pageSize);

             ChangeFlooterEnum(FloolterMenu.AllProduct);

             ViewBag.ShoppingCarCount = LoadShoppingCar().Count();

             var productTypes = ProductService.LoadProductType();
             ViewBag.ProductTypes = productTypes;
           return View(list);
        }

        public ActionResult ProductItems(int categoryId = 0, int areaId = 0, int tcategoryId = 0, int pageIndex = 1, int pageSize = 10)
        {
            var list = ProductService.LoadProductByType(tcategoryId, categoryId, areaId, pageIndex, pageSize);
            return PartialView(list);
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="spellBuyProductId"></param>
        /// <returns></returns>
        public ActionResult Detail(int id, int spellBuyProductId)
        {
            var response = ProductService.LoadProductDetail(id, spellBuyProductId);
            var list = LoadAttendUsers(spellBuyProductId, 1, 10);
            ViewBag.AttendUsers = list;
            return View(response);
        }

        /// <summary>
        /// 商品参与列表
        /// </summary>
        /// <param name="spellBuyProductId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoadAttendList(int spellBuyProductId, int pageIndex = 1, int pageSize = 10)
        {
            var list = LoadAttendUsers(spellBuyProductId,pageIndex,pageSize);
            if (list == null)
                return Json("");
            return Json(list);
        }


        private List<AttendUserDTO> LoadAttendUsers(int spellBuyProductId,int pageIndex=1,int pageSize=10)
        {
            return ProductService.LoadAttendUsers(spellBuyProductId,pageIndex,pageSize);
        }

        public ActionResult CreateShoppingToCar(int id, int spellBuyProductId)
        {
             ShoppingCarList(id, spellBuyProductId);

             return RedirectToAction("ShoppingList", "ShoppingCar");
        }

        public ActionResult Search(string key="", int pageIndex = 1, int pageSize = 10)
        {
            //if(string.IsNullOrEmpty(key))
            //{
            //    return RedirectToAction("List","Product");
            //}
            //else
            //{
            //    return RedirectToAction("SearchResult", "Product", new { key=key});
            //}
            return View();
        }

        /// <summary>
        /// 搜索商品结果
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(string key,int pageIndex=1,int pageSize=10)
        {
            var list = ProductService.SearchProducts(key,pageIndex,pageSize);
            return View(list);
        }
	}
}