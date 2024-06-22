using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebStoreBackEnd.Models;
using WebStoreBackEnd.Models.Dto;
using WebStoreBackEnd.Services;

namespace WebStoreBackEnd.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IAuthenticationService authenticationService;
        private readonly IMapper mapper;

        public AuthController(IConfiguration config, IAuthenticationService authenticationService, IMapper mapper)
        {
            this.config = (IConfigurationRoot)config;
            this.authenticationService = authenticationService;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticationModelDto authModel, CancellationToken cancellationToken)
        {
            if (authModel == null)
                return BadRequest("Invalid client request");
            var model = mapper.Map<AuthenticationModel>(authModel);
            await authenticationService.RegisterNewModelAsync(model, cancellationToken);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<AccessToken>> Login([FromBody] AuthenticationModelDto loginModel, CancellationToken cancellationToken)
        {
            if (loginModel == null)
                return BadRequest("Invalid client request");
            if (await CheckAuthenticationModelAsync(loginModel, cancellationToken))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                        issuer: config["JwtSettings:Issuer"],
                        audience: config["JwtSettings:Audience"],
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signingCredentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                var accessToken = new AccessToken(token, tokenOptions.ValidTo);
                return Ok(accessToken);
            }
            return Unauthorized();
        }
        private async Task<bool> CheckAuthenticationModelAsync(AuthenticationModelDto loginModel, CancellationToken cancellationToken)
        {
            var model = mapper.Map<AuthenticationModel>(loginModel);
            return await authenticationService.CheckAuthenticationModelAsync(model, cancellationToken);
        }
    }
}
