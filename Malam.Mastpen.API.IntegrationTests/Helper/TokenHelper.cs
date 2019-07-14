using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Malam.Mastpen.API.IntegrationTests.Mock;

namespace Malam.Mastpen.API.IntegrationTests.Helper
{
    public static class TokenHelper
    {
        public static async Task<TokenResponse> GetMastpenCustomerTokenAsync(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                var settings = ClientSettingsMocker.GetMastpenIdentityClientSettings(userName, password);

                var disco = await client.GetDiscoveryDocumentAsync(settings.Url);

                return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = settings.ClientId,
                    
                   // ClientSecret = settings.ClientSecret,
                    UserName = settings.UserName,
                    Password = settings.Password
                });
            }
        }

        public static async Task<TokenResponse> GetMastpenTokenForMastpenOperatorAsync()
            => await GetMastpenCustomerTokenAsync("Mastpenoperator1@Mastpen.com", "password1");

        public static async Task<TokenResponse> GetMastpenTokenForMastpenManagerAsync()
            => await GetMastpenCustomerTokenAsync("Mastpenmanager1@Mastpen.com", "password1");

        public static async Task<TokenResponse> GetMastpenTokenForWolverineAsync()
            => await GetMastpenCustomerTokenAsync("alice", "alice");//"jameslogan@walla.com", "wolverine");
    }
}