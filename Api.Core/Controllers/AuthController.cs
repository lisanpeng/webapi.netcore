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
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
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


            return Ok(new { Token = jwtToken });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
