using Application.DTOs.UserDto;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Currency_converter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public AuthenticateController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticate credentials)
        {
            var userAuthenticated = await _userService.AuthenticateUserAsync(credentials);
            if (userAuthenticated is not null)
            {

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

                SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);


                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userAuthenticated.Id.ToString()));
                claimsForToken.Add(new Claim("email", userAuthenticated.Email));
                //claimsForToken.Add(new Claim(ClaimTypes.Role, userAuthenticated.Role.ToString()));



                var jwtSecurityToken = new JwtSecurityToken(
                  _config["Authentication:Issuer"],
                  _config["Authentication:Audience"],
                  claimsForToken,
                  notBefore: DateTime.UtcNow,
                  expires: DateTime.UtcNow.AddMinutes(60),

                  signature);

                var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

                return Ok(tokenToReturn);
            }
            return Unauthorized();
        }
    }
}