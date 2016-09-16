using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AuthService.Common.Models;
using AuthService.Repositories.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace AuthService.Repositories
{
    public class FileAuthProviderRepository : IAuthProviderRepository
    {
        IEnumerable<AuthProvider> _providers = new List<AuthProvider>();

        public FileAuthProviderRepository(IHostingEnvironment env)
        {
            Console.WriteLine("Reading from: " + Path.GetDirectoryName(env.ContentRootPath));
            _providers = JsonConvert.DeserializeObject<IEnumerable<AuthProvider>>(File.ReadAllText(Path.Combine(env.ContentRootPath, @"Resources\providers.json")));
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
                    TokenUrl = x.TokenUrl,
                    ClientId = x.ClientId,
                    ClientSecret = x.ClientSecret,
                    Scope = x.Scope
                }
            ).ToArray();
        }
        
        public AuthProviderConfig GetProvider(string identifier)
        {
            var candidate = _providers.FirstOrDefault(x => x.Identifier == identifier);

            if(candidate == null)
            {
                return null;
            }

            return new AuthProviderConfig
            {
                Identifier = candidate.Identifier,
                DisplayName = candidate.DisplayName,
                GrantType = candidate.GrantType,
                Preferred = candidate.Preferred,
                AuthUrl = candidate.AuthUrl,
                TokenUrl = candidate.TokenUrl,
                ClientId = candidate.ClientId,
                ClientSecret = candidate.ClientSecret,
                Scope = candidate.Scope
            };
        }
    }
}