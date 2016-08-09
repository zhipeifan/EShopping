 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class ResponseData
    {
        public ResponseData()
        {
            ProductVOs = new List<ProductVOs>();
            RecommedVOs = new List<RecommedProduct>();
            IndexImgVOs = new List<IndexImgProduct>();
        }
        public long SystemTime { get; set; }

        public List<ProductVOs> ProductVOs { get; set; }

        public List<RecommedProduct> RecommedVOs { get; set; }

        public List<IndexImgProduct> IndexImgVOs { get; set; }
    }
}
