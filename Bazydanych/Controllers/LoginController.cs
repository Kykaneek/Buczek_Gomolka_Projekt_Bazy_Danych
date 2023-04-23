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
using System.Net;
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
                return BadRequest(new
                {
                    Message = "Błędne dane logowania"
                });
            }
            var user = await _authcontext.Users.FirstOrDefaultAsync(x => x.Login == userObj.Login);
            if (user == null)
                return NotFound(new { Message = "Błędne dane logowania" });
            if (!Passwordhash.Veryfypass(userObj.Pass, user.Pass)) // Admin --hash 6rKKXOPYG7UVjNGcDdat4M1S427v3vu3cnggkmfe8R/aKuJ3
            {
                return BadRequest(new
                {
                    Message = "Błędne hasło logowania"
                });
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
        [Authorize]
        [HttpGet]
        [ActionName("getuser")]
        public JsonResult GetAllContractors(string user)
        {

            string query = @"select login,phone,licence,is_driver,UserRole from users u join roles r on r.userid= u.id where u.id = @userid";
            DataTable data = new DataTable();
            SqlDataReader reader;
            string DataSource = _conn.GetConnectionString("DBCon");
            SqlTransaction transaction;
            using (SqlConnection connection = new SqlConnection(DataSource))
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@userid", user);
                        reader = command.ExecuteReader();
                        data.Load(reader);
                        reader.Close();

                    }
                    transaction.Commit();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return new JsonResult(data);
            }

        }


        [Authorize]
        [HttpPut]
        [ActionName("update")]
        public async Task<IActionResult> Reg(Register user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            if (user.UserRole == null)
            {
                return BadRequest(new
                {
                    Message = "Użytkownik musi posiadać przypisaną role"
                });
            }
            var usertmp = await _authcontext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            var owner = _authcontext.Users.Where(x => x.Login == user.Login);
            if (owner.Count() >= 1)
            {
                var check = await _authcontext.Users.FirstOrDefaultAsync(x => x.Login == user.Login);
                if (check != usertmp)
                {
                    return BadRequest(new
                    {
                        Message = "Zmina nazwy użytkownika nie jest możliwa - taki użytkownik istnieje"
                    });
                }
            }
            var role = await _authcontext.Roles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (usertmp == null || role == null) {
                 return BadRequest(new
                {
                    Message = "Użytkownik musi posiadać przypisaną role"
                });
            }
            using (_authcontext)
            {
                if (usertmp != null)
                {
                    usertmp.Login = user.Login;
                    usertmp.Licence = user.Licence;
                    usertmp.is_driver = user.IsDriver;
                    usertmp.Phone = user.Phone;
                    role.UserRole = user.UserRole;
                    _authcontext.SaveChanges();
                        
                }
                else
                {
                    return NotFound();
                }
            }


            return Ok(new
            {
                Message = "Poprawnie utworzono użytkownika."
            });
        }





    }
}
