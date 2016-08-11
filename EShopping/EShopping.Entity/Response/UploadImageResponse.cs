using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class UploadImageResponse
    {
        public ImageUrl responseData { get; set; }
    }

    public class ImageUrl
    {
        public List<string> imagePathList { get; set; }
    }

    //{
    //"responseData": {
    //    "imagePathList": [
    //        "/home/azureuser/yigoushopping/apache-tomcat-7.0.32/webapps/yigoushopping-imgs/shareInfoImgbuild.prop"
    //    ]
    //},
    //"responseStatus": {
    //    "statusCode": 200,
    //    "statusDes": "200"
    //}
}
