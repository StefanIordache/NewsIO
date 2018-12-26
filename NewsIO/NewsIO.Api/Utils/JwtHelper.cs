using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsIO.Api.Utils
{
    public static class JwtHelper
    {
        public static JwtSecurityToken GetClaimsFromJwt(string bearerToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = bearerToken.Split(' ').Skip(1).FirstOrDefault();

                var claims = handler.ReadJwtToken(token);

                return claims;
            }
            catch
            {
                return null;
            }
        }

        public static string GetUserNameFromClaims(JwtSecurityToken claims)
        {
            try
            {
                return claims.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;
            }
            catch
            {
                return null;
            }
        }

        public static string GetUserIdFromClaims(JwtSecurityToken claims)
        {
            try
            {
                return claims.Claims.Where(c => c.Type == "jti").FirstOrDefault().Value;
            }
            catch
            {
                return null;
            }
        }

        public static string GetUserRoleFromClaims(JwtSecurityToken claims)
        {
            try
            {
                return claims.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;
            }
            catch
            {
                return null;
            }
        }

        public static string GetUserNameFromJwt(string bearerToken)
        {
            try
            {
                JwtSecurityToken jwtSecurityToken = GetClaimsFromJwt(bearerToken);

                return GetUserIdFromClaims(jwtSecurityToken);
            }
            catch
            {
                return null;
            }
        }

        public static string GetUserIdFromJwt(string bearerToken)
        {
            try
            {
                JwtSecurityToken jwtSecurityToken = GetClaimsFromJwt(bearerToken);

                return GetUserNameFromClaims(jwtSecurityToken);
            }
            catch
            {
                return null;
            }
        }

        public static string GetUserRoleFromJwt(string bearerToken)
        {
            try
            {
                JwtSecurityToken jwtSecurityToken = GetClaimsFromJwt(bearerToken);

                return GetUserRoleFromClaims(jwtSecurityToken);
            }
            catch
            {
                return null;
            }
        }

        public static bool CheckIfUserIsMember(string bearerToken)
        {
            try
            {
                if (GetUserRoleFromJwt(bearerToken).Equals("Member"))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckIfUserIsModerator(string bearerToken)
        {
            try
            {
                if (GetUserRoleFromJwt(bearerToken).Equals("Moderator"))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckIfUserIsAdministrator(string bearerToken)
        {
            try
            {
                if (GetUserRoleFromJwt(bearerToken).Equals("Administrator"))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
