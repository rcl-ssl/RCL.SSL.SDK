namespace RCL.SSL.SDK
{
    public interface IAuthTokenService
    {
        Task<AuthToken> GetAuthTokenAsync(string resource);
        Task<string> GetAuthTokenRawResponseAsync(string resource);
    }
}
