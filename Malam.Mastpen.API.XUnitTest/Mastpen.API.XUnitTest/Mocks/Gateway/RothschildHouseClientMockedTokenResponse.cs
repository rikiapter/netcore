
using System.Net;
using IdentityModel.Client;

namespace Malam.Mastpen.API.XUnitTest.Gateway
{
    public class RothschildHouseClientMockedTokenResponse : TokenResponse
    {
        public RothschildHouseClientMockedTokenResponse()
            : base(HttpStatusCode.OK, "mocks", "token")
        {
        }

        public new bool IsError
            => false;
    }
}
