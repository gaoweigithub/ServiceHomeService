using System;
using ServiceStack;
using ServiceHome.Model.Common;
namespace ServiceHome.Services
{
    public class CheckAndLogin : BaseService<CheckAndLoginRequest, CheckAndLoginResponse>
    {
        public override CheckAndLoginResponse Excute(CheckAndLoginRequest request)
        {
            CheckAndLoginResponse response = null;
            bool result = true;
            try
            {
                if (!CommonTool.CommonHelper.IsMobile(request.PhoneNO))
                {
                    throw new Exception(string.Format("wrong phone{0}", request.PhoneNO));
                }
                if (DAL.CheckCodeHelper.GetLastInsertCode(request.PhoneNO) == request.CheckCode)
                {
                    //sucess
                    if (!DAL.UserHelper.ifExistsUser(request.PhoneNO))
                    {
                        result = DAL.UserHelper.AddUser(new ServiceHomeDB.USERS
                        {
                            CT = DateTime.Now,
                            USERNAME = request.PhoneNO,
                            STATE = 0,
                            PHONE = request.PhoneNO,
                            PASSWORD = request.CheckCode,
                            LASTUPDATETIME = DateTime.Now,
                        });
                    }
                    //设置结束
                    DAL.CheckCodeHelper.SetCheckFinishAndUpdateUser(request.PhoneNO, request.CheckCode);
                }
                else
                {
                    result = false;
                }
                response = new CheckAndLoginResponse
                {
                    ResponseStatus = new Model.Common.ResponseStatus
                    {
                        isSuccess = result
                    }
                };
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("error", ex.Message);
                response = new CheckAndLoginResponse
                {
                    ResponseStatus = new Model.Common.ResponseStatus
                    {
                        isSuccess = false
                    }
                };
            }

            return response;

        }
    }
    [Route("/CheckAndLogin", "POST")]
    public class CheckAndLoginRequest : RequestBase
    {
        public string PhoneNO { get; set; }
        public string CheckCode { get; set; }
    }
    public class CheckAndLoginResponse : ResponseBase
    {

    }
}