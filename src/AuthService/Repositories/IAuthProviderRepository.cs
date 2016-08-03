using System.Collections.Generic;
using AuthService.Common.Models;

namespace AuthService.Repositories
{
    public interface IAuthProviderRepository
    {
        IEnumerable<AuthProviderConfig> GetProviders();

        AuthProviderConfig GetProvider(string identifier);
    }
}