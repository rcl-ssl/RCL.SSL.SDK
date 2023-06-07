namespace RCL.SSL.SDK
{
    public interface ICertificateRequestService
    {
        Task GetTestAsync();
        Task<Certificate> GetCertificateAsync(Certificate certificate);
        Task<List<Certificate>> GetCertificatesToRenewAsync();
        Task RenewCertificateAsync(Certificate certificate);
    }
}
