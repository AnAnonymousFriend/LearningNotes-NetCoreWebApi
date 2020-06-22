using API.Core.Common.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API.Core.Common.Http
{
    /// <summary>
    /// 基于 RestSharp 封装HttpHelper
    /// </summary>
    public static class HttpHelper
    {
        static readonly string fsApi_Address = Appsettings.app(new string[] { "AppSettings", "FSAPI", "Address" });
        static readonly string fsapi_key = Appsettings.app(new string[] { "AppSettings", "FSAPI", "Key" });


        /// <summary>
        /// Get 请求
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="baseUrl">根域名:http://apk.neters.club/</param>
        /// <param name="url">接口:api/xx/yy</param>
        /// <param name="pragm">参数:id=2&name=老张</param>
        /// <returns></returns>
        public static T GetApi<T>(string baseUrl, string url, string pragm = "")
        {
            var client = new RestSharpClient(baseUrl);

            var request = client.Execute(string.IsNullOrEmpty(pragm)
                ? new RestRequest(url, Method.GET)
                : new RestRequest($"{url}?{pragm}", Method.GET));

            if (request.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(request.ErrorMessage, typeof(T));
            }

            dynamic temp = Newtonsoft.Json.JsonConvert.DeserializeObject(request.Content, typeof(T));

            //T result = (T)Convert.ChangeType(request.Content, typeof(T));

            return (T)temp;
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="url">完整的url</param>
        /// <param name="body">post body,可以匿名或者反序列化</param>
        /// <returns></returns>
        [Obsolete]
        public static T PostApi<T>(string url, object body = null)
        {
            var client = new RestClient($"{url}");
            IRestRequest queest = new RestRequest();
            queest.Method = Method.POST;
            queest.AddHeader("Accept", "application/json");
            queest.RequestFormat = DataFormat.Json;
            queest.AddBody(body); // 可以使用 JsonSerializer
            var result = client.Execute(queest);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(result.ErrorMessage, typeof(T));
            }

            dynamic temp = Newtonsoft.Json.JsonConvert.DeserializeObject(result.Content, typeof(T));

            //T result = (T)Convert.ChangeType(request.Content, typeof(T));

            return (T)temp;
        }

        /// <summary>
        /// 请求FS的接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete]
        public static T PostFsApi<T>(FsAction Action, string param)
        {
            long t = GetTimestamp();
            string sign = fsapi_key + t + param;
            string signs = GetMD5(sign);//MD5加密

            var url = fsApi_Address + "modules=ajax&handler=cloudAuth&ajax_request_action=" + Action + "&param=" + param + "&sign=" + signs + "&t=" + t + "&key=" + fsapi_key;
            var client = new RestClient($"{url}");
            IRestRequest queest = new RestRequest
            {
                Method = Method.POST
            };
            queest.AddHeader("Accept", "application/json");
            queest.RequestFormat = DataFormat.Json;
            var result = client.Execute(queest);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(result.ErrorMessage, typeof(T));
            }

            dynamic temp = Newtonsoft.Json.JsonConvert.DeserializeObject(result.Content, typeof(T));
            return (T)temp;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private static long GetTimestamp()
        {
            TimeSpan cha = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long t = (long)cha.TotalMilliseconds;
            return t;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetMD5(string input)
        {
            if (input == null)
            {
                return null;
            }
            MD5 md5Hash = MD5.Create();
            // 将输入字符串转换为字节数组并计算哈希数据 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // 创建一个 Stringbuilder 来收集字节并创建字符串 
            StringBuilder sBuilder = new StringBuilder();
            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // 返回十六进制字符串 
            return sBuilder.ToString();
        }



    }
}
