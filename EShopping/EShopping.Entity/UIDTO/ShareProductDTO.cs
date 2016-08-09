using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.UIDTO
{
    public class ShareProductDTO
    {
        public int shareInfoId { get; set; }

        public string FaceImg { get; set; }

        /// <summary>
        /// 添加还是修改：1:添加；2：修改
        /// </summary>
        public int addOrUpdate { get; set; }

        public int productId { get; set; }

        public int spellbuyproductId { get; set; }

        public int userId { get; set; }

        public string shareContent { get; set; }

        public string shareTitle { get; set; }

        public string shareImg1 { get; set; }
        public string shareImg2 { get; set; }
        public string shareImg3 { get; set; }
        public string shareImg4 { get; set; }
        public string shareImg5 { get; set; }


    }
}
