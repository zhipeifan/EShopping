using EShopping.Common.Enums;
using EShopping.Entity.Request;
using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopping.Common;
using Newtonsoft.Json;

namespace EShopping.BusinessService.SelectProduct
{
   public class LoadLocalDataService
    {

       public  LoadLocalDataService()
       {
           var basepath = System.AppDomain.CurrentDomain.BaseDirectory + "LocalData\\";
           string path = basepath + "ProvinceCitys.txt";
           
           if (!Directory.Exists(basepath))
               Directory.CreateDirectory(basepath);

           if (!File.Exists(path))
           {
               File.Create(path).Close();


               var data = LoadCitys();


               FileStream fs = new FileStream(path, FileMode.Append);
               StreamWriter writer = new StreamWriter(fs);
               //开始写入
               string jsonData = JsonConvert.SerializeObject(data);
               writer.WriteLine(jsonData);
               //清空缓冲区
               writer.Flush();
               //关闭流
               writer.Close();
               fs.Close();
           }
       }

       /// <summary>
       /// 获取城市
       /// </summary>
       /// <param name="type"></param>
       /// <param name="provinceName"></param>
       /// <returns></returns>
       public List<ProvoinceDTO> QueryCitys(string type, string provinceName)
       {
           var result = GetLoadCityData();

           if(!string.IsNullOrEmpty(type))
           {
               if (string.IsNullOrEmpty(provinceName))
                   return new List<ProvoinceDTO>();
               else
               {
                   return result.Where(c => c.provinceName == provinceName).ToList();
               }
           }
           return result;
       }


       private List<ProvoinceDTO> GetLoadCityData()
       {
           var basepath = System.AppDomain.CurrentDomain.BaseDirectory + "LocalData\\";
           string path = basepath + "ProvinceCitys.txt";

           if (!Directory.Exists(basepath))
               return new List<ProvoinceDTO>();

           if (!File.Exists(path))
               return new List<ProvoinceDTO>();

           FileStream fs = new FileStream(path, FileMode.Open);
           StreamReader read = new StreamReader(fs);
           string data = read.ReadToEnd();
           //清空缓冲区
           read.Close();
           //关闭流
           fs.Close();
           if (string.IsNullOrEmpty(data))
               return new List<ProvoinceDTO>();

           return JsonConvert.DeserializeObject<List<ProvoinceDTO>>(data);
       }

       private List<ProvoinceDTO> LoadCitys()
       {
           CommonRequest request = new CommonRequest() { payload = "" };
           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryRegion, request.ReplcaceRequest<CommonRequest>());
           if (response == null)
               return new List<ProvoinceDTO>();
           var data = response.ToEntity<QueryRegionResponse>();
           if (data.provinceVOs == null)
               return new List<ProvoinceDTO>();
           return data.provinceVOs;
       }
    }
}
