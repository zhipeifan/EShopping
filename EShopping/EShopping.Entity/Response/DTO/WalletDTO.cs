using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    /// <summary>
    /// 钱包记录
    /// </summary>
    public class WalletDTO
    {
        public bool isRedPacket{get;set;}

        public decimal moeny{get;set;}

        public bool deleteFlag{get;set;}

        public long createTime{get;set;}

        public long lastUpdateTime{get;set;}

        public long startTime{get;set;}

        public long endTime{get;set;}

        public int status{get;set;}
    }
}
