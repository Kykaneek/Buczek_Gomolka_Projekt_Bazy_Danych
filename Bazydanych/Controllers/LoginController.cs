using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Plugins;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        [HttpPost]
        public async Task<IActionResult> Auth(User userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }
            var user = await _authcontext.Users.FirstOrDefaultAsync(x => x.Login == userObj.Login );
            if ( user == null)
                return NotFound(new { Message = "Błędne dane logowania" });
            if (!Passwordhash.Veryfypass(userObj.Pass, user.Pass)) // Admin --hash 6rKKXOPYG7UVjNGcDdat4M1S427v3vu3cnggkmfe8R/aKuJ3
            {
                return BadRequest(new { Message = "Błędne hasło logowania" });
            }
            return Ok(new
            {
                Message = "Poprawnie zalogowano"
            });
        }
    }
 
}
