#nullable disable

namespace RCL.SSL.SDK.Test
{
    [TestClass]
    public class CertificateServiceTest
    {
        private readonly ICertificateRequestService _certificateRequestService;

        public CertificateServiceTest()
        {
            _certificateRequestService = (ICertificateRequestService)DependencyResolver
                 .ServiceProvider().GetService(typeof(ICertificateRequestService));
        }

        [TestMethod]
        public async Task CertificateTestTest()
        {
            try
            {
                await _certificateRequestService.GetTestAsync();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task CertificateTest()
        {
            try
            {
                Certificate certificate = new Certificate
                {
                    certificateName = "shopeneur.com"
                };

                Certificate _certificate = await _certificateRequestService.GetCertificateAsync(certificate);
                Assert.AreNotEqual(string.Empty, _certificate?.certificateName ?? string.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task CertificateCoreTest()
        {
            try
            {
                Certificate _certificate = await _certificateRequestService.GetCertificateCoreAsync("shopeneur.com");
                Assert.AreNotEqual(string.Empty, _certificate?.certificateName ?? string.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task CertificateOrderCoreTest()
        {
            try
            {
                Order _order = await _certificateRequestService.GetCertificateOrderCoreAsync("shopeneur.com");
                Assert.AreNotEqual(string.Empty, _order?.status ?? string.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task CertificateFinalizeOrderCoreTest()
        {
            try
            {
                Certificate _certificate = await _certificateRequestService.GetCertificateFinalizeOrderCoreAsync("shopeneur.com", "https://acme-staging-v02.api.letsencrypt.org/acme/order/114111834/10239929564");
                Assert.AreNotEqual(string.Empty, _certificate?.certificateName ?? string.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task CertificateRenewGetListTest()
        {
            try
            {
                List<Certificate> certificates = await _certificateRequestService.GetCertificatesToRenewAsync();
                Assert.AreNotEqual(0, certificates?.Count);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task CertificateRenewTest()
        {
            try
            {
                Certificate certificate = new Certificate
                {
                    certificateName = "store.shopeneur.com"
                };

                await _certificateRequestService.RenewCertificateAsync(certificate);
                Assert.AreEqual(1,1);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
