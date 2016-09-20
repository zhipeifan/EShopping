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

        public static void Error(string title, string content)
        {
            Error("Error",title,content);
        }

        public static void Error(string content)
        {
            Error("Error", "",content);
        }

        public static void DebugInfo(string title,string content)
        {
            Error("Debug",title, content);
        }

        public static void DebugInfo(string content)
        {
            Error("Debug","", content);
        }

        public static void Error(string MName,string title,string request)
        {
          //  HttpContext.Current.Request
            var basepath = System.AppDomain.CurrentDomain.BaseDirectory+"WebDebugInfo\\";
            string dataStr = DateTime.Now.ToString("yyyyMMdd");
            string path = basepath + "Error_" + dataStr+"_"+MName+".txt";
            if (!Directory.Exists(basepath))
                Directory.CreateDirectory(basepath);

            if (!File.Exists(path))
                File.Create(path).Close();

            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter writer = new StreamWriter(fs);
            //开始写入
            writer.WriteLine(DateTime.Now.ToString()+" --Error Start----------------");
            writer.WriteLine("Title : " + title);
            writer.WriteLine(request);
            writer.WriteLine("Exception : ");
            writer.WriteLine();
            writer.WriteLine(MName + "-----------------Error   End-----------------");
            writer.WriteLine("");
            //清空缓冲区
            writer.Flush();
            //关闭流
            writer.Close();
            fs.Close();

            //string path = "";
        }
    }
}
