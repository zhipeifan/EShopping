using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class BuyProductDTO
    {
        public int id { get; set; }
        public string productBrand { get; set; }
        public string productDetail { get; set; }
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

        public List<BuyDetailDTO> buyDetailVOs { get; set; }

        public int spellbuyproductId { get; set; }
        public int spellbuyCount { get; set; }
        public int spellbuyLimit { get; set; }
        public string licensingCode { get; set; }
    
     
        public long systemTime { get; set; }
        public int currentUserBuyCount { get; set; }
        public string currentUserBuyCode { get; set; }
        public int spellBuyType { get; set; }

        public long winnerTime{get;set;}

        public int winnerStatus{get;set;}

        public string winnerUserName{get;set;}

        public int winnerUserId{get;set;}

        public int winnerUserBuyCount{get;set;}

        public string winnerBuyCode{get;set;}
    }

    public class BuyDetailDTO
    {
        public string buyCode { get; set; }
        public long buyTime { get; set; }
    }
}
