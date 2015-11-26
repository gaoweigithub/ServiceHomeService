using FastJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Top.Api.Parser;
using Top.Api.Request;
using Top.Api.Util;

namespace Top.Api
{
    /// <summary>
    /// 基于REST的TOP客户端。
    /// </summary>
    public class DefaultTopClient : ITopClient
    {
        public const string APP_KEY = "app_key";
        public const string FORMAT = "format";
        public const string METHOD = "method";
        public const string TIMESTAMP = "timestamp";
        public const string VERSION = "v";
        public const string SIGN = "sign";
        public const string SIGN_METHOD = "sign_method";
        public const string PARTNER_ID = "partner_id";
        public const string SESSION = "session";
        public const string FORMAT_XML = "xml";
        public const string SIMPLIFY = "simplify";
        public const string TARGET_APP_KEY = "target_app_key";
        public const string SDK_VERSION = "top-sdk-net-20151126";
        public const string SDK_VERSION_CLUSTER = "top-sdk-net-cluster-20151126";

        private string serverUrl;
        private string appKey;
        private string appSecret;
        private string format = FORMAT_XML;

        private WebUtils webUtils;
        internal ITopLogger topLogger;
        private bool disableParser = false; // 禁用响应结果解释
        private bool disableTrace = false; // 禁用日志调试功能
        private bool useSimplifyJson = false; // 是否采用精简化的JSON返回
        private bool useGzipEncoding = true;  // 是否启用响应GZIP压缩
        private IDictionary<string, string> systemParameters; // 设置所有请求共享的系统级参数

        #region DefaultTopClient Constructors

        public DefaultTopClient(string serverUrl, string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverUrl = serverUrl;
            this.webUtils = new WebUtils();
            this.topLogger = new DefaultTopLogger();
        }

        public DefaultTopClient(string serverUrl, string appKey, string appSecret, string format)
            : this(serverUrl, appKey, appSecret)
        {
            this.format = format;
        }

        #endregion


        public void SetTopLogger(ITopLogger topLogger)
        {
            this.topLogger = topLogger;
        }

        public void SetTimeout(int timeout)
        {
            this.webUtils.Timeout = timeout;
        }

        public void SetReadWriteTimeout(int readWriteTimeout)
        {
            this.webUtils.ReadWriteTimeout = readWriteTimeout;
        }

        public void SetDisableParser(bool disableParser)
        {
            this.disableParser = disableParser;
        }

        public void SetDisableTrace(bool disableTrace)
        {
            this.disableTrace = disableTrace;
        }

        public void SetUseSimplifyJson(bool useSimplifyJson)
        {
            this.useSimplifyJson = useSimplifyJson;
        }

        public void SetUseGzipEncoding(bool useGzipEncoding)
        {
            this.useGzipEncoding = useGzipEncoding;
        }

        public void SetIgnoreSSLCheck(bool ignore)
        {
            this.webUtils.IgnoreSSLCheck = ignore;
        }

        public void SetSystemParameters(IDictionary<string, string> systemParameters)
        {
            this.systemParameters = systemParameters;
        }

        #region ITopClient Members

        public T Execute<T>(ITopRequest<T> request) where T : TopResponse
        {
            return Execute<T>(request, null);
        }

        public T Execute<T>(ITopRequest<T> request, string session) where T : TopResponse
        {
            return Execute<T>(request, session, DateTime.Now);
        }

        public T Execute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T : TopResponse
        {
            return DoExecute<T>(request, session, timestamp);
        }

        #endregion

