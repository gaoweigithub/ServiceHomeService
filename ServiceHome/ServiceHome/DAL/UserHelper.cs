using System;
using System.Linq;
namespace ServiceHome.DAL
{
    public class UserHelper
    {
        public static string cache_UserKeyHead = "CC_";
        //public static bool CheckPermmisson(string phoneNO, string checkCode)
        //{
        //    string phoneNo_Key = cache_UserKeyHead + phoneNO;
        //    try
        //    {
        //        if (CommonTool.MemoryCacheHelper.IsExist(phoneNo_Key.Trim()))
        //        {
        //            return (CommonTool.MemoryCacheHelper.GetValue(phoneNo_Key) as string) == checkCode;
        //        }
        //        else
        //        {
        //            ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();
        //            bool result = db.USERS.Select(a => a.USERNAME == phoneNO && a.PASSWORD == checkCode).Count() > 0 ? true : false;
        //            if (result)
        //            {
        //                //add to cache  30minutes out date
        //                CommonTool.MemoryCacheHelper.Set(phoneNo_Key, checkCode, DateTimeOffset.Now + new TimeSpan(0, 30, 0));
        //            }
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DAL.AddOperationLog.Error(string.Format("CheckPermmisson:{0},{1}", phoneNO, checkCode), ex.Message);
        //        return false;
        //    }
        //}
        /// <summary>
        /// 权限检查 true=check wright false=error
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        public static bool CheckPermmisson(int userid, string checkCode)
        {
            if (userid < 1 || string.IsNullOrWhiteSpace(checkCode))
            {
                return false;
            }
            string checkCode_key = cache_UserKeyHead + userid.ToString();
            try
            {
                if (CommonTool.MemoryCacheHelper.IsExist(checkCode_key.Trim()))
                {
                    return (CommonTool.MemoryCacheHelper.GetValue(checkCode_key) as string) == checkCode;
                }
                else
                {
                    ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();
                    bool result = db.USERS.Select(a => a.USERID == userid && a.PASSWORD == checkCode).Count() > 0 ? true : false;
                    if (result)
                    {
                        //add to cache  30minutes out date
                        CommonTool.MemoryCacheHelper.Set(checkCode_key, checkCode, DateTimeOffset.Now + new TimeSpan(0, 30, 0));
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error(string.Format("CheckPermmisson:{0},{1}", userid.ToString(), checkCode), ex.Message);
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

                var result = db.USERS.Where(u => u.USERNAME == PhoneNO).Count();
                if (result > 0)
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
        public static bool AddUser(ServiceHomeDB.USERS model)
        {
            int i = 0;
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                db.USERS.Add(model);
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