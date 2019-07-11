
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Malam.Mastpen.API.Clients.Contracts;

namespace Malam.Mastpen.API.Clients
{
#pragma warning disable CS1591
    public class IdentityServerClient 
    {
        private readonly MastpenIdentityClientSettings Settings;

        public IdentityServerClient(IOptions<MastpenIdentityClientSettings> settings)
        {
            Settings = settings.Value;
        }

        public async Task<TokenResponse> GetTokenAsync()
        {
            using (var client = new HttpClient())
            {
                // todo: Get identity server url from config file

                var disco = await client.GetDiscoveryDocumentAsync(Settings.Url);

                // todo: Get token request from config file

                return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = Settings.ClientId,
                    ClientSecret = Settings.ClientSecret,
                    UserName = Settings.UserName,
                    Password = Settings.Password
                });
            }
        }
    }
#pragma warning restore CS1591
}
