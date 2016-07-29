using System.Collections.Generic;

namespace AuthService.Common.Models
{
    public class AuthProviderConfig
    {
        public string Identifier { get; set; }

        public Dictionary<string, string> DisplayName { get; set; }

        public Grants GrantType { get; set; }

        public string AuthUrl { get; set; }

        public bool Preferred { get; set; }
    }
}