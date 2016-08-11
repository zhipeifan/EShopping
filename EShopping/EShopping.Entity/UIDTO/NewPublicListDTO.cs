using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.UIDTO
{
    public class NewPublicListDTO
    {
        public List<ProductDTO> Products { get; set; }
        public List<ProductDTO> HistoryProducts { get; set; }
    }
}
