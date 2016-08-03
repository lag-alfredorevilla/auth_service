using AuthService.Common.Models;

namespace AuthService.Services
{
    public interface IAccessTokenExchangeService
    {
        TokenResponse ExchangeForToken(AuthProviderConfig config, string auth);
    }
}