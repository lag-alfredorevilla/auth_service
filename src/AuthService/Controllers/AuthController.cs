using System.Collections.Generic;
using System.Linq;
using AuthService.Controllers.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {

        public IAuthProviderRepository ProviderRepository { get; set; }

        public AuthController (IAuthProviderRepository repo)
        {
            ProviderRepository = repo;
        }
        
        [HttpGet("")]
        public IActionResult GetProviderList() {
            var providers = ProviderRepository.GetProviders().Select(
                x => new AuthProviderConfig
                {
                    Identifier = x.Identifier,
                    DisplayName = x.DisplayName,
                    GrantType = x.GrantType,
                    Preferred = x.Preferred
                }
            );

            return Ok(providers);
        }
    }
}