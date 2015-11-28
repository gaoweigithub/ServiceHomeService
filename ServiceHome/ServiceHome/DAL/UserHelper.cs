﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ServiceHome.DAL
{
    public class UserHelper
    {
        public static string cache_UserKeyHead = "PHO_";
        public static bool CheckPermmisson(string phoneNO, string checkCode)
        {
            string phoneNo_Key = cache_UserKeyHead + phoneNO;
            try
            {
                if (CommonTool.MemoryCacheHelper.IsExist(phoneNo_Key.Trim()))
                {
                    return (CommonTool.MemoryCacheHelper.GetValue(phoneNo_Key) as string) == checkCode;
                }
                else
                {
                    ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();
                    bool result = db.USER.Select(a => a.USERNAME == phoneNO && a.PASSWORD == checkCode).Count() > 0 ? true : false;
                    if (result)
                    {
                        //add to cache  30minutes out date
                        CommonTool.MemoryCacheHelper.Set(phoneNo_Key, checkCode, DateTimeOffset.Now + new TimeSpan(0, 30, 0));
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error(string.Format("CheckPermmisson:{0},{1}", phoneNO, checkCode), ex.Message);
                return false;
            }

        }
        /// <summary>
        /// 是否存在该用户
        /// </summary>
        /// <param name="PhoneNO"></param>
        /// <returns></returns>
        public static bool ifExistsUser(string PhoneNO)
        {
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                var result = db.USER.Select(a => a.PHONE == PhoneNO);
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("ifExistsUser", ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddUser(ServiceHomeDB.USER model)
        {
            int i = 0;
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                db.USER.Add(model);
                i = db.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("AddUser", ex.Message);
            }

            return i > 0 ? true : false;
        }
    }
}