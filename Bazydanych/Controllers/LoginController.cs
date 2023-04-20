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
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDB _authcontext;
        private readonly IConfiguration _conn;

        public LoginController(AppDB appDB, IConfiguration configuration)
        {
            _authcontext = appDB;
            _conn = configuration;
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
        [ActionName("Login")]
        public async Task<IActionResult> Auth(User userObj)
        {
            if (userObj == null)
            {
                return BadRequest(new {
                    Message = "Błędne dane logowania" });
            }
            var user = await _authcontext.Users.FirstOrDefaultAsync(x => x.Login == userObj.Login);
            if (user == null)
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
            user.Token = CreateJwt(user, role);
            return Ok(new
            {

                Message = "Poprawnie zalogowano",
                Token = user.Token
            });
        }
        [Authorize]
        [HttpGet]
        [ActionName("users")]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _authcontext.Users.ToListAsync());
        }
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(User user)
        {

            string query = @"delete from Roles where userid = @id";
            string query1 = @"delete from Users where id = @id";
            string sqlDataSource = _conn.GetConnectionString("DBCon");
            SqlTransaction transaction;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@id", user.Id);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command1 = new SqlCommand(query1, connection, transaction))
                    {
                        command1.Parameters.AddWithValue("@id", user.Id);
                        command1.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                }
                connection.Close();
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono użytkownika."
            });
        }
    }

    
}
