using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// AlibabaAliqinFcVoiceNumSinglecallResponse.
    /// </summary>
    public class AlibabaAliqinFcVoiceNumSinglecallResponse : TopResponse
    {
        /// <summary>
        /// 接口返回参数
        /// </summary>
        [XmlElement("result")]
        public Top.Api.Domain.BizResult Result { get; set; }

    }
}
