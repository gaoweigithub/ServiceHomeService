using System;
using System.Linq;
namespace ServiceHome.DAL
{
    public class UserHelper
    {
        public static string cache_UserKeyHead = "CC_";
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
        public static bool AddUser(ServiceHomeDB.USERS model, out long id)
        {
            id = -1;
            int i = 0;
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                db.USERS.Add(model);
                i = db.SaveChanges();
                id = model.USERID;
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("AddUser", ex.Message);
            }

            return i > 0 ? true : false;
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="checkcode"></param>
        public static void UpdateCache(long userID, string checkcode)
        {
            CommonTool.MemoryCacheHelper.Set(cache_UserKeyHead + userID.ToString(), checkcode);
        }
    }
}