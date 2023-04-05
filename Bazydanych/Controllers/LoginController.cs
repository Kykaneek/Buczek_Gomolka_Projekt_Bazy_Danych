using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Check()
        {
            string query = @"Select * from Users";
            DataTable table = new DataTable();
            string DataSource = _configuration.GetConnectionString("DBCon");
            SqlDataReader reader;
            using (SqlConnection conn= new SqlConnection(DataSource))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn)) {
                    reader = command.ExecuteReader();
                    table.Load(reader);
                reader.Close();
                conn.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
