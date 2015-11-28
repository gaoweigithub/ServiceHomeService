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
                    || request is Services.CheckAndLoginRequest)
                {
                    //发送验证码和注册接口不需要权限验证
                    return Excute(request);
                }

                //否则需要权限验证 
                bool check = DAL.UserHelper.CheckPermmisson(request.requestHeader.UserID, request.requestHeader.UserPwd);
                if (!check)
                {
                    return new TResponse() { ResponseStatus = new Model.Common.ResponseStatus { isSuccess = false } };
                }
                return Excute(request);
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error(request.requestHeader.interfaceName, ex.Message + CommonTool.XmlHelper.Serializer(request));
                return new TResponse() { ResponseStatus = new Model.Common.ResponseStatus { isSuccess = false } };
            }
        }
        public abstract TResponse Excute(TRequest request);
    }
}