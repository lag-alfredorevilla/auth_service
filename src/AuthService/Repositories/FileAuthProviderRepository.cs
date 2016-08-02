using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AuthService.Repositories.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AuthService.Repositories
{
    public class FileAuthProviderRepository : IAuthProviderRepository
    {
        IEnumerable<AuthProvider> _providers = new List<AuthProvider>();

        public FileAuthProviderRepository(IHostingEnvironment env)
        {
            Console.WriteLine("Reading from: " + Path.GetDirectoryName(env.ContentRootPath));
            _providers = JsonConvert.DeserializeObject<IEnumerable<AuthProvider>>(File.ReadAllText(Path.Combine(env.ContentRootPath, "providers.json")));
        }

        public IEnumerable<Common.Models.AuthProviderConfig> GetProviders()
        {
            return _providers.Select(
                x => new Common.Models.AuthProviderConfig()
                {
                    Identifier = x.Identifier,
                    DisplayName = x.DisplayName,
                    GrantType = x.GrantType,
                    Preferred = x.Preferred,
                    AuthUrl = x.AuthUrl,
                    ClientId = x.ClientId,
                    Scope = x.Scope
                }
            ).ToArray();
        }
    }
}