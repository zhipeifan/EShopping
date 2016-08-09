using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class AddressDTO
    {

        public int id { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string district { get; set; }

        public string address { get; set; }

        public string consignee { get;set;}

        public string phone { get; set; }

        public int status { get; set; }

        public bool isDefault { get; set; }

        public int handleType { get; set; }

        public int userId { get; set; }
    }
}
