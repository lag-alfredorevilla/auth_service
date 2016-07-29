using System.Collections.Generic;
using AuthService.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuthService.Controllers.Models
{
    public class AuthProviderConfig
    {
        public string Identifier { get; set; }

        public Dictionary<string, string> DisplayName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Grants GrantType { get; set; }

        public bool Preferred { get; set; }
    }
}