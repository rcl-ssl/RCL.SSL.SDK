#nullable disable

namespace RCL.SSL.SDK.Test
{
    [TestClass]
    public class AuthTokenTest
    {
        private readonly IAuthTokenService _authTokenService;

        public AuthTokenTest()
        {
            _authTokenService = (IAuthTokenService)DependencyResolver
                 .ServiceProvider().GetService(typeof(IAuthTokenService));
        }

        [TestMethod]
        public async Task GetAzureResourceManagerTokenTest()
        {
            try
            {
                AuthToken authToken = await _authTokenService.GetAuthTokenAsync(Constants.AzureResourceManagerResource);
                Assert.AreNotEqual(string.Empty, authToken?.access_token ?? string.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetAzureKeyVaultTokenTest()
        {
            try
            {
                AuthToken authToken = await _authTokenService.GetAuthTokenAsync(Constants.AzureKeyVaultResource);
                Assert.AreNotEqual(string.Empty, authToken?.access_token ?? string.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
