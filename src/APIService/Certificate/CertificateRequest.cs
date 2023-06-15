#nullable disable

using Newtonsoft.Json;

namespace RCL.SSL.SDK
{
    internal class CertificateRequest : ResourceRequest
    {
        public Certificate certificate { get; set; }

        [JsonConstructor]
        public CertificateRequest()
        {
        }
    }
}
