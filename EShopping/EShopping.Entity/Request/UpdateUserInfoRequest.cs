using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class UpdateUserInfoRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string payload { get; set; }

        public string token { get; set; }
    }

    public class UpdateUserInfoDTO
    {
        public int userId { get; set; }
        public string nickName { get; set; }

        public string mobilePhone { get; set; }

        public string faceImg { get; set; }
        
    }
}
