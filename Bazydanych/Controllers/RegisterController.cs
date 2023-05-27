using Bazydanych.Context;
using Bazydanych.Helpers;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AppDB _auth;
        private readonly IConfiguration _conn;
        public RegisterController(AppDB appDB, IConfiguration configuration)
        {
            _auth = appDB;
            _conn = configuration;
        }
        [Authorize]
        [HttpPost]
        [ActionName("register")]
        public async Task<IActionResult> Reg(Register user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            var usertest = await _auth.Users.FirstOrDefaultAsync(x => x.Login == user.Login);
            if (usertest != null)
            {
                return BadRequest(new
                {
                    Message = "Użytkownik o tym loginie istnieje"
                });
            }
            if (user.UserRole == null)
            {
                return BadRequest(new
                {
                    Message = "Użytkownik musi posiadać przypisaną role"
                });
            }
            if(user.Pass != user.VerPass)
            {
                return BadRequest(new
                {
                    Message = "Hasła nie są identyczne"
                });
            }
            if (!Validate.ValidateNr(user.Phone) && user.Phone != "")
            {
                return BadRequest(new
                {
                    Message = "Niepoprawny numer"
                });
            }
            string query = @"insert into dbo.users
                            values (@login,@pass,@phone,@licence,@isdriver,1,0,null)";
            string query1 = @"insert into dbo.roles
                            values ((select id from users where login = @login), @userrole)";
            string sqlDataSource = _conn.GetConnectionString("DBCon");
            SqlDataReader Reader;
            SqlDataReader Reader1;
            string pass = Passwordhash.HashPassword(user.Pass);
            SqlTransaction transaction;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                
                connection.Open();
                transaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@login", user.Login);
                        command.Parameters.AddWithValue("@pass", pass);
                        if (user.Phone != null && user.Phone != "")
                        {
                            command.Parameters.AddWithValue("@phone", user.Phone);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@phone", DBNull.Value);
                        }
                        
                        command.Parameters.AddWithValue("@licence", user.Licence);
                        command.Parameters.AddWithValue("@isdriver", user.is_driver);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command1 = new SqlCommand(query1, connection, transaction))
                    {
                        command1.Parameters.AddWithValue("@userrole", user.UserRole);
                        command1.Parameters.AddWithValue("@login", user.Login);
                        command1.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    connection.Close();
                    return BadRequest(new
                    {
                        Message = ex.Message
                    });
                }
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono użytkownika."
            });
        }



    }
    }

