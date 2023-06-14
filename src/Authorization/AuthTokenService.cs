#nullable disable

using Microsoft.Extensions.Options;
using System.Text.Json;

namespace RCL.SSL.SDK
{
    internal class AuthTokenService : IAuthTokenService
    {
        private readonly IOptions<RCLSDKOptions> _authOptions;
        private static readonly HttpClient _httpClient;

        static AuthTokenService()
        {
            _httpClient = new HttpClient();
        }

        public AuthTokenService(IOptions<RCLSDKOptions> authOptions)
        {
            _authOptions = authOptions;
        }

        public async Task<AuthToken> GetAuthTokenAsync(string resource)
        {
            try
            {
                if(string.IsNullOrEmpty(_authOptions?.Value?.ClientId ?? string.Empty))
                {
                    throw new Exception("Client Id is null or empty, cannot generate access token");
                }
                if (string.IsNullOrEmpty(_authOptions?.Value?.ClientSecret ?? string.Empty))
                {
                    throw new Exception("Client Secret is null or empty, cannot generate access token");
                }
                if (string.IsNullOrEmpty(_authOptions?.Value?.TenantId ?? string.Empty))
                {
                    throw new Exception("Tenant Id is null or empty, cannot generate access token");
                }

                var formcontent = new List<KeyValuePair<string, string>>();
                formcontent.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                formcontent.Add(new KeyValuePair<string, string>("client_id", _authOptions.Value.ClientId));
                formcontent.Add(new KeyValuePair<string, string>("client_secret", _authOptions.Value.ClientSecret));
                formcontent.Add(new KeyValuePair<string, string>("resource", resource));

                string url = $"https://login.microsoftonline.com/{_authOptions.Value.TenantId}/oauth2/token";

                var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(formcontent) };

                var response = await _httpClient.SendAsync(request);

                string jstr = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    AuthToken authToken = JsonSerializer.Deserialize<AuthToken>(jstr);
                    return authToken;
                }
                else
                {
                    throw new Exception($"Could not obtain Access Token. {jstr}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Access Token Error : {ex.Message}");
            }
        }
    }
}
