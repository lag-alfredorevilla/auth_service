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

        public bool Preferred { get; set; }

    }
}