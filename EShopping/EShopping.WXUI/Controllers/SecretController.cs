using EShopping.BusinessService.SelectProduct;
using EShopping.Entity.UIDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public class SecretController : BaseController
    {
        //
        // GET: /Secret/
        public ActionResult Index(int pageIndex=1,int pageSize=10)
        {
            int userid=Convert.ToInt32(User.Identity.Name);
            var list = ActivityService.LoadSecretList(userid,0,pageIndex,pageSize);
            return View(list);
        }

        /// <summary>
        /// 闺蜜团
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SecretGM(int pageIndex = 1, int pageSize = 10)
        {
            var list = ActivityService.LoadSecretList(0, 1, pageIndex, pageSize);
            return View(list);
        }

        /// <summary>
        /// 基友团
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SecretJY(int pageIndex = 1, int pageSize = 10)
        {
            var list = ActivityService.LoadSecretList(0, 2, pageIndex, pageSize);
            return View(list);
        }

        /// <summary>
        /// 发起秘密团
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="spellbuyproductId"></param>
        /// <returns></returns>
        public ActionResult SecretStart(int productId, int spellbuyproductId,int type)
        {
           var secretDto= ActivityService.CreateSecret(productId,UserId,type);
           if (secretDto == null)
               return RedirectToAction("Index");

           SecretDTO dto = new SecretDTO();
           dto.LinceCode = secretDto.licensingCode;
        //   dto.PrivateCode = secretDto.spellbuyproductId.ToString();
           dto.PrivateMemberCount = 1;
           dto.SpellbuyLimit = secretDto.spellbuyLimit;
           dto.NewSpellbuyproductId = secretDto.spellbuyproductId;

           var product= ProductService.LoadProductDetail(productId, spellbuyproductId,UserId);
            if(product!=null)
            {
                dto.ProductImg = product.coverImg1;
                dto.ProductName = product.productName;
                dto.ProductPrice = product.productPrice;
            }
           return View(dto);
        }

        public ActionResult SaveSecret(SecretDTO dto)
        {
            ActivityService.ModifySecret(dto.NewSpellbuyproductId,dto.PrivateMemberCount,dto.PrivateCode);

            return View("SecretView", dto);
        }

	}
}