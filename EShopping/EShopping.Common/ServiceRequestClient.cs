using EShopping.Common.Enums;
using EShopping.Entity.Request;
using EShopping.Entity.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EShopping.Common
{
    public static class ServiceRequestClient
    {

        public static string PostRquest(ServicesEnum name, string requestContent)
        {
            try
            {
                var request = WebRequest.Create(string.Format(ESCommonConfig.ServiceUrl, string.Format("api/{0}",name.ToString()))) as HttpWebRequest;
                request.Method = "POST";
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 300;
                request.AllowAutoRedirect = true;
                request.Timeout = 100000;
                request.ReadWriteTimeout = 10000;
                request.ContentType = "application/json";
                // request.Accept = "application/xml";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Headers.Add("X-Auth-Token", HttpUtility.UrlEncode("openstack"));
                // string strContent = "{\"appId\":\"\",\"method\":\"\",\"tokenId\":\"\",\"data\":{\"userAccountId\":\"" + userid + "\"}}";
                // request.Proxy = null;
                // request.CookieContainer = cookieContainer;

                ApplicationLog.RequestMsg(name.ToString(),requestContent);

                if (!string.IsNullOrEmpty(requestContent))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(requestContent);
                    using (var dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(bytes, 0, bytes.Length);
                    }
                }
                var response = (HttpWebResponse)request.GetResponse();
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    var resp= stream.ReadToEnd();
                    ApplicationLog.RequestMsg(name.ToString(), resp);
                    return resp;

                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="name"></param>
        /// <param name="requestContent"></param>
        /// <returns></returns>
        public static string PostRquestAndAttachment(Dictionary<string, Stream> streams)
        {
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

                var request = WebRequest.Create(string.Format(ESCommonConfig.ServiceUrl, "uploadImage?fileType=shareInfoImg")) as HttpWebRequest;
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                using (Stream stream = request.GetRequestStream())
                {
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;

                    int i = 0;
                    foreach (var item in streams)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string header = string.Format(headerTemplate, "file" + i, item.Key);
                        byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                        stream.Write(headerbytes, 0, headerbytes.Length);
                        var bytes = StreamToBytes(item.Value);
                        stream.Write(bytes, 0, bytes.Length);
                    }

                    stream.Write(endbytes, 0, endbytes.Length);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    return stream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }



        public static CommonResponse PrivatePostRquest(ServicesEnum name, string requestContent)
        {
            try
            {
                var request = WebRequest.Create(string.Format(ESCommonConfig.ServiceUrl, name.ToString())) as HttpWebRequest;
                request.Method = "POST";
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 300;
                request.AllowAutoRedirect = true;
                request.Timeout = 10000;
                request.ReadWriteTimeout = 10000;
                request.ContentType = "application/json";
                // request.Accept = "application/xml";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Headers.Add("X-Auth-Token", HttpUtility.UrlEncode("openstack"));
                // string strContent = "{\"appId\":\"\",\"method\":\"\",\"tokenId\":\"\",\"data\":{\"userAccountId\":\"" + userid + "\"}}";
                // request.Proxy = null;
                // request.CookieContainer = cookieContainer;

                if (!string.IsNullOrEmpty(requestContent))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(requestContent);
                    using (var dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(bytes, 0, bytes.Length);
                    }
                }
                var response = (HttpWebResponse)request.GetResponse();
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    string responseStr = stream.ReadToEnd();
                    if (string.IsNullOrEmpty(responseStr))
                        return new CommonResponse();
                   // responseStr = "{" + responseStr + "}";
                    return JsonConvert.DeserializeObject<CommonResponse>(responseStr);
                }

                return new CommonResponse();
            }
            catch (Exception ex)
            {
                return new CommonResponse
                {
                    responseStatus = new PrivateResponseStatus
                    {
                        statusCode = 201,
                        statusDes = ex.Message
                    }
                };
            }

        }
    }
}
