using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
namespace ServiceHome
{
    public class ServiceHomeHost : AppHostBase
    {
        public ServiceHomeHost() : base("HomeService Services", typeof(BaseService<Model.Common.RequestBase, Model.Common.ResponseBase>).Assembly) { }
        public override void Configure(Funq.Container container)
        {
            //register any dependencies your services use, e.g:
            //container.Register<ICacheClient>(new MemoryCacheClient());
        }
    }
}