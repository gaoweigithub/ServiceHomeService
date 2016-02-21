using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
namespace ServiceHome
{
    public class ServiceHomeHost : AppSelfHostBase
    {
        public ServiceHomeHost() : base("HomeService Services", typeof(BaseService<Model.Common.RequestBase, Model.Common.ResponseBase>).Assembly) { }
        public override void Configure(Funq.Container container)
        {
            //register any dependencies your services use, e.g:
            //container.Register<ICacheClient>(new MemoryCacheClient());
            //支持cors
            SetConfig(new HostConfig
            {
                GlobalResponseHeaders =
                {
                    { "Access-Control-Allow-Origin", "*" },
                    { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                    { "Access-Control-Allow-Headers", "Content-Type" },
                },
            });
        }

    }
}