using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Core.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post(string username, string password)
        {
            //模拟登录
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "user")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.app(new string[] { "Jwt", "secret" })));

                var token = new JwtSecurityToken(
                    issuer: Appsettings.app(new string[] { "Jwt", "issuer" }),
                    audience: Appsettings.app(new string[] { "Jwt", "audience" }),
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { token = jwtToken });
            }
            else
            {
                return BadRequest("用户名或密码错误");
            }
        }

    }
}
