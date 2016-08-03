using System;
using System.Collections.Generic;
using System.Linq;
using AuthService.Common.Models;
using AuthService.Controllers;
using AuthService.Repositories;
using AuthService.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AuthService.Hosting
{
    public class AuthControllerTests
    {
        [Fact]
        public void TestGetProviders()
        {
            // Arrange
            var fakeProvider = new FakeProvider
            {
                Providers = new List<AuthProviderConfig>(){
                    new AuthProviderConfig
                    {
                        Identifier = "Test",
                        GrantType = Grants.Implicit
                    }
                }
            };
            var subject = new AuthController(fakeProvider, new FakeIAccessTokenExchangeService());
            // Act 

            var results = subject.GetProviderList();

            // Assert
            results.Should().NotBeNull();
            results.Should().Contain(x => x.Identifier == "Test" && x.GrantType == Grants.Implicit);
        }

        
        [Fact]
        public void TestGetAuthenticationRequest()
        {
            // Arrange
            var testid = "testid";
            var scope = "read";
            var providerId = "digitalocean";
            var fakeProviderConfig = new AuthProviderConfig
                    {
                        Identifier = providerId,
                        AuthUrl = "https://cloud.digitalocean.com/v1/oauth/authorize?response_type=code&client_id={CLIENT_ID}&redirect_uri={CALLBACK_URL}&scope={SCOPE}",
                        GrantType = Grants.AuthorizationCode,
                        ClientId = testid,
                        Scope = scope
                    };
            var fakeTokenResponse = new TokenResponse(){AccessToken = "TestToken"};
            
            var fakeProvider = new FakeProvider
            {
                Providers = new List<AuthProviderConfig>()
                {
                    fakeProviderConfig
                }
            };

            var fakeExchange = new FakeIAccessTokenExchangeService() { Response = fakeTokenResponse };
            var subject = new AuthController(fakeProvider, fakeExchange);
            
            // Act
            var result = subject.ProcessAuthCallback(providerId, "TestCode", null);

            // Assert
            result.Should().BeAssignableTo<OkObjectResult>();
            (result as OkObjectResult).Value.As<Controllers.Models.AuthResponse>().Token.Should().Be("TestToken");
        }


        internal class FakeProvider : IAuthProviderRepository
        {
            public IEnumerable<AuthProviderConfig> Providers { get; set; }

            public AuthProviderConfig GetProvider(string identifier)
            {
                return Providers.FirstOrDefault(x => x.Identifier == identifier);
            }

            public IEnumerable<AuthProviderConfig> GetProviders()
            {
                return Providers;
            }
        }


        internal class FakeIAccessTokenExchangeService : IAccessTokenExchangeService
        {
            public TokenResponse Response { get; set; }

            public TokenResponse ExchangeForToken(AuthProviderConfig config, string auth)
            {
                return Response;
            }
        }
    }
}