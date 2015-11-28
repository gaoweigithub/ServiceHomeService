using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHome.DAL
{
    public class ServicePlace
    {
        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ServiceHomeDB.SERVICEPLACES> GetServicePlaces(int userID)
        {
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                var result = db.SERVICEPLACES.Where(a => a.USERID == userID).Select(a => new ServiceHomeDB.SERVICEPLACES
                {
                    ADDR_TITLE = a.ADDR_TITLE,
                    CREATE_TIME = a.CREATE_TIME,
                    LAST_TIME = a.LAST_TIME,
                    PLACE_DETAIL = a.PLACE_DETAIL,
                    SERVICE_PLACE_ID = a.SERVICE_PLACE_ID,
                    PHONE = a.PHONE,
                    USERID = a.USERID
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("GetServicePlaces", ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <returns></returns>
        public bool DeleteServicePlace(int servicePlaceID)
        {
            try
            {
                int i = 0;
                using (ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities())
                {
                    string sql = "delete from SERVICEPLACES where SERVICE_PLACE_ID={0}";
                    i = db.Database.ExecuteSqlCommand(string.Format(sql, servicePlaceID));
                    db.SaveChanges();
                }
                return i > 0 ? true : false;
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("DeleteServicePlace", ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 新增地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddServicePlace(ServiceHomeDB.SERVICEPLACES model)
        {
            int servicePlaceID = -1;
            try
            {
                using (ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities())
                {
                    servicePlaceID = db.SERVICEPLACES.Add(model).SERVICE_PLACE_ID;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("AddServicePlace", ex.Message);
            }
            return servicePlaceID;
        }
    }
}