using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// TopTestDelayResponse.
    /// </summary>
    public class TopTestDelayResponse : TopResponse
    {
        /// <summary>
        /// ok
        /// </summary>
        [XmlElement("result")]
        public string Result { get; set; }

    }
}
