namespace RCL.SSL.SDK
{
    internal interface IAuthTokenService
    {
        Task<AuthToken> GetAuthTokenAsync(string resource);
    }
}
