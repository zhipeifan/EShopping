using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class Response
    {
        public ResponseStatus ResponseStatus { get; set; }

        public ResponseData ResponseData { get; set; }
    }
}
