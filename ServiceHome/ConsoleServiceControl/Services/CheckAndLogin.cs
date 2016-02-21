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
            long userid = -1;
            bool isExsits = true;
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
                        isExsits = false;
                        result = DAL.UserHelper.AddUser(new ServiceHomeDB.USERS
                        {
                            CT = DateTime.Now,
                            USERNAME = request.PhoneNO,
                            STATE = 0,
                            PHONE = request.PhoneNO,
                            PASSWORD = request.CheckCode,
                            LASTUPDATETIME = DateTime.Now,
                        }, out userid);
                    }
                    //设置结束
                    DAL.CheckCodeHelper.SetCheckFinishAndUpdateUser(request.PhoneNO, request.CheckCode, isExsits, ref userid);

                    DAL.UserHelper.UpdateCache(userid, request.CheckCode);

                }
                else
                {
                    result = false;
                }
                response = new CheckAndLoginResponse
                {
                    USERID= userid.ToString(),
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
    [Route("/CheckAndLogin")]
    public class CheckAndLoginRequest : RequestBase
    {
        public string PhoneNO { get; set; }
        public string CheckCode { get; set; }
    }
    public class CheckAndLoginResponse : ResponseBase
    {
        /// <summary>
        /// 数据库表中的user主键
        /// </summary>
        public string USERID { get; set; }
    }
}