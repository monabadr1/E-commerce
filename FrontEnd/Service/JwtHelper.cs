using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontEnd.Service
{
    public class JWTHelper
    {
        public static string?GetRole(string token)
        {
            var handler =new JwtSecurityTokenHandler();
            var jwt=handler.ReadJwtToken(token);

            return jwt.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)
                ?.Value;
        }

    }
}
