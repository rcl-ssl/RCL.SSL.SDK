#nullable disable

namespace RCL.SSL.SDK
{
    public class RCLSDKOptions
    {
        public const string RCLSDK = "RCLSDK";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string ApiBaseUrl { get; set; }
        public string SubscriptionId { get; set; }
        public string SourceApplication { get; set; }
    }
}
