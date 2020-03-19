using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace B2C.IPificationDemo.Controllers
{
    [ApiController]
    [RequireHttps]
    public class ApiController : Controller
    {
        private static double TOKEN_DURATION = 180;

        [HttpPost]
        [Route("createjwt")]
        [AllowAnonymous]
        public IActionResult CreateJwt([FromBody] Dictionary<string, string> content)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest("Authorization header is required.");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

            var issuer = credentials[0];
            var hmacSecret = credentials[1];

            if (hmacSecret.Length < 16)
            {
                throw new Exception("Insufficient HMAC secret length.");
            }

            var signingCred = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hmacSecret)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer,
                null,
                content.Select(kvp => new Claim(kvp.Key, kvp.Value)),
                DateTime.Now,
                DateTime.Now.AddSeconds(TOKEN_DURATION),
                signingCred
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return new JsonResult(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
