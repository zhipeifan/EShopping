using EShopping.Common;
using EShopping.Common.Enums;
using EShopping.Entity.Request;
using EShopping.Entity.UIDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopping.Common;
using EShopping.Entity.Response.DTO;
using Newtonsoft.Json;
using EShopping.Entity.Response;
using System.IO;

namespace EShopping.BusinessService.SelectProduct
{
    public class ShareService
    {

        public static SayDTO LoadSayList()
        {
            SayDTO say = new SayDTO();

            var payload = new QueryNewsListRequest
            {
                type = 1//小编说
            };

            var response = ServiceRequestClient.PostRquest(ServicesEnum.queryProductListByType, payload.FormatRequest<QueryNewsListRequest>());
            if (response != null)
            {
                SayResponseDTO _say = response.ToEntity<SayResponseDTO>(); 
                if(_say!=null)
                {
                    say.Content = _say.content;
                }
            }

            var indexData=ProductService.LoadIndexData();
            if (indexData != null && indexData.RecommedVOs!=null&&indexData.RecommedVOs.Count>0)
            {
                say.Products = indexData.RecommedVOs.Where(x=>x.Product!=null).Select(x=>x.Product).ToList();
            }
            return say;
        }

        /// <summary>
        /// 查询晒单分享
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="prefecture"></param>
        /// <param name="searchPram"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<QueryShareInfoListDTO> ShareList(int pageIndex, int PageSize, int userId, int byDetailOrMy=1)
        {
            var payload = new QueryShareInfoListRequest
            {
                currentPage = pageIndex,
                pageSize = PageSize,
                byDetailOrMy = byDetailOrMy,
                userId = userId,
                 isShowAll=true
            };

            if(payload.byDetailOrMy==2)
            {
                payload.isShowAll = false;
            }

            CommonRequest request = new CommonRequest
            {
                payload = payload.ReplcaceRequest<QueryShareInfoListRequest>()
            };

            var responseStr = ServiceRequestClient.PostRquest(ServicesEnum.queryShareInfoList, JsonConvert.SerializeObject(request));
            if (string.IsNullOrEmpty(responseStr))
                return new List<QueryShareInfoListDTO>();
            var response = responseStr.ToEntity<QueryShareInfoListResponse>();
            if (response != null && response.responseData != null)
                return response.responseData;
            return new List<QueryShareInfoListDTO>();
        }


        public void LoadSpellbuyRecordList(int spellbuyRecordId,int pageIndex,int PageSize)
        {
            QuerySpellbuyrecordListResponse quest = new QuerySpellbuyrecordListResponse()
            {

            };

        }


        public static string UploadAttachment(Dictionary<string, Stream> streams)
        {
            var response = ServiceRequestClient.PostRquestAndAttachment(streams);
            if(!string.IsNullOrEmpty(response))
            {
                var data = response.ToEntity<UploadImageResponse>();
                if(data!=null&&data.responseData!=null&&data.responseData.imagePathList!=null&&data.responseData.imagePathList.Count>0)
                return data.responseData.imagePathList[0];
            }
            return "";
        }

        /// <summary>
        /// 批量上传图片
        /// </summary>
        /// <param name="streams"></param>
        /// <returns></returns>
        public static List<string> BatchUploadAttachment(Dictionary<string, Stream> streams)
        {
            var response = ServiceRequestClient.PostRquestAndAttachment(streams);
            if (!string.IsNullOrEmpty(response))
            {
                var data = response.ToEntity<UploadImageResponse>();
                if (data != null && data.responseData != null && data.responseData.imagePathList != null && data.responseData.imagePathList.Count > 0)
                    return data.responseData.imagePathList;
            }
            return new List<string>();
        }
    }
}
