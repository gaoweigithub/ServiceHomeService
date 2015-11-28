using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;
namespace ServiceHome.Services
{
    public class CheckAndLogin
    {
        public object Any(CheckAndLoginRequest request)
        {
            CheckAndLoginResponse response = null;
            bool result = false;
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
                        result = DAL.UserHelper.AddUser(new ServiceHomeDB.USER
                        {
                            CT = DateTime.Now,
                            USERNAME = request.PhoneNO,
                            STATE = 0,
                            PHONE = request.PhoneNO,
                            PASSWORD = request.CheckCode,
                            LASTUPDATETIME = DateTime.Now,
                        });
                    }
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
    public class CheckAndLoginRequest : RequestBase
    {
        public string PhoneNO { get; set; }
        public string CheckCode { get; set; }
    }
    public class CheckAndLoginResponse : ResponseBase
    {

    }
}