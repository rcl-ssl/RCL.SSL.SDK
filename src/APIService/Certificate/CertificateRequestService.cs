#nullable disable

using Microsoft.Extensions.Options;

namespace RCL.SSL.SDK
{
    internal class CertificateRequestService : ApiRequestBase, ICertificateRequestService
    {
        private readonly IAuthTokenService _authTokenService;

        public CertificateRequestService(IAuthTokenService authTokenService,
            IOptions<RCLSDKOptions> options)
            : base(options)
        {
            _authTokenService = authTokenService;
        }

        public async Task GetTestAsync()
        {
            try
            {
                ResourceRequest resourceRequest = await GetResourceRequestAsync();

                string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate/test";

                await TestAsync<ResourceRequest>(uri, resourceRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task<Certificate> GetCertificateAsync(Certificate certificate)
        {
            try
            {
                CertificateRequest certificateRequest = await GetCertificateRequestAsync(certificate);

                string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate";

                Certificate _certificate = await PostAsync<CertificateRequest, Certificate>(uri, certificateRequest);

                return _certificate;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task<Certificate> GetCertificateCoreAsync(string certificateName)
        {
            try
            {
                string uri = $"v1/subscription/{_options.Value.SubscriptionId}/core/certificate/{certificateName}";

                Certificate _certificate = await GetAsync<Certificate>(uri);

                return _certificate;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task<Order> GetCertificateOrderCoreAsync(string certificateName)
        {
            try
            {
                string uri = $"v1/subscription/{_options.Value.SubscriptionId}/core/certificate/{certificateName}/order";

                Order _order = await GetAsync<Order>(uri);

                return _order;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task<Certificate> GetCertificateFinalizeOrderCoreAsync(string certificateName, string orderuri)
        {
            try
            {
                string uri = $"v1/subscription/{_options.Value.SubscriptionId}/core/certificate/{certificateName}/finalize?orderuri={orderuri}";

                Certificate _certificate = await GetAsync<Certificate>(uri);

                return _certificate;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task<List<Certificate>> GetCertificatesToRenewAsync()
        {
            try
            {
                ResourceRequest resourceRequest = await GetResourceRequestAsync();

                string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate/renew/getlist";

                List<Certificate> certificates = await PostAsync<ResourceRequest, List<Certificate>>(uri, resourceRequest);

                return certificates;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        public async Task RenewCertificateAsync(Certificate certificate)
        {
            try
            {
                CertificateRequest certificateRequest = await GetCertificateRequestAsync(certificate);

                string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate/renew";

                await PostAsync<CertificateRequest>(uri, certificateRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        private async Task<string> GetAccessToken(string resource)
        {
            try
            {
                AuthToken authToken = await _authTokenService.GetAuthTokenAsync(resource);
                return authToken.access_token;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : {ex.Message}");
            }
        }

        private async Task<ResourceRequest> GetResourceRequestAsync()
        {
            try
            {
                string accessToken = await GetAccessToken(Constants.AzureResourceManagerResource);
                string accessTokenKeyVault = await GetAccessToken(Constants.AzureKeyVaultResource);

                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(accessTokenKeyVault))
                {
                    throw new Exception($"ERROR from {this.GetType().Name} : Cannot get an access token");
                }

                ResourceRequest resourceRequest = new ResourceRequest
                {
                    accessToken = accessToken,
                    accessTokenKeyVault = accessTokenKeyVault,
                };

                return resourceRequest;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : Could not get Resource Request, {ex.Message}");
            }
        }

        private async Task<CertificateRequest> GetCertificateRequestAsync(Certificate certificate)
        {
            try
            {
                ResourceRequest resourceRequest = await GetResourceRequestAsync();

                CertificateRequest certificateRequest = new CertificateRequest
                {
                    accessToken = resourceRequest.accessToken,
                    accessTokenKeyVault = resourceRequest.accessTokenKeyVault,
                    certificate = certificate
                };

                return certificateRequest;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR from {this.GetType().Name} : Could not get Certificate Request, {ex.Message}");
            }
        }
    }
}
