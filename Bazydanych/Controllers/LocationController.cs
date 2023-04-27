using Bazydanych.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        private readonly AppDB _authcontext;
        private readonly IConfiguration _conn;


        public LocationController(AppDB authcontext, IConfiguration conn)
        {
            _authcontext = authcontext;
            _conn = conn;
        }

        [Authorize]
        [HttpGet]
        [ActionName("Getall")]
        public JsonResult GetAllContractors()
        {

            string query = @"select * from Location";
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

    }
}
