using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Bazydanych.Helpers;
using Microsoft.EntityFrameworkCore.Storage;



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
        [ActionName("getAllLocations")]
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




        [Authorize]
        [HttpPost]
        [ActionName("addLocation")]
        public async Task<IActionResult> AddLocation(Location lokalizacja)
        {
            if (lokalizacja == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            var LocationTest = await _authcontext.Locations.FirstOrDefaultAsync(x => x.Name == lokalizacja.Name);
            if (LocationTest != null)
            {
                return BadRequest(new
                {
                    Message = "Brak lokalizacji"
                });
            }


            string query = @"insert into dbo.location
                            values (@locationId,@name,@city,@street,@number)";
            string sqlDataSource = _conn.GetConnectionString("DBCon");
            //SqlTransaction transaction;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                /* transaction = connection.BeginTransaction();
                 try
                 {*/
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@locationId", "");
                    command.Parameters.AddWithValue("@name", lokalizacja.Name);
                    command.Parameters.AddWithValue("@city", lokalizacja.City);
                    command.Parameters.AddWithValue("@street", lokalizacja.Street);
                    command.Parameters.AddWithValue("@number", lokalizacja.Number);
                    command.ExecuteNonQuery();
                }/*
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    BadRequest(new
                    {
                        Message = "Komunikat o takiej samej lokalizacji."
                    });
                }*/
                connection.Close();
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono Lokalizację."
            });




        }
    }
}
