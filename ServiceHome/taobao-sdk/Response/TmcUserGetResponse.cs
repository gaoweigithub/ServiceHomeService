using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// TmcUserGetResponse.
    /// </summary>
    public class TmcUserGetResponse : TopResponse
    {
        /// <summary>
        /// 开通的用户数据
        /// </summary>
        [XmlElement("tmc_user")]
        public Top.Api.Domain.TmcUser TmcUser { get; set; }

    }
}
