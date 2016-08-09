using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{

    public class SigDTO
    {
        /// <summary>
        /// 是连续签到多少天有红包
        /// </summary>
        public int days { get; set; }

        /// <summary>
        /// 是最高奖励多少分
        /// </summary>
        public int integral { get; set; }

        /// <summary>
        /// 开始奖励积分,就是第一天签到多少积分
        /// </summary>
        public int integralStart { get; set; }

        /// <summary>
        /// 是增量，比如第一天签到当天获取10，增量是10分的话 第二天当天签到就获取20分
        /// </summary>
        public int integralDelta { get; set; }

        /// <summary>
        /// 是红包的金额
        /// </summary>
        public int redPacket { get; set; }

        /// <summary>
        /// 是连续签到了多少次
        /// </summary>
        public bool deleteFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int signIntegral { get; set; }

        /// <summary>
        /// 连续签到了多少次
        /// </summary>
        public int signCount { get; set; }

        /// <summary>
        /// 是最后签到时间
        /// </summary>
        public long signLastTime { get; set; }

        public long systemTime { get; set; }

        public List<SignHistoryDTO> signHistoryVOs { get; set; }
    }


   public class SignHistoryDTO
    {

       public int id{get;set;}

       public long signTime{get;set;}

       /// <summary>
       /// 签到时间
       /// </summary>
       public DateTime signTimeString{get;set;}

       /// <summary>
       /// 是否有红包
       /// </summary>
       public bool isHadReadPackage{get;set;}

       public int integral{get;set;}

    }
}
