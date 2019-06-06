using System.Security.Claims;
using IdentityModel;

namespace Malam.Mastpen.API.XUnitTest.Mocks.Identity
{
    public static class IdentityMocker
    {
        public static ClaimsPrincipal GetCustomerIdentity()
            => new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtClaimTypes.PreferredUserName, "jameslogan"),
                new Claim(JwtClaimTypes.Email, "jameslogan@walla.com"),
                new Claim(JwtClaimTypes.Role, "Customer"),
                new Claim(JwtClaimTypes.GivenName, "James"),
                new Claim(JwtClaimTypes.MiddleName, ""),
                new Claim(JwtClaimTypes.FamilyName, "Logan")
            }));

        public static ClaimsPrincipal GetMastpenOperatorIdentity()
            => new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtClaimTypes.PreferredUserName, "Mastpenoperator1"),
                new Claim(JwtClaimTypes.Email, "Mastpenoperator1@Mastpen.com"),
                new Claim(JwtClaimTypes.Role, "MastpenOperator")
            }));
    }
}
