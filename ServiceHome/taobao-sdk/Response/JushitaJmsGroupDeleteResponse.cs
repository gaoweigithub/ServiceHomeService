using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// JushitaJmsGroupDeleteResponse.
    /// </summary>
    public class JushitaJmsGroupDeleteResponse : TopResponse
    {
        /// <summary>
        /// 操作结果
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

    }
}
