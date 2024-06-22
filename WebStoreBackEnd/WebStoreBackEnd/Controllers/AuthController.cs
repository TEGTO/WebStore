using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebStoreBackEnd.Contracts;
using WebStoreBackEnd.Models;
using WebStoreBackEnd.Models.Dto;

namespace WebStoreBackEnd.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AuthController(IConfiguration config, UserManager<User> userManager, IMapper mapper)
        {
            this.config = (IConfigurationRoot)config;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationlDto userForRegistrationlDto, CancellationToken cancellationToken)
        {
            if (userForRegistrationlDto == null)
                return BadRequest("Invalid client request");
            var user = mapper.Map<User>(userForRegistrationlDto);
            var result = await userManager.CreateAsync(user, userForRegistrationlDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new ResponseError { StatusCode = HttpStatusCode.BadRequest.ToString(), Messages = errors.ToArray() });
            }
            return Created();
        }
        //[HttpPost("login")]
        //public async Task<ActionResult<AccessToken>> Login([FromBody] UserRegistrationlDto loginModel, CancellationToken cancellationToken)
        //{
        //    if (loginModel == null)
        //        return BadRequest("Invalid client request");
        //    if (await CheckAuthenticationModelAsync(loginModel, cancellationToken))
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!));
        //        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //        var tokenOptions = new JwtSecurityToken(
        //                issuer: config["JwtSettings:Issuer"],
        //                audience: config["JwtSettings:Audience"],
        //                claims: new List<Claim>(),
        //                expires: DateTime.Now.AddMinutes(10),
        //                signingCredentials: signingCredentials
        //        );
        //        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //        var accessToken = new AccessToken(token, tokenOptions.ValidTo);
        //        return Ok(accessToken);
        //    }
        //    return Unauthorized();
        //}
        //private async Task<bool> CheckAuthenticationModelAsync(UserRegistrationlDto loginModel, CancellationToken cancellationToken)
        //{
        //    var model = mapper.Map<AuthenticationModel>(loginModel);
        //    return await authenticationService.CheckAuthenticationModelAsync(model, cancellationToken);
        //}
    }
}
