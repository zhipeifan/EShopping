using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class QueryShareInfoListResponse
    {
        public List<QueryShareInfoListDTO> responseData { get; set; }
    }
    public class QueryShareInfoListDTO
    {
        public int Id { get; set; }

        public int replyCount { get; set; }

        public string shareContent { get; set; }

        public long shareDate { get; set; }

        public string shareTitle { get; set; }

        public int upCount { get; set; }

        public string shareImg1 { get; set; }

        public string shareImg2 { get; set; }

        public string shareImg3 { get; set; }

        public string shareImg4 { get; set; }

        public string shareImg5 { get; set; }

        public bool isBuyCurrentShareInfo { get; set; }

        public bool isUpCurrentShareInfo { get; set; }

        public int spellbuyproductId { get; set; }

        public string spellbuyCount { get; set; }

        public string spellbuyLimit { get; set; }

        public string licensingCode { get; set; }

        public long winnerTime { get; set; }

        public int winnerStatus { get; set; }

        public string winnerUserName { get; set; }

        public string winnerUserId { get; set; }

        public string winnerUserBuyCount { get; set; }

        public string winnerBuyCode { get; set; }

        public string systemTime { get; set; }

        public int currentUserBuyCount { get; set; }

        public int spellBuyType { get; set; }

        public UserDTO userInfoVO { get; set; }

        public ProductDTO productVO { get; set; }
             
    }
}


 
 
	
	 

