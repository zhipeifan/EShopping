using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class RecommedVOs
    {
        public List<RecommedProduct> RecommedVos { get; set; }
    }

    public class RecommedProduct
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public ProductDTO Product { get; set; }
    }
}
