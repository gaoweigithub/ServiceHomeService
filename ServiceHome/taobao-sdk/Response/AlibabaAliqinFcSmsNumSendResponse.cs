using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// AlibabaAliqinFcSmsNumSendResponse.
    /// </summary>
    public class AlibabaAliqinFcSmsNumSendResponse : TopResponse
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        [XmlElement("result")]
        public Top.Api.Domain.BizResult Result { get; set; }

    }
}
