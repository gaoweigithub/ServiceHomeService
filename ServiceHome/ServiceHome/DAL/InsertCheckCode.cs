using System;
using System.Linq;
using System.Transactions;
namespace ServiceHome.DAL
{
    public class CheckCodeHelper
    {
        /// <summary>
        /// 判断是否重复验证码，三分钟之内  false=no  true=yes
        /// </summary>
        /// <param name="phoneNO"></param>
        /// <returns></returns>
        public static bool IsOutDate(string phoneNO)
        {
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                var dt = (from t in db.CHECKCODERECORD
                          where t.PHONENO == phoneNO && t.ISCHECKED == "F"
                          select t.CREATETIME).FirstOrDefault();

                if (dt == null || dt == DateTime.MinValue || (DateTime.Now - dt) > new TimeSpan(0, 3, 0))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error(" 判断是否重复验证码，三分钟之内", ex.Message);
            }

            return false;
        }
        /// <summary>
        /// 插入验证码
        /// </summary>
        /// <param name="PhoneNo"></param>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        public static bool InsertCheckCode(string PhoneNo, string checkCode)
        {
            int i = 0;
            bool result = false;
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                ServiceHomeDB.CHECKCODERECORD mdoel = new ServiceHomeDB.CHECKCODERECORD
                {
                    PHONENO = PhoneNo,
                    CHECKCODE = checkCode,
                    ISCHECKED = "F",
                    CREATETIME = DateTime.Now
                };
                db.CHECKCODERECORD.Add(mdoel);
                i = db.SaveChanges();
                result = i > 0 ? true : false;
                if (result)
                {
                    //add cache
                    CommonTool.MemoryCacheHelper.Set(UserHelper.cache_UserKeyHead + PhoneNo, checkCode);
                }
            }
            catch (Exception ex)
            {
                AddOperationLog.Error("写入验证码错误", ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 获取最新插入未验证的验证码
        /// </summary>
        /// <param name="PhoneNO"></param>
        /// <returns></returns>
        public static string GetLastInsertCode(string PhoneNO)
        {
            string code = string.Empty;
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                code = (from t in db.CHECKCODERECORD
                        where t.PHONENO == PhoneNO && t.ISCHECKED == "F"
                        orderby t.CREATETIME descending
                        select t.CHECKCODE).FirstOrDefault();
            }
            catch (Exception ex)
            {
                AddOperationLog.Error("获取验证码错误", PhoneNO + ex.Message);
            }
            return code;
        }
        /// <summary>
        /// 设置验证码验证结束并更新用户表字段
        /// </summary>
        public static void SetCheckFinishAndUpdateUser(string PhoneNO, string checkCode)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {

                    ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();
                    string sql1 = "update CHECKCODERECORD set ISCHECKED='T' where PHONENO={0} and CHECKCODE={1} and ISCHECKED='F'";
                    string sql2 = "update USERS set PASSWORD='{0}' where USERNAME='{1}'";
                    db.Database.ExecuteSqlCommand(string.Format(sql1, PhoneNO, checkCode));
                    db.Database.ExecuteSqlCommand(string.Format(sql2, checkCode, PhoneNO));
                    db.SaveChanges();
                    tran.Complete();
                }

                catch (Exception ex)
                {
                    AddOperationLog.Error("设置验证码验证结束错误", PhoneNO + ex.Message);
                }
            }
        }
        /// <summary>
        /// 设置验证码验证结束
        /// </summary>
        public static void SetCheckFinish(string PhoneNO)
        {
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();
                string sql = "update CHECKCODERECORD set ISCHECKED='T' where PHONENO={0} and ISCHECKED='F'";
                db.Database.ExecuteSqlCommand(string.Format(sql, PhoneNO));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                AddOperationLog.Error("设置验证码验证结束错误", PhoneNO + ex.Message);
            }
        }
    }
}