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
            try
            {
                if (!CommonTool.CommonHelper.IsMobile(request.PhoneNO))
                {
                    throw new Exception(string.Format("wrong phone{0}", request.PhoneNO));
                }
                if (DAL.CheckCodeHelper.GetLastInsertCode(request.PhoneNO) == request.CheckCode)
                {
                    //sucess
                    
                }


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