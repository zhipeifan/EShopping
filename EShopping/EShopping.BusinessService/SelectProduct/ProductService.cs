using EShopping.Common;
using EShopping.Common.Enums;
using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopping.Common;
using EShopping.Entity.Request;
using Newtonsoft.Json;
using EShopping.Entity.Response;
using EShopping.Entity.UIDTO;   

namespace EShopping.BusinessService.SelectProduct
{
   public class ProductService
    {
       /// <summary>
       /// 首页数据查询
       /// </summary>
       /// <returns></returns>
       public static ResponseData LoadIndexData()
       {
           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryIndex, "");
           if (response == null)
               return null;
           var data= response.ToEntity<Response>();
           return data.ResponseData;
       }

       /// <summary>
       /// Product 详情页
       /// </summary>
       /// <returns></returns>
       public static ProductDTO LoadProductDetail(int id, int spellBuyId,int userId=0)
       {
           var payload = new payload()
           {
               productId = id,
               spellbuyproductId = spellBuyId,
               userId = userId
           };

           string _payload = JsonConvert.SerializeObject(payload);
           _payload = _payload.Replace("\"","'");
           _payload = _payload.Replace("\\", "");

           ProductDetailRequest request = new ProductDetailRequest();
           request.payload = _payload;

           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryProductDetail, JsonConvert.SerializeObject(request));
           if (response == null)
               return null;
           var data = response.ToEntity<ProductDetailResponse>();
           return data.ResponseData;
       }

       /// <summary>
       /// 指定商品参与列表
       /// </summary>
       /// <param name="spellBuyProductId"></param>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public static List<AttendUserDTO> LoadAttendUsers(int spellBuyProductId,int pageIndex,int pageSize)
       {
           var payload = new AttendUserRequestDTO
                {
                    currentPage = pageIndex,
                    pageSize = pageSize,
                    spellbuyproductId = spellBuyProductId
                };


           var response = ServiceRequestClient.PostRquest(ServicesEnum.querySpellbuyrecordList, payload.FormatRequest<AttendUserRequestDTO>());
           if (response == null)
               return null;
           var data = response.ToEntity<QuerySpellbuyrecordListResponse>();
           return data.ResponseData;
       }

       /// <summary>
       /// 全部商品查询
       /// </summary>
       /// <param name="productTypeId"></param>
       /// <param name="prefecture"></param>
       /// <param name="searchPram"></param>
       /// <param name="pageIndex"></param>
       /// <param name="PageSize"></param>
       /// <returns></returns>
       public static List<ProductDTO> LoadProductByType(int productTypeId, int prefecture, int searchPram, int pageIndex, int PageSize)
       {
           var payload = new QueryProductListByType
             {
                 currentPage = pageIndex,
                 pageSize = PageSize,
                 prefecture = prefecture,
                 productTypeId = productTypeId,
                 searchParam = searchPram
             };


           CommonRequest request = new CommonRequest
           {
               payload = payload.ReplcaceRequest<QueryProductListByType>()
           };

           //queryProductListByType

           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryProductListByType, JsonConvert.SerializeObject(request));
           if (response == null)
               return new List<ProductDTO>();
           var data = response.ToEntity<QueryProductListByTypeResponse>();
           if (data!=null&&data.responseData == null)
               return new List<ProductDTO>();
           return data.responseData.productVOs ;
       }


       public static List<ProductDTO> LoadNewPublic(int pageIndex,int pageSize)
       {
           QueryPublishingListRequestDTO payload = new QueryPublishingListRequestDTO
           {
                PageIndex=pageIndex,
                PageSize=pageSize
           };


           QueryPublishingListRequest request = new QueryPublishingListRequest
           {
               payload = payload.ReplcaceRequest<QueryPublishingListRequestDTO>()
           };

           //queryProductListByType

           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryPublishingList, JsonConvert.SerializeObject(request));
           if (response == null)
               return new List<ProductDTO>();
           var data = response.ToEntity<QueryPublishingListResponse>();
           if (data.responseData == null)
               return new List<ProductDTO>();
           return data.responseData.productVOs;
       }

       /// <summary>
       /// 商品类型
       /// </summary>
       /// <returns></returns>
       public static List<ProductTypeDTO> LoadProductType()
       {
           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryProductType,"");
           if (response == null)
               return new List<ProductTypeDTO>();
           var data = response.ToEntity<QueryProductTypeResponse>();
           if (data.responseData == null)
               return new List<ProductTypeDTO>();
           return data.responseData;
       }

       /// <summary>
       /// 推荐商品列表
       /// </summary>
       /// <returns></returns>
       public static List<RecommedProduct> LoadRecommandProductList()
       {
           CommonRequest request = new CommonRequest() {  payload=""};
           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryRecommendList, request.ReplcaceRequest<CommonRequest>());
           if (response == null)
               return new List<RecommedProduct>();
           var data = response.ToEntity<QueryRecommendListReponse>();
           if (data.responseData == null)
               return new List<RecommedProduct>();
           return data.responseData;
       }

       /// <summary>
       /// 赞一个
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="shareId"></param>
       public static void SendUpCount(int userId,int shareId)
       {
           SendUpCountRequest request = new SendUpCountRequest
           {
                shareInfoId=shareId,
                 userId=userId
           };

           ServiceRequestClient.PostRquest(ServicesEnum.sendUpCount, request.FormatRequest<SendUpCountRequest>());
       }

       /// <summary>
       /// 商品搜索
       /// </summary>
       /// <param name="key"></param>
       /// <param name="pageIndex"></param>
       /// <param name="PageSize"></param>
       /// <returns></returns>
       public  static List<ProductDTO> SearchProducts(string key,int pageIndex,int PageSize)
       {
           SearchReqeustDTO payload = new SearchReqeustDTO
           {
               currentPage = pageIndex,
               keyword = key,
               pageSize = PageSize
           };

           var response = ServiceRequestClient.PostRquest(ServicesEnum.searchProductList, payload.FormatRequest<SearchReqeustDTO>());
           if (response == null)
               return new List<ProductDTO>();
           var data = response.ToEntity<QueryPublishingListResponse>();
           if (data.responseData == null)
               return new List<ProductDTO>();
           return data.responseData.productVOs;
       }

       /// <summary>
       ///添加晒单
       /// </summary>
       /// <param name="dto"></param>
       public static void AddShareProductOrder(ShareProductDTO dto)
       {
          ServiceRequestClient.PostRquest(ServicesEnum.addShareInfo, dto.FormatRequest<ShareProductDTO>());
           //if (response == null)
           //    return new List<ProductDTO>();
           //var data = response.ToEntity<QueryPublishingListResponse>();
           //if (data.responseData == null)
           //    return new List<ProductDTO>();
           //return data.responseData.productVOs;
       }
    }
}
