using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class IndexImgVOs
    {
        public List<IndexImgProduct> IndexProducts { get; set; }
    }

    public class IndexImgProduct
    {
        public int Id{get;set;}

        public string proImg{get;set;}
        public string proUrl{get;set;}

        public int status{get;set;}

        public string title{get;set;}

        public ProductVOs Product { get; set; }
    }
}
