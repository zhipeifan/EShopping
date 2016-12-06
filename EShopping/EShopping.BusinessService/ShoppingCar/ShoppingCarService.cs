using EShopping.Entity.Request;
using EShopping.Entity.UIDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopping.Common;
using EShopping.Common.Enums;
using Newtonsoft.Json;
using EShopping.Entity.Response;
using EShopping.Entity.UIDTO.Enum;
using EShopping.Entity.Response.DTO;

namespace EShopping.BusinessService.ShoppingCar
{
    public class ShoppingCarService
    {
        //handleShoppingCart
        /// <summary>
        /// 操作购物车
        /// </summary>
        /// <param name="product"></param>
        /// <param name="userName"></param>
        public static void OperatShoppingProduct(ShoppingUIDTO product, string userName)
        {

            ShoppingProduct sproduct = new ShoppingProduct
            {
                buyCount = product.BuyCount,
                spellbuyproductId = product.SpellbuyproductId
            };
            ShoppingProductOperat operat = new ShoppingProductOperat
            {
                handleShoppingCartVOs = new List<ShoppingProduct>() { sproduct },
                updateOrDelete = (int)product.ShoppingOperatType,
                userName = userName
            };

            var payload=operat.ReplcaceRequest<ShoppingProductOperat>();
            HandleShoppingCartRequest rquest = new HandleShoppingCartRequest()
            {
                payload = payload,
                token = "2560c444-f025-4800-a477-b52d75b62ede"
            };

            ServiceRequestClient.PostRquest(ServicesEnum.handleShoppingCart, JsonConvert.SerializeObject(rquest));
        }

        /// <summary>
        /// 操作购物车
        /// </summary>
        /// <param name="product"></param>
        /// <param name="userName"></param>
        public static void BatchModifyShoppingCar(List<ShoppingUIDTO> products, string userName,ShoppingOperatTypeEnum operateType)
        {
            var shoppingItems = products.Select(x => new ShoppingProduct
            {
                buyCount = x.BuyCount,
                spellbuyproductId = x.SpellbuyproductId
            }).ToList();

            ShoppingProductOperat operat = new ShoppingProductOperat
            {
                handleShoppingCartVOs = shoppingItems,
                updateOrDelete = (int)operateType,
                userName = userName
            };

            var payload = operat.ReplcaceRequest<ShoppingProductOperat>();
            HandleShoppingCartRequest rquest = new HandleShoppingCartRequest()
            {
                payload = payload,
                token = "2560c444-f025-4800-a477-b52d75b62ede"
            };

            ServiceRequestClient.PostRquest(ServicesEnum.handleShoppingCart, JsonConvert.SerializeObject(rquest));
        }

        /// <summary>
        /// 获取购物车数据
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<QueryShoppingCarProduct> LoadShoppingList(string userName)
        {
            ShoppingUserInfo user = new ShoppingUserInfo
            {
                userName = userName
            };
            var payload = user.ReplcaceRequest<ShoppingUserInfo>();
            QueryShoppingCartListReuqest request = new QueryShoppingCartListReuqest
            {
                payload = payload,
                token = "2560c444-f025-4800-a477-b52d75b62ede"
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.queryShoppingCartList, JsonConvert.SerializeObject(request));
            if (string.IsNullOrEmpty(response))
                return new List<QueryShoppingCarProduct>();
            var data = response.ToEntity<QueryShoppingCartListResponse>();
            if (data == null || data.responseData == null || data.responseData.Count == 0)
                return new List<QueryShoppingCarProduct>();
            return data.responseData;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ip"></param>
        /// <param name="shoppingProducts"></param>
        public static SubmitOrderDTO CreateOrder(int userId, string ip, List<BuyProductVOs> shoppingProducts,string appId,TradeTypeEnum tradeType,string openId)
        {
            OrderRequest orderFilter = new OrderRequest()
            {
                buyProductVOs = shoppingProducts,
                ipAddress = ip,
                userId = userId,
                appId = appId,
                openId = openId,
                tradeType = tradeType.ToString()
            };

            CommonRequest request = new CommonRequest
            {
                payload = orderFilter.ReplcaceRequest<OrderRequest>()
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.submitOrder, JsonConvert.SerializeObject(request));

            if (string.IsNullOrEmpty(response))
                return new SubmitOrderDTO();

            var data = response.ToEntity<SubmitOrderResponse>();
            if (data == null || data.responseData == null)
                return new SubmitOrderDTO();
            return data.responseData;
        }

        /// <summary>
        /// 查询购买记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<BuyProductDTO> LoadBuyList(int userId, BuyTypeEnum type, int pageIndex, int pageSize)
        {
            QueryMyBuyListRequest request = new QueryMyBuyListRequest()
            {
                currentPage = pageIndex,
                pageSize = pageSize,
                status = (int)type,
                userId = userId
            };
           var response= ServiceRequestClient.PostRquest(ServicesEnum.queryMyBuyList, request.FormatRequest<QueryMyBuyListRequest>());
           if (string.IsNullOrEmpty(response))
               return new List<BuyProductDTO>();

           var data= response.ToEntity<QueryMyBuyListResponse>();
           if (data == null || data.responseData == null || data.responseData.productVOs == null)
               return new List<BuyProductDTO>();
           return data.responseData.productVOs;

        }
    }
}
