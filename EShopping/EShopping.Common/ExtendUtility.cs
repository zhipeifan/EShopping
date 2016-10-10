using EShopping.Common.Enums;
using EShopping.Entity.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Common
{
    public static class ExtendUtility
    {
        public static List<T> ToEntitys<T>(this string response)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(response);
        }

        public static T ToEntity<T>(this string response)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
        }

        /// <summary>
        /// 格式化Request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string FormatRequest<T>(this T entity)
        {
            CommonRequest request = new CommonRequest
            {
                payload = entity.ReplcaceRequest<T>()
            };
            return JsonConvert.SerializeObject(request);
        }

 


        /// <summary>
        /// 转换为API需要的Request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ReplcaceRequest<T>(this T entity)
        {
            string _payload = JsonConvert.SerializeObject(entity);
            _payload = _payload.Replace("\"", "'");
            _payload = _payload.Replace("\\", "");
            return _payload;
        }

        public static string ConvertProductspeed(int productLimit, int spellbuyCount)
        {
            string liWidth = "0%";
            if (productLimit == 0)
                return liWidth;
            if (productLimit > spellbuyCount)
            { liWidth = "100%"; }
            else
            {
                liWidth = ((Convert.ToDouble(productLimit) / Convert.ToDouble(spellbuyCount)) * 100).ToString("F2") + "%";
            }
            return liWidth;
        }

        public static string ConvertProductspeed(double productLimit, int spellbuyCount)
        {
            string liWidth = "0%";
            if (productLimit == 0)
                return liWidth;
            if (productLimit > spellbuyCount)
            { liWidth = "100%"; }
            else
            {
                liWidth = ((Convert.ToDouble(productLimit) / Convert.ToDouble(spellbuyCount)) * 100).ToString("F2") + "%";
            }
            return liWidth;
        }

        /// <summary>
        /// 将long转为指定小时、分、秒
        /// </summary>
        /// <param name="winnerTime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int ConvertToPublicDate(this long unixTimeStamp, ConvertDateTypeEnum type)
        { 
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            }catch(Exception ex)
            {
                dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            }

           //var diffDate = dtDateTime - DateTime.Now;
            var diffDate = dtDateTime;

            switch (type)
            {
                case ConvertDateTypeEnum.Hour:
                    return diffDate.Hour;
                    break;
                case ConvertDateTypeEnum.Minutes:
                    return diffDate.Minute;
                    break;
                case ConvertDateTypeEnum.Second:
                    return diffDate.Second;
                    break;
                default:
                    return 0;
                    break;
            }
        }
        public static string ConvertToPublicDate(this long unixTimeStamp, string format = "yyyy-MM-dd HH:mm")
        {
            //if (winnerTime == 0)
            //    return "";
            //return new DateTime(winnerTime).ToString(format);
            if (unixTimeStamp == 0)
                return "";

            // 定义其实时间
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            }
            catch (Exception ex)
            {
                dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            }
            return dtDateTime.ToString(format);
        }

        public static DateTime ConvertToPublicDateFormat(this long unixTimeStamp, string format = "yyyy-MM-dd HH:mm")
        {
            //if (winnerTime == 0)
            //    return "";
            //return new DateTime(winnerTime).ToString(format);
            if (unixTimeStamp == 0)
                return Convert.ToDateTime("1900-01-01");

            // 定义其实时间
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            }
            catch (Exception ex)
            {
                dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            }
            return dtDateTime;
        }

        public static string ConvertToPublicDate_Second(this long unixTimeStamp, string format = "yyyy-MM-dd HH:mm")
        {
            //if (winnerTime == 0)
            //    return "";
            //return new DateTime(winnerTime).ToString(format);

            // 定义其实时间
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            }
            catch (Exception ex)
            {
                dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            }
            return dtDateTime.ToString(format);
        }

        public static long DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }

        public static Stream StringToStream(this string str)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            return new MemoryStream(strBytes);
        }
    }
}
