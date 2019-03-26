using LinkMobile.Network.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace LinkMobile.Network
{
    public class RestClient
    {
        private readonly string _contentType;

        public RestClient(string contentType)
        {
            _contentType = contentType;
        }

        public async Task<TResp> GetAsync<TResp>(Uri uri, Dictionary<string, string> header, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                header.ForEach(x => request.Headers.Add(x.Key, x.Value));
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        var content = await StreamToStringAsync(stream);
                        throw new ApiException
                        {
                            StatusCode = (ApiStatus)response.StatusCode,
                            Content = content
                        };
                    }

                    return DeserializeJsonFromStream<TResp>(stream);
                }
            }

        }

        public async Task<TResp> PostAsync<TReq, TResp>(Uri uri, TReq requestObject, Dictionary<string, string> header, CancellationToken cancellationToken) where TResp : BaseResponse
        {
            return await SendDataAsync<TReq, TResp>(uri, requestObject, header, cancellationToken, HttpMethod.Post);
        }

        public async Task<TResp> PutAsync<TReq, TResp>(Uri uri, TReq requestObject,
            Dictionary<string, string> header, CancellationToken cancellationToken) where TResp : BaseResponse
        {
            return await SendDataAsync<TReq, TResp>(uri, requestObject, header, cancellationToken, HttpMethod.Put);
        }

        private async Task<TResp> SendDataAsync<TReq, TResp>(Uri uri, TReq requestObject, Dictionary<string, string> header,
            CancellationToken cancellationToken, HttpMethod httpMethod) where TResp : BaseResponse
        {
            var json = JsonConvert.SerializeObject(requestObject);
            var httpContent = new StringContent(json, Encoding.UTF8, _contentType);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(httpMethod, uri) { Content = httpContent })
            {
                header.ForEach(x => request.Headers.Add(x.Key, x.Value));
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        var content = await StreamToStringAsync(stream);
                        throw new ApiException
                        {
                            StatusCode = (ApiStatus)response.StatusCode,
                            Content = content
                        };
                    }

                    return DeserializeJsonFromStream<TResp>(stream);
                }
            }
        }

        private T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || !stream.CanRead)
            {
                return default(T);
            }

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                return js.Deserialize<T>(jtr);
            }
        }

        private async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                {
                    content = await sr.ReadToEndAsync();
                }
            }

            return content;
        }
    }
}


