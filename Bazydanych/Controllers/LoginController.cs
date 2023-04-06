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

        [HttpPost("login")]
        public async Task<IActionResult> Auth([FromBody] User userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }
            var user = await _authcontext.Users.FirstOrDefaultAsync(x => x.Login == userObj.Login && x.Pass == userObj.Pass);
            if ( user == null)
                return NotFound(new { Message = "Błędne dane logowania" });
            return Ok(new
            {
                Message = "Poprawnie zalogowano"
            });
        }
    }
 
}
