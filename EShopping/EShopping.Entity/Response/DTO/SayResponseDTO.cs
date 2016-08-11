using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class SayResponseDTO
    {
        public int id { get; set; }
        public string content { get; set; }
        public long startTime { get; set; }
        public long endTime { get; set; }
        public long createTime { get; set; }
        public string title { get; set; }
        public string  imagePath { get; set; }
        public int status { get; set; }
    }
}
