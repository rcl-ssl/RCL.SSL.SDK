namespace RCL.SSL.SDK
{
    public interface ICertificateRequestService
    {
        Task GetTestAsync();
        Task<Certificate> GetCertificateAsync(Certificate certificate);
        Task<Certificate> GetCertificateCoreAsync(string certificateName);
        Task<Order> GetCertificateOrderCoreAsync(string certificateName);
        Task<Certificate> GetCertificateFinalizeOrderCoreAsync(string certificateName, string orderuri);
        Task<List<Certificate>> GetCertificatesToRenewAsync();
        Task RenewCertificateAsync(Certificate certificate);
    }
}
