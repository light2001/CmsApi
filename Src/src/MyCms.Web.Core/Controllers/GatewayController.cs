using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using XFramework.Configuration.Client;
using XFramework.Core.Abstractions.Client;
using XFramework.Core.Abstractions.Client.Serializer;
using XFramework.Core.Abstractions.Error;
using XFramework.Soa.Abstractions.Error;

namespace MyCms.Controllers
{

    /// <summary>
    /// 用于序列化和反序列化对象
    /// </summary>
    public class JsonSerializer : IServiceSerializer
    {
        public T Deserialize<T>(byte[] response)
        {
            return JsonConvert.DeserializeObject<T>(UTF8Encoding.UTF8.GetString(response));
        }

        public byte[] Serialize<T>(T request)
        {
            return UTF8Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request, Formatting.None));
        }
    }
    public class GatewayController: MyCmsControllerBase
    {
        /// <summary>
        ///  默认使用Json序列化器
        /// </summary>
        private readonly IServiceSerializer serializer = new JsonSerializer();

        private readonly IServiceClient serviceClient = new HttpServiceClient();

        private const int DEFAULT_TIMEOUT_IN_MS = 35000;

        public GatewayController()
        {

        }
        /// <summary>
        /// Soa自动寻址
        /// </summary>
        /// <typeparam name="Req"></typeparam>
        /// <typeparam name="Resp"></typeparam>
        /// <param name="serviceId"></param>
        /// <param name="operationName"></param>
        /// <param name="timeoutInMs"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Resp> Call<Req, Resp>(string serviceId, string operationName, Req request, int timeoutInMs = DEFAULT_TIMEOUT_IN_MS)
        {
            var provider = DynamicConfigFactory.GetInstance($"soa.{serviceId}.properties");
            var address = provider.GetStringProperty("ServerList");

            if (string.IsNullOrEmpty(address))
            {
                throw new SoaClientException(ErrorCode.AppResourceUnavilable, "There's no effective instances available");
            }

            var addressList = address.Split(';');

            // TODO, 这里需要优化为Soa负载均衡策略
            var val = (new Random()).Next(addressList.Length - 1);

            return await Call<Req, Resp>(addressList[val], serviceId, operationName, request, timeoutInMs);
        }

        public async Task<Resp> Call<Req, Resp>
            (string uri, string serviceId, string operationName, Req request, int timeoutInMs = DEFAULT_TIMEOUT_IN_MS)
        {
            if (string.IsNullOrEmpty(uri) || string.IsNullOrEmpty(serviceId) || string.IsNullOrEmpty(operationName))
            {
                throw new XFramework.Core.Abstractions.Error.FrameworkException(ErrorCode.InvalidUri,
                    $"Invalid arguments, url : {uri}, service Id : {serviceId}, " +
                    $"operation Name : {operationName}, any of these parameters is empty");
            }

            var address = BuildAddress(uri, serviceId, operationName);

            return await serviceClient.Call<Req, Resp>(address, request, timeoutInMs, serializer);
        }

        /// <summary>
        /// 组装Http地址
        /// Http地址格式: http://ipaddress/api/[serviceId]/[operationName]
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="serviceId"></param>
        /// <param name="operationName"></param>
        /// <returns></returns>
        private string BuildAddress(string uri, string serviceId, string operationName)
        {
            var sb = new StringBuilder(uri.Length + serviceId.Length + operationName.Length + 10);

            uri = uri.Trim();
            if (!uri.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                sb.Append("http://");
            }

            sb.Append(uri);

            if (!uri.EndsWith("/"))
            {
                sb.Append("/");
            }

            sb.Append($"api/{serviceId}/{operationName}");

            return sb.ToString();
        }

        [HttpPost()]
        [HttpGet()]
        [AllowAnonymous]
        [Route("api/gateway/{*catchall}")]
        public async Task<object> Transfer(string value)
        {
            using (var sr = new StreamReader(HttpContext.Request.Body))
            {
                var path = HttpContext.Request.Path.ToString().Split("/");
                if (path.Length < 3)
                {
                    throw new SoaClientException(ErrorCode.InvalidRequestUrl, "请求地址无效");
                }

                var val = sr.ReadToEnd();
                var request = JsonConvert.DeserializeObject(val);
                return await Call<object, object>(path[path.Length - 2], path[path.Length - 1], request);
            }
        }
    }
}
