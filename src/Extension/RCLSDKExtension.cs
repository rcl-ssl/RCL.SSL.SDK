using Microsoft.Extensions.DependencyInjection;

namespace RCL.SSL.SDK
{
    public static class RCLSDKExtension
    {
        public static IServiceCollection AddRCLSDKService(this IServiceCollection services, Action<RCLSDKOptions> setupAction)
        {
            services.AddTransient<IAuthTokenService, AuthTokenService>();
            services.AddTransient<ICertificateRequestService, CertificateRequestService>();
            services.Configure<RCLSDKOptions>(setupAction);

            return services;
        }
    }
}
