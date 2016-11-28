using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class QueryPublishingHistoryListResponse
    {
        public QueryPublishingHistoryListDTO responseData { get; set; }
    }

    public class QueryPublishingHistoryListDTO
    {
        public List<WinnerItems> productVOs { get; set; }
    }

    public class WinnerItems
    {
        /// <summary>
        /// 获奖人头像
        /// </summary>
        public string winnerFaceImg { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public long winnerBuyTime { get; set; }

        /// <summary>
        /// 幸运号码
        /// </summary>
        public string winnerBuyCode { get; set; }

        /// <summary>
        /// 中奖人昵称
        /// </summary>
        public string winnerUserName { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int winnerUserBuyCount { get; set; }


        /// <summary>
        /// 期号
        /// </summary>
        public string licensingCode { get; set; }
    }
}
