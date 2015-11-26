using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.top.test.delay
    /// </summary>
    public class TopTestDelayRequest : BaseTopRequest<Top.Api.Response.TopTestDelayResponse>
    {
        /// <summary>
        /// 系统自动生成
        /// </summary>
        public Nullable<long> SleepTime { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.top.test.delay";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("sleep_time", this.SleepTime);
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
