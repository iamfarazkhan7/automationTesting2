using Newtonsoft.Json;
using System;
using System.Text;
using DevToolsFetch = OpenQA.Selenium.DevTools.V108.Fetch;
using DevToolsNetwork = OpenQA.Selenium.DevTools.V108.Network;

namespace TG.Test.WebApps.Common.DTO
{
    public class NetworkRequest
    {
        public string RequestId { get; set; }
        public DevToolsNetwork.Request Request { get; set; }
        public DevToolsNetwork.Response Response { get; set; }
        public DevToolsFetch.GetResponseBodyCommandResponse FetchResponseBody { get; set; }
        public DevToolsNetwork.GetResponseBodyCommandResponse NetworkResponseBody { get; set; }

        public NetworkRequest()
        {

        }

        public NetworkRequest(DevToolsNetwork.Request request, DevToolsFetch.GetResponseBodyCommandResponse responseBody)
        {
            Request = request;
            FetchResponseBody = responseBody;
        }

        public NetworkRequest(string requestId, DevToolsNetwork.Request request, DevToolsNetwork.Response response, DevToolsNetwork.GetResponseBodyCommandResponse responseBody)
        {
            RequestId = requestId;
            Request = request;
            Response = response;
            NetworkResponseBody = responseBody;
        }

        public T GetFetchResponseBody<T>()
        {
            return GetResponseBody<T>(FetchResponseBody.Body, FetchResponseBody.Base64Encoded);
        }

        public T GetNetworkResponseBody<T>()
        {
            return GetResponseBody<T>(NetworkResponseBody.Body, NetworkResponseBody.Base64Encoded);
        }

        public string GetRequestBody() => Request?.PostData;

        public string GetFetchResponseBody() => GetFetchResponseBody<string>();

        public string GetNetworkResponseBody() => GetNetworkResponseBody<string>();

        public override string ToString()
        {
            return $"RequestId: {RequestId}, Url: {Request?.Url}";
        }

        private T GetResponseBody<T>(string responseBodyValue, bool base64Encoded)
        {
            var response = responseBodyValue;
            if (base64Encoded)
            {
                var base64EncodedBytes = Convert.FromBase64String(responseBodyValue);
                response = Encoding.ASCII.GetString(base64EncodedBytes);
            }

            if (typeof(T).Equals(typeof(string)))
            {
                return (T)Convert.ChangeType(response, typeof(T));
            }

            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
