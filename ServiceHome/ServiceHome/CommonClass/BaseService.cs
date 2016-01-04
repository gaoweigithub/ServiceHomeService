using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
namespace ServiceHome
{
    public abstract class BaseService<TRequest, TResponse> : Service
        where TRequest : Model.Common.RequestBase, new()
        where TResponse : Model.Common.ResponseBase, new()
    {
        public TResponse Any(TRequest request)
        {
            try
            {
                if (request is Services.SendSMSCheckCodeRequest
                    || request is Services.CheckAndLoginRequest
                    || request is Services.GetOpenedCityRequest
                    || request is Services.GetCityServiceRequest)
                {
                    //发送验证码和注册接口不需要权限验证
                    return Excute(request);
                }
                int userID = -1;
                if (!string.IsNullOrWhiteSpace(Request.GetParam("userid")) && int.TryParse(Request.GetParam("userid").Trim(), out userID))
                {
                    string accesscode = Request.GetParam("acode");
                    request.requestHeader = new Model.Common.RequestHead { UserID = userID, AccessCode = accesscode };
                    //否则需要权限验证 
                    bool check = DAL.UserHelper.CheckPermmisson(request.requestHeader.UserID, request.requestHeader.AccessCode);
                    if (!check)
                    {
                        return new TResponse() { ResponseStatus = new Model.Common.ResponseStatus { isSuccess = false } };
                    }
                    return Excute(request);
                }
                else
                {
                    throw new Exception("无效userid");
                }


            }
            catch (Exception ex)
            {
                //DAL.AddOperationLog.Error(request.requestHeader.interfaceName, ex.Message + CommonTool.XmlHelper.Serializer(request));
                return new TResponse()
                {
                    ResponseStatus = new Model.Common.ResponseStatus
                    {
                        isSuccess = false,
                        ErrorList = new List<string> { ex.Message, ex.StackTrace }
                    }
                };
            }
        }
        public abstract TResponse Excute(TRequest request);
    }
}