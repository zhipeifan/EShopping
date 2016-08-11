using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.UIDTO
{
    public class SayDTO
    {
        public string Content { get; set; }

        public List<ProductDTO> Products { get; set; }
    }
}
