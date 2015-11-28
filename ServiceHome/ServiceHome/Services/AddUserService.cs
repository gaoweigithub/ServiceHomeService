using System.Collections.Generic;
using ServiceHome.ServiceHomeDB;
namespace ServiceHome
{
    public class AddUserService : BaseService
    {
        public object Post(AddUserRequest request)
        {
            housekeepingEntities db = new housekeepingEntities();
            USERS uu = db.USERS.Add(request.User);
            int iRow=db.SaveChanges();
            if (iRow != 0)
            {
                return new AddUserResponse()
                {
                    ResponseStatus = new Model.Common.ResponseStatus
                    {
                        isSuccess = true,
                        ErrorList = new List<string> { "添加成功" }
                    },
                };
            }
            return new AddUserResponse()
            {
                ResponseStatus = new Model.Common.ResponseStatus
                {
                    isSuccess = false,
                    ErrorList = new List<string> { "添加失败" }
                },
            };
        }
    }
}