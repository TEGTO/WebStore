using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using WebStoreBackEnd.Contracts;
using WebStoreBackEnd.Models;
using WebStoreBackEnd.Models.Dto;
using WebStoreBackEnd.Services;

namespace WebStoreBackEnd.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly JwtHandler jwtHandler;

        public AuthController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.jwtHandler = jwtHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userForRegistrationlDto)
        {
            if (userForRegistrationlDto == null)
                return BadRequest("Invalid client request");
            var user = mapper.Map<User>(userForRegistrationlDto);
            var result = await userManager.CreateAsync(user, userForRegistrationlDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Where(e => !e.Description.Contains("Username")).Select(e => e.Description).ToArray();
                return BadRequest(new ResponseError { StatusCode = $"{(int)HttpStatusCode.BadRequest}", Messages = errors.ToArray() });
            }
            return Created();
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] UserAuthenticationDto loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginModel.Password))
                return Unauthorized(new ResponseError { StatusCode = HttpStatusCode.Unauthorized.ToString(), Messages = ["Invalid Authentication"] });
            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaims(user);
            var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { Token = token, ExpiredOn = tokenOptions.ValidTo });
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateDataDto updateModel)
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
            if (identityErrors.Count > 0)
            {
                var errors = identityErrors.Where(e => !e.Description.Contains("Username")).Select(e => e.Description).ToArray();
                return BadRequest(new ResponseError { StatusCode = $"{(int)HttpStatusCode.BadRequest}", Messages = errors.ToArray() });
            }
            return Ok();
        }
    }
}
