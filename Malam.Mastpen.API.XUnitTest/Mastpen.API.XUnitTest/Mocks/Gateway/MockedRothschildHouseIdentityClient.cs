
using System.Threading.Tasks;
using IdentityModel.Client;
using Malam.Mastpen.API.Clients.Contracts;


namespace Malam.Mastpen.API.XUnitTest.Gateway
{
    public class MockedRothschildHouseIdentityClient : IRothschildHouseIdentityClient
    {
#pragma warning disable CS1998
        public async Task<TokenResponse> GetRothschildHouseTokenAsync()
            => new RothschildHouseClientMockedTokenResponse();
#pragma warning restore CS1998
    }
}
