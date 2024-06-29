using AuthenticationManager.Models;
using AuthenticationWebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationWebApi.Services
{
    public interface IAuthService
    {
        public Task<AccessTokenData> LoginUserAsync(string email, string password, int refreshTokeExpiryInDays);
        public Task<AccessTokenData> RefreshToken(AccessTokenData accessTokenData);
        public Task<IdentityResult> RegisterUserAsync(User user, string password);
        public Task<List<IdentityError>> UpdateUser(UserDataUpdate updateModel);
    }
}