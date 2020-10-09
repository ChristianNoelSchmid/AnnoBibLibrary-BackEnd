using System.Collections.Generic;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.RouteModels;

namespace AnnoBibLibrary.Repos
{
    public interface IUserRepo
    {
        Task<string> CreateUser(NewUser newUser);
        Task<UserInfo> Authenticate(Credentials credentials);
        Task<ApplicationUser> GetById(string id);
    }
}