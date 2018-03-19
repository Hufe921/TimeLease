using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rita.Utilities.Helper
{
    public class HttpResponceHelper
    {
        public static HttpResponseMessage NotExsit(HttpResponseMessage message, string txt = "")
        {
            txt = string.IsNullOrEmpty(txt) ? "该数据不存在" : $"{txt}不存在";
            return Defined(message, txt);
        }
        public static HttpResponseMessage UpdateError(HttpResponseMessage message)
        {
            return Defined(message, "更新数据失败");
        }

        public static HttpResponseMessage AddError(HttpResponseMessage message)
        {
            return Defined(message, "新增数据失败");
        }
        public static HttpResponseMessage DeleteError(HttpResponseMessage message)
        {
            return Defined(message, "删除信息失败");
        }
        public static HttpResponseMessage GetError(HttpResponseMessage message)
        {
            return Defined(message, "获取信息失败");
        }

        public static HttpResponseMessage Defined(HttpResponseMessage message, string txt)
        {
            message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            HybridDictionary reason = new HybridDictionary();
            reason.Add("Reason", txt);
            message.Content = new StringContent(JsonConvert.SerializeObject(reason));
            return message;
        }
        /// <summary>
        /// 文件下载或者传输的响应消息构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static HttpResponseMessage FileResponse(string name, string path)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fileStream = File.OpenRead(path);
            httpResponseMessage.Content = new StreamContent(fileStream);
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = name
            };
            return httpResponseMessage;
        }
    }
}
