using AuthService.Common.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthService.Services
{
    /// <summary>
    /// some factories may be handy here!
    /// works with azuread only!!
    /// </summary>
    public class HttpAccessTokenExchangeService : IAccessTokenExchangeService
    {

        public TokenResponse ExchangeForToken(AuthProviderConfig config, string authorization_code)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            var url = config.TokenUrl;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            var dic = new Dictionary<string, string>();
            dic.Add("grant_type", config.GrantType == Grants.AuthorizationCode ? "authorization_code" : "");
            dic.Add("client_id", config.ClientId);
            dic.Add("code", authorization_code);
            dic.Add("redirect_uri", $"http://localhost:5000/auth/providers/{config.Identifier}/callback");
            dic.Add("client_secret", config.ClientSecret);
            request.Content = new FormUrlEncodedContent(dic);

            var content = client.PostAsync(url, request.Content).RunAsyncTask().Content;
            var json = content.ReadAsStringAsync().RunAsyncTask();
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            var tokenResponse = new Common.Models.TokenResponse()
            {
                AccessToken = token.access_token,
                ExpiresIn = token.expires_in,
                RefreshToken = token.refresh_token,
                TokenType = token.token_type,
            };

            return tokenResponse;

        }
    }
}