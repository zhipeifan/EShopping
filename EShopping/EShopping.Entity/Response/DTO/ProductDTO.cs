using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
   /// <summary>
   /// 商品详情
   /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }
        public string productBrand { get; set; }
        public string productDetail { get; set; }

        /// <summary>
        /// 拼购上限
        /// </summary>
        public int productLimit { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public decimal productRealPrice { get; set; }
        public string productTitle { get; set; }
        public int singlePrice { get; set; }
        public int status { get; set; }
        public string coverImg1 { get; set; }
        public string coverImg2 { get; set; }
        public string coverImg3 { get; set; }
        public string coverImg4 { get; set; }
        public string coverImg5 { get; set; }
        public string detailImg { get; set; }
        public int prefecture { get; set; }
        public int spellbuyproductId { get; set; }
        /// <summary>
        /// 当前拼购数量
        /// </summary>
        public int spellbuyCount { get; set; }
        public int spellbuyLimit { get; set; }
        public string licensingCode { get; set; }
        public long winnerTime { get; set; }
        public int winnerStatus { get; set; }

        public int winnerUserId { get; set; }

        public int winnerUserBuyCount { get; set; }

        public string winnerBuyCode { get; set; }

        public string winnerUserName { get; set; }

        public string winnerFaceImg { get; set; }

        public long systemTime { get; set; }
        public int currentUserBuyCount { get; set; }

        public List<string> ProductImages
        {
            get
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(coverImg1))
                    list.Add(coverImg1);
                if (!string.IsNullOrEmpty(coverImg2))
                    list.Add(coverImg2);
                if (!string.IsNullOrEmpty(coverImg3))
                    list.Add(coverImg3);
                if (!string.IsNullOrEmpty(coverImg4))
                    list.Add(coverImg4);
                if (!string.IsNullOrEmpty(coverImg5))
                    list.Add(coverImg5);

                return list;
            }
        }


        //public int allBuyCount { get; set; }

        //public string winnerCode { get; set; }

             
    }
}
