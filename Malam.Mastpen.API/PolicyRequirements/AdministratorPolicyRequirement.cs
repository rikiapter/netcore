using System.Threading.Tasks;
using IdentityModel;
using Malam.Mastpen.API.Security;
using Microsoft.AspNetCore.Authorization;


namespace Malam.Mastpen.API.PolicyRequirements
{
#pragma warning disable CS1591
    public class AdministratorPolicyRequirement : AuthorizationHandler<AdministratorPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministratorPolicyRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == JwtClaimTypes.Role 
            && claim.Value == Roles.Administrator 
            && claim.Value == Roles.Customer))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.FromResult(0);
        }
    }
#pragma warning restore CS1591
}
