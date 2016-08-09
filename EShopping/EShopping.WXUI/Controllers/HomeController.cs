using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EShopping.Entity.Response.DTO;
using EShopping.BusinessService.SelectProduct;
using EShopping.Entity.UIDTO;
using System.Web.Security;
using EShopping.BusinessService.ShoppingCar;
using EShopping.Entity.UIDTO.Enum;
using System.Collections.Specialized;
using ConsoleApplication1;
using System.IO;

namespace EShopping.WXUI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {

         
            var response = ProductService.LoadIndexData();
            if (response == null)
                response = new ResponseData();

            var winnerList = ActivityService.LoadWinnerList();
            if (winnerList == null)
                winnerList = new List<WinnerProductDTO>();
            ViewBag.WinnerList = winnerList;

            return View(response);
        }

        public ActionResult testIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            Dictionary<string, Stream> streams = new Dictionary<string, Stream>();
            var files = Request.Files;
            streams.Add(files[0].FileName, files[0].InputStream);
            string url = ShareService.UploadAttachment(streams);

            return View();
        }

        [HttpPost]
        public int AddProductToCar(int id, int spellBuyProductId)
        {
            return ShoppingCarList(id,spellBuyProductId);
        }

         /// <summary>
         /// 上传附件
         /// </summary>
         /// <returns></returns>
        public string UploadAttachment()
        {
            var files=Request.Files;

           // files[0].InputStream

            return "";
        }
	}
}