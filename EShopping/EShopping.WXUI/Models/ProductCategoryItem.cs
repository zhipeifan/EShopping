using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopping.WXUI.Models
{
    /// <summary>
    /// 商品页搜索条件
    /// </summary>
    public class ProductCategoryItem
    {
        public int categoryId{get;set;}

        public int areaId{get;set;}

        public int tcategoryId{get;set;}

        public int pageIndex{get;set;}

        public int pageSize{get;set;}

    }
}