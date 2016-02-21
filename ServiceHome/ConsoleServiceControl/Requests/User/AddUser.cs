using ServiceStack;
using ServiceHome.ServiceHomeDB;
using ServiceHome.Model.Common;
namespace ServiceHome
{
    [Route("/AddUser", "POST")]
    [Route("/AddUser/{requestHeader}/{User}","POST")]
    public class AddUserRequest : RequestBase
    {
        public USERS User { get; set; }
    }
}