        private T DoExecute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T : TopResponse
        {
            // 提前检查业务参数
            try
            {
                request.Validate();
            }
            catch (TopException e)
            {
                return CreateErrorResponse<T>(e.ErrorCode, e.ErrorMsg);
            }

            // 添加协议级请求参数
            TopDictionary txtParams = new TopDictionary(request.GetParameters());
            txtParams.Add(METHOD, request.GetApiName());
            txtParams.Add(VERSION, "2.0");
            txtParams.Add(SIGN_METHOD, Constants.SIGN_METHOD_MD5);
            txtParams.Add(APP_KEY, appKey);
            txtParams.Add(FORMAT, format);
            txtParams.Add(PARTNER_ID, GetSdkVersion());
            txtParams.Add(TIMESTAMP, timestamp);
            txtParams.Add(TARGET_APP_KEY, request.GetTargetAppKey());
            txtParams.Add(SESSION, session);
            txtParams.AddAll(this.systemParameters);

            if (this.useSimplifyJson)
            {
                txtParams.Add(SIMPLIFY, "true");
            }

            // 添加签名参数
            txtParams.Add(SIGN, TopUtils.SignTopRequest(txtParams, appSecret, true));

            // 添加头部参数
            if (this.useGzipEncoding)
            {
                request.GetHeaderParameters()[Constants.ACCEPT_ENCODING] = Constants.CONTENT_ENCODING_GZIP;
            }

            string realServerUrl = GetServerUrl(this.serverUrl, request.GetApiName(), session);
            string reqUrl = webUtils.BuildGetUrl(realServerUrl, txtParams);
            try
            {
                string body;
                if (request is ITopUploadRequest<T>) // 是否需要上传文件
                {
                    ITopUploadRequest<T> uRequest = (ITopUploadRequest<T>)request;
                    IDictionary<string, FileItem> fileParams = TopUtils.CleanupDictionary(uRequest.GetFileParameters());
                    body = webUtils.DoPost(realServerUrl, txtParams, fileParams, request.GetHeaderParameters());
                }
                else
                {
                    body = webUtils.DoPost(realServerUrl, txtParams, request.GetHeaderParameters());
                }

                // 解释响应结果
                T rsp;
                if (disableParser)
                {
                    rsp = Activator.CreateInstance<T>();
                    rsp.Body = body;
                }
                else
                {
                    if (FORMAT_XML.Equals(format))
                    {
                        ITopParser tp = new TopXmlParser();
                        rsp = tp.Parse<T>(body);
                    }
                    else
                    {
                        ITopParser tp;
                        if (useSimplifyJson)
                        {
                            tp = new TopJsonSimplifyParser();
                        }
                        else
                        {
                            tp = new TopJsonParser();
                        }
                        rsp = tp.Parse<T>(body);
                    }
                }

                // 追踪错误的请求
                if (!disableTrace && rsp.IsError)
                {
                    StringBuilder sb = new StringBuilder(reqUrl).Append(" response error!\r\n").Append(rsp.Body);
                    topLogger.Warn(sb.ToString());
                }
                return rsp;
            }
            catch (Exception e)
            {
                if (!disableTrace)
                {
                    StringBuilder sb = new StringBuilder(reqUrl).Append(" request error!\r\n").Append(e.StackTrace);
                    topLogger.Error(sb.ToString());
                }
                throw e;
            }
        }

        internal virtual string GetServerUrl(string serverUrl, string apiName, string session)
        {
            return serverUrl;
        }

        internal virtual string GetSdkVersion()
        {
            return SDK_VERSION;
        }

        private T CreateErrorResponse<T>(string errCode, string errMsg) where T : TopResponse
        {
            T rsp = Activator.CreateInstance<T>();
            rsp.ErrCode = errCode;
            rsp.ErrMsg = errMsg;

            if (FORMAT_XML.Equals(format))
            {
                XmlDocument root = new XmlDocument();
                XmlElement bodyE = root.CreateElement(Constants.ERROR_RESPONSE);
                XmlElement codeE = root.CreateElement(Constants.ERROR_CODE);
                codeE.InnerText = errCode;
                bodyE.AppendChild(codeE);
                XmlElement msgE = root.CreateElement(Constants.ERROR_MSG);
                msgE.InnerText = errMsg;
                bodyE.AppendChild(msgE);
                root.AppendChild(bodyE);
                rsp.Body = root.OuterXml;
            }
            else
            {
                IDictionary<string, object> errObj = new Dictionary<string, object>();
                errObj.Add(Constants.ERROR_CODE, errCode);
                errObj.Add(Constants.ERROR_MSG, errMsg);
                IDictionary<string, object> root = new Dictionary<string, object>();
                root.Add(Constants.ERROR_RESPONSE, errObj);

                string body = JSON.ToJSON(root);
                rsp.Body = body;
            }
            return rsp;
        }
    }
}
