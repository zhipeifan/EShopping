using EShopping.Entity.Request;
using EShopping.Entity.Response.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopping.Common;
using EShopping.Common.Enums;
using EShopping.Entity.Response;
using EShopping.Common;

namespace EShopping.BusinessService.SelectProduct
{
   public  class LoginService
    {
       public static UserDTO LoginUser(UserDTO user)
       {

           CommonRequest request = new CommonRequest
           {
               payload = user.ReplcaceRequest<UserDTO>()
           };

           var response = ServiceRequestClient.PostRquest(ServicesEnum.verifyOtherLogin, JsonConvert.SerializeObject(request));
           if (response == null)
               return null;
           var data = response.ToEntity<VerifyOtherLoginResponseDTO>();
           if (data.responseData == null)
               return new UserDTO();

           return data.responseData;
       }

       /// <summary>
       /// 查询个人信息
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static UserDTO LoadUserInfo(int userId)
       {
            QueryUserInfoRequest request = new QueryUserInfoRequest
           {
              userId=userId
           };

           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryUserInfo, request.FormatRequest<QueryUserInfoRequest>());
           if (response == null)
               return null;
           var data = response.ToEntity<VerifyOtherLoginResponseDTO>();
           if (data.responseData == null)
               return new UserDTO();

           return data.responseData;
       }

       /// <summary>
       /// 收货地址列表
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static List<AddressDTO> LoadAddressList(int userId)
       {
           QueryUserAddressListRequest requst = new QueryUserAddressListRequest
           {
               userId=userId
           };

           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryUserAddressList, requst.FormatRequest<QueryUserAddressListRequest>());
           if (string.IsNullOrEmpty(response))
               return new List<AddressDTO>();
           var data = response.ToEntity<QueryUserAddressListResponse>();
           if (data == null||data.responseData==null)
               return new List<AddressDTO>();
           return data.responseData;
       }

       /// <summary>
       /// 操作收货地址
       /// </summary>
       /// <param name="dto"></param>
       public static void ModifyAddress(AddressDTO dto)
       {
         ServiceRequestClient.PostRquest(ServicesEnum.handleUserAddress, dto.FormatRequest<AddressDTO>());
       }

       /// <summary>
       /// 获取金榜排行
       /// </summary>
       /// <returns></returns>
       public static List<UserDTO> LoadGoldList()
       {
           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryUserByIntegralTop, "");
           if (string.IsNullOrEmpty(response))
               return new List<UserDTO>();
           var data = response.ToEntity<QueryUserByIntegralTopResponse>();
           if (data == null || data.responseData == null)
               return new List<UserDTO>();
           return data.responseData;
       }

       /// <summary>
       /// 签到规则
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static SigDTO LoadSigHistoryList(int userId)
       {
           QuerySignConfigRequest request = new QuerySignConfigRequest
           {
               userId=userId
           };

           var response = ServiceRequestClient.PostRquest(ServicesEnum.querySignConfig, request.FormatRequest<QuerySignConfigRequest>());
           if (string.IsNullOrEmpty(response))
               return null;
           var data = response.ToEntity<QuerySignConfigResponse>();
           if (data == null || data.responseData == null)
               return null;
           return data.responseData;
       }

       /// <summary>
       /// 添加签到
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="signIntegral"></param>
       /// <param name="signCount"></param>
       /// <param name="signLastTime"></param>
       /// <param name="redPacket"></param>
       public static bool AddSign(int userId)
       {
           var SignConfig = LoadSigHistoryList(userId);
           if (SignConfig == null || SignConfig.days == 0)
               return false;


           //判断是否累计签到,如果连续签到则加1，否则归1
           if (SignConfig.signLastTime.ConvertToPublicDateFormat().AddDays(1).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
           {
               SignConfig.signCount += 1;
           }
           else
           {
               SignConfig.signCount = 1;
           }
           SignConfig.signLastTime = DateTime.Now.AddDays(-1).DateTimeToUnixTimestamp();
           //本次增加的签到积分
           int addsignintegral = SignConfig.integralDelta * SignConfig.signCount;
           if (addsignintegral > SignConfig.integral)
           {
               addsignintegral = SignConfig.integral;
           }

           //计算增加后自己的实际积分
           SignConfig.signIntegral = SignConfig.signIntegral + addsignintegral;
           //签到时间
           //SignConfig.signLastTime =Public_Function.DateTimeToUnixTimestamp( DateTime.Now);
           SignConfig.redPacket = 0;
           //计算是否有红包
           if (SignConfig.signCount % SignConfig.days == 0)
           {
               Random rand = new Random();
               SignConfig.redPacket = rand.Next(10, 50) / 10;
           }


           AddSignRequest request = new AddSignRequest()
           {
               redPacket = SignConfig.redPacket,
               signCount = SignConfig.signCount,
               signIntegral = SignConfig.signIntegral,
               signLastTime = SignConfig.signLastTime,
               userId = userId
           };

           ServiceRequestClient.PostRquest(ServicesEnum.addSign, request.FormatRequest<AddSignRequest>());

           return true;
       }

       /// <summary>
       /// 加载钱包信息
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static List<WalletDTO> LoadWallet(int userId)
       {
           QueryWalletRequest request = new QueryWalletRequest() {  userId=userId};

           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryWallet, request.FormatRequest<QueryWalletRequest>());
           if (string.IsNullOrEmpty(response))
               return null;
           var data = response.ToEntity<QueryWalletResponse>();
           if (data == null || data.responseData == null)
               return null;
           return data.responseData;
       }

       /// <summary>
       /// VIP等级规则查询
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static PrivateLevelConfigDTO  QueryPrivateLevelConfig(int userId)
       {
           QueryLevelConfigRequest request = new QueryLevelConfigRequest() { userId = userId };

           var response = ServiceRequestClient.PostRquest(ServicesEnum.queryLevelConfig, request.FormatRequest<QueryLevelConfigRequest>());
           if (string.IsNullOrEmpty(response))
               return null;
           var data = response.ToEntity<QueryLevelConfigResponse>();
           if (data == null || data.responseData == null)
               return null;
           return data.responseData;
       }

       /// <summary>
       /// 积分兑换
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="integralCount"></param>
       public static void ExchangeIntegral(int userId,int integralCount)
       {
           ExchangeIntegralRequest request = new ExchangeIntegralRequest
           {
               exchangeIntegral = integralCount,
               exchangeMoney = integralCount / 1000,
               userId = userId
           };
           ServiceRequestClient.PostRquest(ServicesEnum.exchangeIntegral, request.FormatRequest<ExchangeIntegralRequest>());
       }


    }
}
