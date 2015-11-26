using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;

namespace Top.Api.Util
{
    /// <summary>
    /// 网络工具类。
    /// </summary>
    public sealed class WebUtils
    {
        private int _timeout = 20000;
        private int _readWriteTimeout = 60000;
        private bool _ignoreSSLCheck = true;

        /// <summary>
        /// 等待请求开始返回的超时时间
        /// </summary>
        public int Timeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }

        /// <summary>
        /// 等待读取数据完成的超时时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return this._readWriteTimeout; }
            set { this._readWriteTimeout = value; }
        }

        /// <summary>
        /// 是否忽略SSL检查
        /// </summary>
        public bool IgnoreSSLCheck
        {
            get { return this._ignoreSSLCheck; }
            set { this._ignoreSSLCheck = value; }
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> textParams)
        {
            return DoPost(url, textParams, null);
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, string> headerParams)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(textParams));
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(string url, IDictionary<string, string> textParams)
        {
            return DoGet(url, textParams, null);
        }

        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(string url, IDictionary<string, string> textParams, IDictionary<string, string> headerParams)
        {
            if (textParams != null && textParams.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(textParams);
                }
                else
                {
                    url = url + "?" + BuildQuery(textParams);
                }
            }

            HttpWebRequest req = GetWebRequest(url, "GET", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        /// 执行带文件上传的HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="fileParams">请求文件参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, FileItem> fileParams, IDictionary<string, string> headerParams)
        {
            // 如果没有文件参数，则走普通POST请求
            if (fileParams == null || fileParams.Count == 0)
            {
                return DoPost(url, textParams, headerParams);
            }

            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线

            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

            System.IO.Stream reqStream = req.GetRequestStream();
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            // 组装文本请求参数
            string textTemplate = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> textEnum = textParams.GetEnumerator();
            while (textEnum.MoveNext())
            {
                string textEntry = string.Format(textTemplate, textEnum.Current.Key, textEnum.Current.Value);
                byte[] itemBytes = Encoding.UTF8.GetBytes(textEntry);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);
            }

            // 组装文件请求参数
            string fileTemplate = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileItem>> fileEnum = fileParams.GetEnumerator();
            while (fileEnum.MoveNext())
            {
                string key = fileEnum.Current.Key;
                FileItem fileItem = fileEnum.Current.Value;
                string fileEntry = string.Format(fileTemplate, key, fileItem.GetFileName(), fileItem.GetMimeType());
                byte[] itemBytes = Encoding.UTF8.GetBytes(fileEntry);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);

                byte[] fileBytes = fileItem.GetContent();
                reqStream.Write(fileBytes, 0, fileBytes.Length);
            }

            reqStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        public HttpWebRequest GetWebRequest(string url, string method, IDictionary<string, string> headerParams)
        {
            HttpWebRequest req = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = CertificateManager.GetCallback(this._ignoreSSLCheck);
                req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(url);
            }

            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (string key in headerParams.Keys)
                {
                    req.Headers.Add(key, headerParams[key]);
                }
            }

            req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            req.KeepAlive = true;
            req.UserAgent = "top-sdk-net";
            req.Accept = "text/xml,text/javascript";
            req.Timeout = this._timeout;
            req.ReadWriteTimeout = this._readWriteTimeout;

            return req;
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                if (Constants.CONTENT_ENCODING_GZIP.Equals(rsp.ContentEncoding, StringComparison.OrdinalIgnoreCase))
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        /// <summary>
        /// 组装GET请求URL。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>带参数的GET请求URL</returns>
        public string BuildGetUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters);
                }
            }
            return url;
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;

            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }

                    postData.Append(name);
                    postData.Append("=");
                    postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    hasParam = true;
                }
            }

            return postData.ToString();
        }
    }
}
