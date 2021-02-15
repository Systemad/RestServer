using Microsoft.AspNetCore.Mvc;
using WeatherLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RestServer.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration Configuration;
        private readonly InventoryContext Context;
        
        public TokenController(IConfiguration config, InventoryContext context)
        {
            Configuration = config;
            Context = context;
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Post(UserLoginInfo userLoginInfo)
        {
            if (userLoginInfo != null && userLogin. != null && userLoginInfo.Password != null)
            {
                var user = await GetUser(userLoginInfo.Email, userLoginInfo.Password);
 
                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.UserId.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email)
                    };
 
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
 
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
 
                    var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
 
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
            
    }
}