﻿#nullable disable

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
            string accessToken = await GetAccessToken(Constants.AzureResourceManagerResource);

            ResourceRequest resourceRequest = new ResourceRequest
            {
                accessToken = accessToken
            };

            string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate/test";

            await TestAsync<ResourceRequest>(uri, resourceRequest);

        }

        public async Task<Certificate> GetCertificateAsync(Certificate certificate)
        {
            string accessToken = await GetAccessToken(Constants.AzureResourceManagerResource);
            string accessTokenKeyVault = await GetAccessToken(Constants.AzureKeyVaultResource);

            CertificateRequest certificateRequest = new CertificateRequest
            {
                accessToken = accessToken,
                accessTokenKeyVault = accessTokenKeyVault,
                certificate = certificate
            };

            string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate";

            Certificate _certificate = await PostAsync<CertificateRequest, Certificate>(uri, certificateRequest);

            return _certificate;
        }

        public async Task<List<Certificate>> GetCertificatesToRenewAsync()
        {
            string accessToken = await GetAccessToken(Constants.AzureResourceManagerResource);
            string accessTokenKeyVault = await GetAccessToken(Constants.AzureKeyVaultResource);

            ResourceRequest resourceRequest = new ResourceRequest
            {
                accessToken = accessToken,
                accessTokenKeyVault = accessTokenKeyVault,
            };

            string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate/renew/getlist";

            List<Certificate> certificates = await PostAsync<ResourceRequest,List<Certificate>>(uri,resourceRequest);

            return certificates;
        }

        public async Task RenewCertificateAsync(Certificate certificate)
        {
            string accessToken = await GetAccessToken(Constants.AzureResourceManagerResource);
            string accessTokenKeyVault = await GetAccessToken(Constants.AzureKeyVaultResource);

            CertificateRequest certificateRequest = new CertificateRequest
            {
                accessToken = accessToken,
                accessTokenKeyVault = accessTokenKeyVault,
                certificate = certificate
            };

            string uri = $"v1/subscription/{_options.Value.SubscriptionId}/public/certificate/renew";

            await PostAsync<CertificateRequest>(uri, certificateRequest);

        }

        private async Task<string> GetAccessToken(string resource)
        {
            AuthToken authToken = await _authTokenService.GetAuthTokenAsync(resource);
            return authToken.access_token;
        }
    }
}
