using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RCL.SSL.SDK.Test
{
    public static class DependencyResolver
    {
        public static ServiceProvider ServiceProvider()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddUserSecrets<TestProject>();
            IConfiguration configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services.Configure<RCLSDKOptions>(configuration.GetSection(RCLSDKOptions.RCLSDK));
            services.AddRCLSDKService();

            return services.BuildServiceProvider();
        }
    }

    public class TestProject
    {
    }
}
