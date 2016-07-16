﻿using System;
using System.Collections.Generic;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP请求接口。asd
    /// </summary>
    public interface ITopRequest<T> where T : TopResponse
    {
        /// <summary>
        /// 获取TOP的API名称。
        /// </summary>
        string GetApiName();

        /// <summary>
        /// 获取被调用的目标AppKey
        /// </summary>
        string GetTargetAppKey();

        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。
        /// </summary>
        IDictionary<string, string> GetParameters();

        /// <summary>
        /// 获取自定义HTTP请求头参数。
        /// </summary>
        IDictionary<string, string> GetHeaderParameters();

        /// <summary>
        /// 客户端参数检查，减少服务端无效调用。
        /// </summary>
        void Validate();
    }
}
