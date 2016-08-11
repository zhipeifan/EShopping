using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
            loginType = 2;
        }
        public int loginType { get; set; }
        public int userId { get; set; }
        public string CardNo { get; set; }
        public double Experience { get; set; }

        public string faceImg { get; set; }

        public int Invite{get;set;}

        public string email{get;set;}
 
        public string mailCheck{get;set;}

        public string mobileCheck{get;set;}

        public string mobilePhone{get;set;}

        public string sex{get;set;}

        public string Signature{get;set;}

        public string userName{get;set;}

        public string nickName{get;set;}
 
        //public string UserPwd{get;set;}

        public string Email{get;set;}

        public string UserType{get;set;}

        public string QQ{get;set;}

        public string weixinOpenId { get; set; }

        public decimal RedPacketMoenyOfAll{get;set;}

        public decimal BuyMoneyOfAll { get; set; }

        public decimal CurrentWalletOfAll { get; set; }

        public int Integral{get;set;}

        public decimal money { get; set; }

        /// <summary>
        /// 当前等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 距下一等级需要
        /// </summary>
        public int nextLevelNeedExpense { get; set; }
    }
}
