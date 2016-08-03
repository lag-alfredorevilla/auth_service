using System;
using System.Collections.Generic;
using System.Linq;
using AuthService.Controllers.Models;
using AuthService.Repositories;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {

        public IAuthProviderRepository ProviderRepository { get; set; }

        public IAccessTokenExchangeService TokenExchangeService { get; set; }

        public AuthController (IAuthProviderRepository repo, IAccessTokenExchangeService exchange)
        {
            ProviderRepository = repo;
            TokenExchangeService = exchange;
        }
        
        [HttpGet("providers/")]
        public IEnumerable<AuthProviderConfig> GetProviderList() {
            var providers = ProviderRepository.GetProviders().Select(
                x => new AuthProviderConfig
                {
                    Identifier = x.Identifier,
                    DisplayName = x.DisplayName,
                    GrantType = x.GrantType,
                    Preferred = x.Preferred
                }
            );

            return providers;
        }

        [HttpGet("providers/{id}/request")]
        public AuthRequest GetAuthRequest(string id)
        {
            var provider = ProviderRepository.GetProviders().FirstOrDefault(x => x.Identifier == id);
            if(provider == null)
            {
                throw new Exception("Not found");
            }

            // Assemble the callback URL
            var url = provider.AuthUrl
                .Replace("{CLIENT_ID}", provider.ClientId)
                .Replace("{SCOPE}", provider.Scope)
                .Replace("{CALLBACK_URL}", $"http://www.docugate.ch/auth/providers/{provider.Identifier}/callback");

            return new AuthRequest
            {
                Url = url
            };
        }


        [HttpGet("providers/{id}/callback")]
        public IActionResult ProcessAuthCallback([FromRouteAttribute] string id, [FromQuery] string code, [FromQuery] string error)
        {
            if(error != null)
            {
                return this.Unauthorized();
            }

            try
            {
                var provider = ProviderRepository.GetProvider(id);

                if(provider == null)
                {
                    return NotFound();
                }

                var token = TokenExchangeService.ExchangeForToken(provider, code);

                if(token == null)
                {
                    return this.Unauthorized();
                }

                return Ok(new AuthResponse { Token = token.AccessToken });
            }
            catch (System.Exception)
            {
                return this.Unauthorized();
            }
        }
    }
}