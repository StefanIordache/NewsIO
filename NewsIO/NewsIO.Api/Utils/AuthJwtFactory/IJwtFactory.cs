using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsIO.Api.Utils.AuthJwtFactory
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string userId, string role);
    }
}
