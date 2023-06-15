#nullable disable

using Newtonsoft.Json;

namespace RCL.SSL.SDK
{
    internal class ResourceRequest
    {
        public string accessToken { get; set; }
        public string accessTokenKeyVault { get; set; }

        [JsonConstructor]
        public ResourceRequest()
        {
        }
    }
}
