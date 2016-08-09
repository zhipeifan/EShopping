using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class CommonResponse
    {
        //{"responseData":[],"responseStatus":{"statusCode":200,"statusDes":"200"}}
        public PrivateResponseStatus responseStatus { get; set; }

        public string responseData { get; set; } 
    }

    public class PrivateResponseStatus
    {
        public int statusCode { get; set; }

        public string statusDes { get; set; }
    }
}
