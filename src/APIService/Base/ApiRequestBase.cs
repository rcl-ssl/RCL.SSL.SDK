# nullable disable

using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace RCL.SSL.SDK
{
    internal abstract class ApiRequestBase
    {
        protected readonly IOptions<RCLSDKOptions> _options;
        private static readonly HttpClient _client;

        static ApiRequestBase()
        {
            _client = new HttpClient();
        }

        public ApiRequestBase(IOptions<RCLSDKOptions> options)
        {
            _options = options;
        }

        public async Task PostAsync<T>(string uri, T payload)
            where T : class
        {
            try
            {
                SetClientHeaders();
                var response = await _client.PostAsync($"{_options.Value.ApiBaseUrl}/{uri}",
                     new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    throw new Exception($"ERROR from {this.GetType().Name} : {response.StatusCode} : {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task<TResult> PostAsync<T, TResult>(string uri, T payload)
            where TResult : new()
            where T : class
        {
            try
            {
                SetClientHeaders();
                var response = await _client.PostAsync($"{_options.Value.ApiBaseUrl}/{uri}",
                     new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TResult obj = JsonSerializer.Deserialize<TResult>(content);
                    return obj;
                }
                else
                {
                    throw new Exception($"ERROR from {this.GetType().Name} : {response.StatusCode} : {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task TestAsync<T>(string uri, T payload)
        where T : class
        {
            try
            {
                SetClientHeaders();
                var response = await _client.PostAsync($"{_options.Value.ApiBaseUrl}/{uri}",
                     new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new Exception($"ERROR from {this.GetType().Name} : The API Endpoint is Not Found. Please check the API Endpoint configuration.");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new Exception($"ERROR from {this.GetType().Name} : The request is Unauthorized. Please check the configuration for the auth credentials. Also, check the configuration for the SubscriptionId.");
                    }
                    else
                    {
                        throw new Exception($"ERROR from {this.GetType().Name} : {response.StatusCode} : {content}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ResolveContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            else
            {
                if (content.ToLower().Contains("!doctype html"))
                {
                    return string.Empty;
                }
                else
                {
                    return content;
                }
            }
        }

        private void SetClientHeaders()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("RCL-Source", _options.Value.SourceApplication);
        }
    }
}
