using System.Collections.Generic;

namespace AuthService.Repositories
{
    public interface IAuthProviderRepository
    {
        IEnumerable<Common.Models.AuthProviderConfig> GetProviders();
    }
}