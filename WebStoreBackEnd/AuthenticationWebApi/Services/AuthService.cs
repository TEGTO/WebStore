using AuthenticationManager.Models;
using AuthenticationManager.Services;
using AuthenticationWebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly JwtHandler jwtHandler;

        public AuthService(UserManager<User> userManager, JwtHandler jwtHandler)
        {
            this.userManager = userManager;
            this.jwtHandler = jwtHandler;
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }
        public async Task<AccessTokenData> LoginUserAsync(string email, string password, int refreshTokeExpiryInDays)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null || !await userManager.CheckPasswordAsync(user, password))
            {
                throw new UnauthorizedAccessException("Invalid Authentication");
            }
            var token = jwtHandler.CreateToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokeExpiryInDays);
            await userManager.UpdateAsync(user);
            return token;
        }
        public async Task<List<IdentityError>> UpdateUser(UserDataUpdate updateModel)
        {
            var user = await userManager.FindByEmailAsync(updateModel.OldEmail);
            List<IdentityError> identityErrors = new List<IdentityError>();
            if (!string.IsNullOrEmpty(updateModel.NewEmail))
            {
                var token = await userManager.GenerateChangeEmailTokenAsync(user, updateModel.NewEmail);
                var result = await userManager.ChangeEmailAsync(user, updateModel.NewEmail, token);
                identityErrors.AddRange(result.Errors);
                await userManager.SetUserNameAsync(user, updateModel.NewEmail);
            }
            if (!string.IsNullOrEmpty(updateModel.NewPassword))
            {
                var result = await userManager.ChangePasswordAsync(user, updateModel.OldPassword, updateModel.NewPassword);
                identityErrors.AddRange(result.Errors);
            }
            return identityErrors;
        }
        public async Task<AccessTokenData> RefreshToken(AccessTokenData accessTokenData)
        {
            var principal = jwtHandler.GetPrincipalFromExpiredToken(accessTokenData.AccessToken);
            var user = await userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null || user.RefreshToken != accessTokenData.RefreshToken
                || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new InvalidDataException("Refresh token is not valid!");
            }
            return jwtHandler.CreateToken(user);
        }
    }
}
