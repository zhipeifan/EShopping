using EShopping.BusinessService.SelectProduct;
using EShopping.BusinessService.ShoppingCar;
using EShopping.Common;
using EShopping.Common.Enums;
using EShopping.Entity.Request;
using EShopping.Entity.Response.DTO;
using EShopping.Entity.UIDTO;
using EShopping.Entity.UIDTO.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            dto.productId = id;
            dto.spellbuyproductId = spellbuyId;

            if(product!=null)
            {
                dto.shareTitle = product.productTitle;
            }
            return View(dto);
        }

        /// <summary>
        /// 添加晒单
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveShareProduct(ShareProductDTO dto)
        {
            if (string.IsNullOrEmpty(dto.shareContent))
            {
                ModelState.AddModelError("ShareOrderContent", "亲，请填写晒单内容！");
                return View("CreateShareProduct", dto);
            }

            if (dto.ShareImages == null || dto.ShareImages.Count == 0 || dto.ShareImages.Count > 5)
            {
                ModelState.AddModelError("ShareOrderImage", "亲，晒带图片最多上传5张，至少上传一张！");
                return View("CreateShareProduct", dto);
            }


            Dictionary<string, Stream> streams = new Dictionary<string, Stream>();

            int index=1;
            dto.ShareImages.ForEach(x =>
            {
                streams.Add(DateTime.Now.ToString("yyyyMMddHHmmssfff")+index + ".jpg", x.StringToStream());
                index++;
            });
            List<string> urls = ShareService.BatchUploadAttachment(streams);


            if (urls != null && urls.Count > 0)
            {
                for (var i = 0; i < urls.Count; i++)
                {
                    if (i == 0)
                        dto.shareImg1 = urls[i];
                    if (i == 1)
                        dto.shareImg2 = urls[i];
                    if (i == 2)
                        dto.shareImg3 = urls[i];
                    if (i == 3)
                        dto.shareImg4 = urls[i];
                    if (i == 4)
                        dto.shareImg5 = urls[i];
                }
            }
            dto.userId = UserId;
            dto.addOrUpdate = 1;
            ProductService.AddShareProductOrder(dto);
            return RedirectToAction("ShoppingWinnedList", "MyEShopping");
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

        public ActionResult MeInfo()
         {
             if (!User.Identity.IsAuthenticated)
             {
                 return RedirectToAction("WeChatLogin", "Base");
             }
             UserDTO user = LoadUserInfo();
             return View(user);
         }

        /// <summary>
        /// 修改头像、名称
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyMe()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("WeChatLogin", "Base");
            }
            UserDTO user = LoadUserInfo();
            return View(user);
        }

        [HttpPost]
        public ActionResult SaveMyInfo(UserDTO newUser)
        {
            if (string.IsNullOrEmpty(newUser.nickName))
            {
                ModelState.AddModelError("UserNickName","亲，用户昵称不能为空哦！");
                return View("ModifyMe",newUser);
            }

            // ModelState.AddModelError

            // if(Request.Form["hidFaceImg"].)
            UserDTO user = LoadUserInfo();
            user.nickName = newUser.nickName;

            var img = Request.Files["uploadImg"];
            if(img!=null)
            {
                Dictionary<string, Stream> streams = new Dictionary<string, Stream>();
                streams.Add(DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg", img.InputStream);
                string url = ShareService.UploadAttachment(streams);
                user.faceImg = url;
            }

            UpdateUserInfoDTO updateUserInfo = new UpdateUserInfoDTO()
            {
                faceImg = user.faceImg,
                mobilePhone = user.mobilePhone,
                nickName = user.nickName,
                userId = user.userId
            };

            LoginService.ModifyUserInfo(updateUserInfo);

            var newuserinfo = LoginService.LoadUserInfo(updateUserInfo.userId);

            string _userInfo = Newtonsoft.Json.JsonConvert.SerializeObject(newuserinfo);
            ReloadCookie(newuserinfo.userId, _userInfo);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 错误弹层
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorPage(string error)
        {
            return PartialView(error);
        }

        /// <summary>
        ///我的晒单
        /// </summary>
        /// <returns></returns>
        public ActionResult MyShareOrderList()
        {
            var list = ShareService.ShareList(1, 10, UserId,2);
            return View(list);
        }
             
        
	}
}