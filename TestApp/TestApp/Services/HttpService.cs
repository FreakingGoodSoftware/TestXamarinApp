using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public class CoreException : Exception
    {
        public CoreException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }
        public HttpStatusCode Code { get; set; }
    }
    public class HttpService
    {
        public static HttpClient Client { get; private set; }

        public HttpService()
        {
            Client = new HttpClient();
        }
        public async Task<R> RequestAsync<P, R>(string endpoint, P payload, HttpMethod method, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return default(R);

            HttpResponseMessage message = null;

            try
            {
                if (method == HttpMethod.Get)
                {
                    message = await Client.GetAsync(endpoint,
                        token);
                }
                string result = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (message.StatusCode != HttpStatusCode.OK && message.StatusCode != HttpStatusCode.Created)
                {
                    throw new CoreException(message.StatusCode, result);
                }
                return await DeserializeAsync<R>(result);
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception e) when (e is WebException)
            {
                throw new CoreException(HttpStatusCode.BadRequest, "There was a problem completing the request, please try again.");
            }
            return default(R);

        }

        async Task<R> DeserializeAsync<R>(string json)
        {
            return await
                Task.Run(
                    () => JsonConvert.DeserializeObject<R>(json))
                    .ConfigureAwait(false);
        }

        async Task<string> SerializeAsync<T>(T payload)
        {
            return await
                Task.Run(
                    () => JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }))
                    .ConfigureAwait(false);
        }
    }
}
