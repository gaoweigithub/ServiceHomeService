using System;
using System.Collections.Generic;
using System.Text;
using Top.Api.Request;

namespace Top.Api
{
    /// <summary>
    /// 基础TOP请求类，存放一些通用的请求参数。
    /// </summary>
    public abstract class BaseTopRequest<T> : ITopRequest<T> where T : TopResponse
    {
        /// <summary>
        /// HTTP请求头参数
        /// </summary>
        public TopDictionary HeaderParams { get; set; }
        /// <summary>
        /// 自定义表单参数
        /// </summary>
        public TopDictionary OtherParams { get; set; }
        /// <summary>
        /// 请求目标AppKey
        /// </summary>
        public string TargetAppKey { get; set; }
        /// <summary>
        /// 指定哪个入参是混淆参数
        /// </summary>
        public string TopMixParams { get; set; }

        public void AddOtherParameter(string key, string value)
        {
            if (this.OtherParams == null)
            {
                this.OtherParams = new TopDictionary();
            }
            this.OtherParams.Add(key, value);
        }

        public void AddHeaderParameter(string key, string value)
        {
            GetHeaderParameters().Add(key, value);
        }

        public IDictionary<string, string> GetHeaderParameters()
        {
            if (this.HeaderParams == null)
            {
                this.HeaderParams = new TopDictionary();
            }
            return this.HeaderParams;
        }

        public string GetTargetAppKey()
        {
            return this.TargetAppKey;
        }

        public abstract string GetApiName();

        public abstract void Validate();

        public abstract IDictionary<string, string> GetParameters();
    }
}
