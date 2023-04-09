using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDB _authcontext;

        public LoginController(AppDB appDB)
        {
            _authcontext = appDB;
        }
        private string CreateJwt(User user, Role roles)
        {

            var jwtHandler = new JwtSecurityTokenHandler();
            var key_encode = Encoding.ASCII.GetBytes("CreateKeySecrect");
            var identity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Role,roles.UserRole),
            new Claim(ClaimTypes.Name,$"{user.Login}")
            });
            var creedindetials = new SigningCredentials(new SymmetricSecurityKey(key_encode), SecurityAlgorithms.HmacSha256);

            var TokenDescrypter = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(20),
                SigningCredentials = creedindetials
            };
            var token = jwtHandler.CreateToken(TokenDescrypter);
            return jwtHandler.WriteToken(token);
        }


        [HttpPost]
        public async Task<IActionResult> Auth(User userObj)
        {
            if(userObj == null)
            {
                return BadRequest(new {
                    Message = "Błędne dane logowania" });
            }
            var user = await _authcontext.Users.FirstOrDefaultAsync(x => x.Login == userObj.Login );
            if ( user == null)
                return NotFound(new { Message = "Błędne dane logowania" });
            if (!Passwordhash.Veryfypass(userObj.Pass, user.Pass)) // Admin --hash 6rKKXOPYG7UVjNGcDdat4M1S427v3vu3cnggkmfe8R/aKuJ3
            {
                return BadRequest(new {
                    Message = "Błędne hasło logowania" });
            }
            var role = await _authcontext.Roles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (role == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane logowania"
                });

            }
            user.Token = CreateJwt(user,role);
            return Ok(new
            {
                
                Message = "Poprawnie zalogowano",
                Token = user.Token
            }) ;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _authcontext.Users.ToListAsync());
        }
    }

    
}
