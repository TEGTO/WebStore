using AuthenticationManager.Models;
using AuthenticationWebApi.Models;
using AuthenticationWebApi.Models.Dto;
using AuthenticationWebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middlewares.Contracts;
using System.Net;

namespace AuthenticationWebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            if (userRegistrationRequest == null)
                return BadRequest("Invalid client request");
            var user = mapper.Map<User>(userRegistrationRequest);
            var result = await authService.RegisterUserAsync(user, userRegistrationRequest.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Where(e => !e.Description.Contains("Username")).Select(e => e.Description).ToArray();
                return BadRequest(new ResponseError { StatusCode = $"{(int)HttpStatusCode.BadRequest}", Messages = errors.ToArray() });
            }
            return Created();
        }
        [HttpPost("login")]
        public async Task<ActionResult<AccessTokenDto>> Login([FromBody] UserAuthenticationRequest loginModel)
        {
            int expiryInDays = 7; //Hardcode, because i'm lazy and it's only a test project 
            var token = await authService.LoginUserAsync(loginModel.Email, loginModel.Password, expiryInDays);
            var tokenDto = mapper.Map<AccessTokenDto>(token);
            tokenDto.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(expiryInDays);
            return Ok(tokenDto);
        }
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserUpdateDataRequest updateModel)
        {
            UserDataUpdate userDataUpdate = mapper.Map<UserDataUpdate>(updateModel);
            var identityErrors = await authService.UpdateUser(userDataUpdate);
            if (identityErrors.Count > 0)
            {
                var errors = identityErrors.Where(e => !e.Description.Contains("Username")).Select(e => e.Description).ToArray();
                return BadRequest(new ResponseError { StatusCode = $"{(int)HttpStatusCode.BadRequest}", Messages = errors.ToArray() });
            }
            return Ok();
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<AccessTokenDto>> Refresh([FromBody] AccessTokenDto accessTokenDto)
        {
            AccessTokenData accessToken = mapper.Map<AccessTokenData>(accessTokenDto);
            var newToken = await authService.RefreshToken(accessToken);
            return Ok(newToken);
        }
    }
}
