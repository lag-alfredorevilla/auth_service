using AuthService.Common.Models;

namespace AuthService.Services
{
    public class HttpAccessTokenExchangeService : IAccessTokenExchangeService
    {
        public TokenResponse ExchangeForToken(AuthProviderConfig config, string auth)
        {
            // This is a dummy response
            return new TokenResponse { AccessToken = auth };
        }
    }
}