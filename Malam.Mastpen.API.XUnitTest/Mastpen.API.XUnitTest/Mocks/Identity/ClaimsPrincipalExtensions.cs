﻿using System.Security.Claims;
using IdentityModel;
using Malam.Mastpen.Core;

namespace Malam.Mastpen.API.XUnitTest.Mocks.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        public static IUserInfo GetUserInfo(this ClaimsPrincipal user)
        {
            var userInfo = new UserInfo();

            foreach (var claim in user.Claims)
            {
                if (claim.Type == JwtClaimTypes.Email)
                    userInfo.Email = claim.Value;
                else if (claim.Type == JwtClaimTypes.PreferredUserName)
                    userInfo.UserName = claim.Value;
                else if (claim.Type == JwtClaimTypes.Role)
                    userInfo.Role = claim.Value;
                else if (claim.Type == JwtClaimTypes.GivenName)
                    userInfo.GivenName = claim.Value;
                else if (claim.Type == JwtClaimTypes.MiddleName)
                    userInfo.MiddleName = claim.Value;
                else if (claim.Type == JwtClaimTypes.FamilyName)
                    userInfo.FamilyName = claim.Value;
            }

            return userInfo;
        }
    }
}
