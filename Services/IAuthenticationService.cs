using System.Security.Claims;
using System.Threading.Tasks;
using AnnoBibLibrary.RouteModels;

namespace AnnoBibLibrary.Services
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateLoginAsync(Credentials credentials);

        Task<ClaimsPrincipal> GetPrincipalAsync(string email);
    }
}