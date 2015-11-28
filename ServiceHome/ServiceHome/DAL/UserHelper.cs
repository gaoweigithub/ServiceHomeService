using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHome.DAL
{
    public class UserHelper
    {
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