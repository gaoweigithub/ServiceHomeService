using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.httpdns.get
    /// </summary>
    public class HttpdnsGetRequest : BaseTopRequest<Top.Api.Response.HttpdnsGetResponse>
    {
        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.httpdns.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
