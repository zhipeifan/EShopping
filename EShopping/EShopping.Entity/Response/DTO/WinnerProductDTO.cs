using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class WinnerProductDTO
    {
        public string licensingCode { get; set; }

        public string winnerUserName { get; set; }
        public long winnerTime { get; set; }

        public string buyCode { get; set; }

        public int allBuyCount { get; set; }

        public string productName { get; set; }

        public int spellbuyproductId { get; set; }

        public string productImg1 { get; set; }

        public decimal productPrice { get; set; }

        public bool isShareInfo { get; set; }

        public string winnerCode { get; set; }

        public string allBuyCode { get; set; }

        public int spellbuyCount { get; set; }

        public int spellbuyLimit { get; set; }

        public int newSpellbuyproductId { get; set; }

        public decimal singlePrice { get; set; }

        public string expressCode { get; set; }

        public int expressStatus { get; set; }

        public string expressNo { get; set; }

        public int productId { get; set; }
    }

}
