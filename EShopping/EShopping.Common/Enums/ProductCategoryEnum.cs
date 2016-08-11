using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Common.Enums
{
    public enum ProductCategoryEnum : int
    {
        /// <summary>
        /// 全部
        /// </summary>
        All=0,
        /// <summary>
        /// 新手
        /// </summary>
        NewUser = 1,
        /// <summary>
        /// 大众
        /// </summary>
        Public = 2,
        /// <summary>
        /// 土豪
        /// </summary>
        Tycoon = 3,
        /// <summary>
        /// 限购
        /// </summary>
        Limit = 4,
        /// <summary>
        /// 返现
        /// </summary>
       Return=5
    }

    public enum AreaEnum : int
    {
        /// <summary>
        /// 最新
        /// </summary>
        NewProduct = 0,
        /// <summary>
        /// 最快
        /// </summary>
        Fast = 1,
        /// <summary>
        /// 最热 
        /// </summary>
        Hot = 2

    }
}
