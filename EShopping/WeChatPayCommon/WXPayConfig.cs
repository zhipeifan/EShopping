using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatPayCommon
{
    /// <summary>
    /// =======【基本信息设置】=====================================
    ///* 微信公众号信息配置
    ///* APPID：绑定支付的APPID（必须配置）
    ///* MCHID：商户号（必须配置）
    ///* KEY：商户支付密钥，参考开户邮件设置（必须配置）
    /// * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
    /// </summary>
    public static class WXPayConfig
    {


        /// <summary>
        /// APPID
        /// </summary>
        //public const string APPID = "wx2a31736af6d953ae";//网站的APPID
        public static string APPID
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["APPID"];
            }
        }

        /// <summary>
        /// APP SECRET
        /// </summary>
        // public const string APPSECRET = "9a1078ae0a79a4fee662f2a19e19105b";
        public static string APPSECRET
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["APPSECRET"];
            }
        }

        /// <summary>
        /// 商户ID
        /// </summary>
        // public const string MCHID = "1381539402";
        public static string MCHID
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["MCHID"];
            }
        }

        /// <summary>
        /// 统一订单API安全验证KEY
        /// </summary>
       // public const string APIKEY = "h03xvskitey5d7KcJ3lfanT9oPBD3bgO";
        public static string APIKEY
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["APIKEY"];
            }
        }
    }
}
