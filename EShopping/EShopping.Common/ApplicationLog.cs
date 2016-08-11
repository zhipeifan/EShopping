using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Common
{
    public static class ApplicationLog
    {

        public static void Error(string content)
        {
            Error("Error",content);
        }

        public static void DebugInfo(string content)
        {
            Error("Error", content);
        }

        public static void Error(string MName,string request)
        {
          //  HttpContext.Current.Request
            var basepath = System.AppDomain.CurrentDomain.BaseDirectory;
            string dataStr = DateTime.Now.ToString("yyyyMMdd");
            string path = basepath + "Error_" + dataStr+"_"+MName+".txt";
            if (!File.Exists(path))
                File.Create(path);


            using(var writer=File.AppendText(path))
            {
                writer.WriteLine(MName + "-----------------Error Start-----------------");
                writer.WriteLine("Request : ");
                writer.WriteLine(request);
                writer.WriteLine("Response Exception : ");
                writer.WriteLine();
                writer.WriteLine(MName + "-----------------Error   End-----------------");
            }

            //string path = "";
        }
    }
}
