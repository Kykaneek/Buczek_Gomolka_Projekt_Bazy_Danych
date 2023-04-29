using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly AppDB _authcontext;
        private readonly IConfiguration _conn;


        public CarController(AppDB authcontext, IConfiguration conn)
        {
            _authcontext = authcontext;
            _conn = conn;
        }

        [Authorize]
        [HttpGet]
        [ActionName("Getall")]
        public JsonResult GetAllCars()
        {

            string query = @"select *, CONVERT(VARCHAR(10), buy_date, 105) kupno from Cars";
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
        [ActionName("addCar")]
        public async Task<IActionResult> AddCar(Car pojazd)
        {
            if (pojazd == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            var CarTest = await _authcontext.Car.FirstOrDefaultAsync(x => x.Driver == pojazd.Driver);
            if (CarTest != null)
            {
                return BadRequest(new
                {
                    Message = "Brak lokalizacji"
                });
            }

            string query = @"insert into dbo.cars values (@driver,@register_number,@mileage,@buy_date, @isTruck, @loadingsize, @isAvailable)";

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

                        command.Parameters.AddWithValue("@driver", pojazd.Driver);
                        command.Parameters.AddWithValue("@register_number", pojazd.Registration_Number);
                        command.Parameters.AddWithValue("@mileage", pojazd.Mileage);
                        command.Parameters.AddWithValue("@buy_date", pojazd.Buy_Date);
                        command.Parameters.AddWithValue("@isTruck", pojazd.IS_truck);
                        command.Parameters.AddWithValue("@loadingsize", pojazd.loadingsize);
                        command.Parameters.AddWithValue("@isAvailable", pojazd.is_available);
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    return BadRequest(new
                    {
                        Message = ex.Message
                    });
                }
                connection.Close();
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono Pojazd."
            });

        }



    }
}
