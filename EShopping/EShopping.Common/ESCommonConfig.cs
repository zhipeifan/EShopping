using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Common
{
    public class ESCommonConfig
    {
        /// <summary>
        /// 请求Service Url
        /// </summary>
        public static string ServiceUrl
        {

            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"];
            }
        }

        public static string AppId
        {

            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WechatAppId"];
            }
        }

        public static string WechatAppSecret
        {

            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WechatAppSecret"];
            }
        }

        /// <summary>
        /// 统一订单Url
        /// </summary>
        public static string UnifiedOrderUrl
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["unifiedOrderUrl"]; }
        }
        
    }
}
