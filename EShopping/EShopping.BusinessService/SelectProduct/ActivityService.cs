using EShopping.Common.Enums;
using EShopping.Entity.Request;
using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopping.Common;
using EShopping.Entity.Response;

namespace EShopping.BusinessService.SelectProduct
{
    public class ActivityService
    {
        /// <summary>
        /// 获取最新获取或小编说
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<ActivityDTO> LoadActivity(SearchActivityTypeEnum type)
        {
            var payload = new QueryNewsListRequest
            {
                type = (int)type
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.queryNewsList, payload.FormatRequest<QueryNewsListRequest>());
            if (response == null)
                return new List<ActivityDTO>();
            var data = response.ToEntity<QueryNewsListResponse>();
            if (data.responseData == null)
                return new List<ActivityDTO>();
            return data.responseData;
        }

        /// <summary>
        /// 获取活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ActivityDTO LoadActivityDetial(int id)
        {
            QueryNewsDetailRequest request=new QueryNewsDetailRequest(){
                 newsId=id
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.queryNewsDetail, request.FormatRequest<QueryNewsDetailRequest>());
            if (response == null)
                return new ActivityDTO();
            var data = response.ToEntity<QueryNewsDetailResponse>();
            if (data.responseData == null)
                return new ActivityDTO();
            return data.responseData;
        }

        /// <summary>
        /// 秘密团列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="queryType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public  static List<ProductDTO> LoadSecretList(int userId,int queryType,int pageIndex,int PageSize)
        {
            if (queryType > 0)
                userId = 0;

            QueryProductListByPrivateRequest request = new QueryProductListByPrivateRequest
            {
                currentPage = pageIndex,
                pageSize = PageSize,
                privateType1Or2 = queryType,
                userId = userId
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.queryProductListByPrivate, request.FormatRequest<QueryProductListByPrivateRequest>());
            if(string.IsNullOrEmpty(response)) 
                return new List<ProductDTO>();

            var data = response.ToEntity<QueryProductListByPrivateResponse>();
            if (data == null ||data.responseData==null|| data.responseData.productVOs == null)
                return new List<ProductDTO>();
            return data.responseData.productVOs;
        }

        /// <summary>
        /// 预创建秘密团
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        public static AddSpellbuyproductResponseDTO CreateSecret(int productId, int userId, int type)
        {
            AddSpellbuyproductRequest request = new AddSpellbuyproductRequest
            {
                privateType1Or2 = type,
                productId = productId,
                userId = userId
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.addSpellbuyproduct, request.FormatRequest<AddSpellbuyproductRequest>());
            if (string.IsNullOrEmpty(response))
                return null;
            var data = response.ToEntity<AddSpellbuyproductResponse>();
            if (data == null || data.responseData == null)
                return null;
            return data.responseData;
        }

        /// <summary>
        /// 修改完善秘密团信息
        /// </summary>
        /// <param name="spellbuyproductId"></param>
        /// <param name="privateMemberCount"></param>
        /// <param name="privateCode"></param>
        public static void ModifySecret(int spellbuyproductId, int privateMemberCount, string privateCode)
        {
            UpdateSpellbuyproductRequest request = new UpdateSpellbuyproductRequest
            {
                privateCode = privateCode,
                privateMemberCount = privateMemberCount,
                spellbuyproductId = spellbuyproductId
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.updateSpellbuyproduct, request.FormatRequest<UpdateSpellbuyproductRequest>());
        }

        /// <summary>
        /// 我的中奖记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<WinnerProductDTO> LoadMyWinnerList(int userId)
        {
            QueryMyWinnersListRequest request = new QueryMyWinnersListRequest()
            {
                 userId=userId
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.queryMyWinnersList, request.FormatRequest<QueryMyWinnersListRequest>());
            if (string.IsNullOrEmpty(response))
                return new List<WinnerProductDTO>();

            var data = response.ToEntity<QueryMyWinnersListResponse>();
            if (data == null || data.responseData == null)
                return new List<WinnerProductDTO>();
            return data.responseData;
        }

        /// <summary>
        /// 中奖列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<WinnerProductDTO> LoadWinnerList()
        {
            var response = ServiceRequestClient.PostRquest(ServicesEnum.queryWinnersList, "");
            if (string.IsNullOrEmpty(response))
                return new List<WinnerProductDTO>();
            var data = response.ToEntity<QueryMyWinnersListResponse>();
            if (data == null || data.responseData == null)
                return new List<WinnerProductDTO>();
            return data.responseData;
        }
    }
}
