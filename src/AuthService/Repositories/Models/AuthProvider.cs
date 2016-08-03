using System.Collections.Generic;
using AuthService.Common.Models;

namespace AuthService.Repositories.Models
{
    public class AuthProvider
    {
        public string Identifier { get; set; }

        public Dictionary<string, string> DisplayName { get; set; }

        public Grants GrantType { get; set; }

        public string AuthUrl { get; set; }

        public string TokenUrl { get; set; }

        public bool Preferred { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Scope { get; set; }

    }
}