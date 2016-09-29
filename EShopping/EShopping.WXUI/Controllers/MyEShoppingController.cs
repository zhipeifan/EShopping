using EShopping.BusinessService.SelectProduct;
using EShopping.BusinessService.ShoppingCar;
using EShopping.Common;
using EShopping.Common.Enums;
using EShopping.Entity.Response.DTO;
using EShopping.Entity.UIDTO;
using EShopping.Entity.UIDTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class MyEShoppingController : BaseController
    {

        public MyEShoppingController()
        {
            ViewBag.SelectEnum = (int)FloolterMenu.Index;
        }
        //
        // GET: /MyEShopping/
        public ActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("WeChatLogin","Base");
            }
            UserDTO user=LoadUserInfo();
            ApplicationLog.DebugInfo("回调结束",Newtonsoft.Json.JsonConvert.SerializeObject(user));
            var levelConfig = LoginService.QueryPrivateLevelConfig(UserId);
            if(levelConfig!=null)
            {
                user.Experience = Convert.ToDouble(levelConfig.currentExpense);
                user.Level = levelConfig.currentLevel;
                user.nextLevelNeedExpense = Convert.ToInt32(levelConfig.nextLevelNeedExpense);
            }
            return View(user);
        }

        /// <summary>
        /// 我红包
        /// </summary>
        /// <returns></returns>
        public ActionResult MyRed()
        {
            var list = LoginService.LoadWallet(UserId);
            return View(list);
        }

        /// <summary>
        /// 收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult AddressList()
        {
            var list = LoginService.LoadAddressList(UserId);
            return View(list);
        }

        /// <summary>
        /// 编辑收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyAddress(int Id=0)
        {
            AddressDTO dto = new AddressDTO();
            if (Id > 0)
            {
                var list = LoginService.LoadAddressList(UserId);
                dto = list.Where(x => x.id == Id).FirstOrDefault();
                if (dto == null)
                    dto = new AddressDTO();
            }
            return View(dto);
        }

        /// <summary>
        /// 编辑收货地址
        /// </summary>
        /// <returns></returns>
       [HttpPost]
        public ActionResult ModifyAddress(AddressDTO dto)
        {
            if (dto.id > 0)
                dto.handleType = 2;
            
            dto.userId = UserId;
            LoginService.ModifyAddress(dto);
            return RedirectToAction("AddressList");
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteAddress(int Id)
        {
            AddressDTO dto = new AddressDTO
            {
                id = Id,
                userId = UserId,
                 handleType=1
            };
            LoginService.ModifyAddress(dto);
            return RedirectToAction("AddressList");
        }

        public ActionResult SetDefaultAddress(int Id)
        {
            AddressDTO dto = new AddressDTO
            {
                id = Id,
                userId = UserId,
                handleType = 3
            };
            LoginService.ModifyAddress(dto);
            return RedirectToAction("AddressList");
        }

        /// <summary>
        /// 金榜排行
        /// </summary>
        /// <returns></returns>
        public ActionResult GoldList()
        {
            var list = LoginService.LoadGoldList();
            return View(list);
        }

        /// <summary>
        /// 邀请好友得红包
        /// </summary>
        /// <returns></returns>
        public ActionResult RobReb()
        {
            return View();
        }

        /// <summary>
        /// 历史购买记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingHistoryList(int pageIndex=1,int pageSize=10)
        {
            var list = ShoppingCarService.LoadBuyList(UserId,BuyTypeEnum.All,pageIndex,pageSize);
            return View(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult ShoppingStartingList(int pageIndex = 1, int pageSize = 10)
        {
            var list = ShoppingCarService.LoadBuyList(UserId, BuyTypeEnum.Staring, pageIndex, pageSize);
            return View(list);
        }

        public ActionResult ShoppingWinnedList(int pageIndex = 1, int pageSize = 10)
        {
            var list = ShoppingCarService.LoadBuyList(UserId, BuyTypeEnum.Winned, pageIndex, pageSize);
            return View(list);
        }

        /// <summary>
        /// 添加晒单
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateShareProduct(int id,int spellbuyId)
        {
            ShareProductDTO dto = new ShareProductDTO();

            var product = ProductService.LoadProductDetail(id,spellbuyId,UserId);

            dto.FaceImg = UserInfo.faceImg;
            dto.UserName = UserInfo.userName;

            if(product!=null)
            {
                dto.shareTitle = product.productTitle;
            }
            return View(dto);
        }
             

        /// <summary>
        /// 中奖记录
        /// </summary>
        /// <returns></returns>
        public  ActionResult   WinnerList()
        {
           var list= ActivityService.LoadMyWinnerList(1);
           return View(list);
        }

        public ActionResult Registering()
        {
            var model = LoginService.LoadSigHistoryList(UserId);
            return View(model);
        }

        public ActionResult CreateSign()
        {
            LoginService.AddSign(UserId);
            return RedirectToAction("Registering");
        }

        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Integral()
        {
            var user = LoginService.LoadUserInfo(UserId);

            IntegralUIDTO ieg = new IntegralUIDTO();
            if(user!=null)
            {
                ieg.Integral = user.Integral;
                ieg.UseIntegral = user.Integral;
            }
            return View(ieg);
        }

         [HttpPost]
        public ActionResult Integral(IntegralUIDTO dto)
        {
            if (dto.UseIntegral > dto.Integral)
                return View("Integral",dto);
             if(dto.UseIntegral<1)
             {
                 return View("Integral", dto);
             }

            LoginService.ExchangeIntegral(UserId,dto.UseIntegral);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="provinceName"></param>
        /// <returns></returns>
         public JsonResult GetProvinceOrCity(string type, string provinceName)
         {
             var data= new LoadLocalDataService().QueryCitys(type, provinceName);
             return Json(data);
         }
	}
}