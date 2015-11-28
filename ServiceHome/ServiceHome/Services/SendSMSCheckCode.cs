﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;
namespace ServiceHome.Services
{
    public class SendSMSCheckCode : BaseService
    {
        public object Any(SendSMSCheckCodeRequest request)
        {
            SendSMSCheckCodeResponse response = null;
            try
            {
                //check
                if (!CommonTool.CommonHelper.IsMobile(request.PhoneNO))
                {
                    throw new Exception("wrong phoneNO:" + request.PhoneNO);
                }

                if (DAL.CheckCodeHelper.IsOutDate(request.PhoneNO))
                {
                    throw new Exception("phoneno:{0} send code twice in 3 minutes");
                }

                //set all check finished
                DAL.CheckCodeHelper.SetCheckFinish(request.PhoneNO.Trim());

                //code
                string randomCode = CommonTool.CommonHelper.GetNextCheckCode();

                if (DAL.CheckCodeHelper.InsertCheckCode(request.PhoneNO, randomCode))
                {
                    throw new Exception(request.PhoneNO + ";insert checkcode into db error");
                }
                //send
                bool result = CommonTool.SMSHelper.SendRegisterCheckCode(request.PhoneNO, randomCode);

                //log
                DAL.AddOperationLog.Error(string.Format("发送验证码:phoneno:{0},checkcode:{1}", request.PhoneNO, randomCode), result ? "成功" : "失败");

                //return
                response = new SendSMSCheckCodeResponse
                {
                    ResponseStatus = new Model.Common.ResponseStatus
                    {
                        isSuccess = result
                    }
                };
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("发送验证码错误", ex.Message);
                //exception
                response = new SendSMSCheckCodeResponse
                {
                    ResponseStatus = new Model.Common.ResponseStatus
                    {
                        isSuccess = false
                    }
                };
            }
            //return result
            return response;
        }
    }

    public class SendSMSCheckCodeRequest : RequestBase
    {
        public string PhoneNO { get; set; }
    }
    public class SendSMSCheckCodeResponse : ResponseBase
    {

    }
}