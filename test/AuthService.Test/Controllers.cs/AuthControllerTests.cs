using System;
using System.Collections.Generic;
using AuthService.Common.Models;
using AuthService.Controllers;
using AuthService.Repositories;
using FluentAssertions;
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
            var subject = new AuthController(fakeProvider);
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

            var fakeProvider = new FakeProvider
            {
                Providers = new List<AuthProviderConfig>(){
                    new AuthProviderConfig
                    {
                        Identifier = providerId,
                        AuthUrl = "https://cloud.digitalocean.com/v1/oauth/authorize?response_type=code&client_id={CLIENT_ID}&redirect_uri={CALLBACK_URL}&scope={SCOPE}",
                        GrantType = Grants.AuthorizationCode,
                        ClientId = testid,
                        Scope = scope
                    }
                }
            };
            var subject = new AuthController(fakeProvider);
            // Act 

            var result = subject.GetAuthRequest("digitalocean");

            // Assert
            result.Should().NotBeNull();
            result.Url.Should().Be($"https://cloud.digitalocean.com/v1/oauth/authorize?response_type=code&client_id={testid}&redirect_uri=https://localhost:8080/auth/{providerId}/callback&scope={scope}");
        }


        internal class FakeProvider : IAuthProviderRepository
        {
            public IEnumerable<AuthProviderConfig> Providers { get; set; }

            public IEnumerable<AuthProviderConfig> GetProviders()
            {
                return Providers;
            }
        }
    }
}