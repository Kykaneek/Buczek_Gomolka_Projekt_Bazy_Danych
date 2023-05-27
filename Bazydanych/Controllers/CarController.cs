using Bazydanych.Context;
using Bazydanych.Helpers;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

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

            string query = @"select c.*,u.login, CONVERT(VARCHAR(10), buy_date, 105) kupno from Cars c left join Users u on u.id = c.Driver ";
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
                    connection.Close();
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
                    Message = "Ten kierowca ma już przypisany pojazd"
                });
            }
            var UserTest = await _authcontext.Users.FirstOrDefaultAsync(x => x.Id == pojazd.Driver);
            if (UserTest == null)
            {
                return BadRequest(new
                {
                    Message = "Brak takiego użytkownika"
                });
            }
            if (!UserTest.is_driver)
            {
                return BadRequest(new
                {
                    Message = "Pracownik nie jest kierowcą"
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
                    using (SqlCommand command = new SqlCommand(query, connection,transaction))
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
                Message = "Poprawnie utworzono Pojazd."
            });

        }


        [Authorize]
        [HttpPost]
        [ActionName("DeleteCar")]
        public async Task<IActionResult> DeleteCar(Car pojazd)
        {

             var checkTrace = await _authcontext.PlannedTraces.FirstOrDefaultAsync(x => x.CarId == pojazd.Id);
            if (checkTrace != null)
            {
                return BadRequest(new
                {
                    Message = "Istnieją zaplanowane trasy dla tego pojazdu"
                });
            }



            string query = @"delete from cars where id = @id";
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
                        command.Parameters.AddWithValue("@id", pojazd.Id);
                        command.ExecuteNonQuery();
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
                Message = "Poprawnie usunięto pojazd."
            });
        }








        [Authorize]
        [HttpGet]
        [ActionName("getCar")]
        public JsonResult GetAllCars(string car)
        {

            string query = @"select CONVERT(VARCHAR(10), c.buy_date, 31) kupno, c.*,u.login from cars c 
                            left join users u on u.id = c.driver where c.id = @carid";
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
                        command.Parameters.AddWithValue("@carid", car);
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
                    connection.Close();
                }
                return new JsonResult(data);
            }

        }

        [Authorize]
        [HttpPut]
        [ActionName("update")]
        public async Task<IActionResult> update(Car car)
        {
            if (car == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            if (car.Driver == null)
            {
                return BadRequest(new
                {
                    Message = "Brak kierowcy"
                });
            }
            var cartmp = await _authcontext.Car.FirstOrDefaultAsync(x => x.Id == car.Id);
            var owner = _authcontext.Car.Where(x => x.Driver == car.Driver);
            if (owner.Count() > 1)
            {

                    return BadRequest(new
                    {
                        Message = "Zmiana kierowcy nie jest możliwa, ten kierowca posiada już pojazd"
                    });
            }
            using (_authcontext)
            {
                if (cartmp != null)
                {
                    cartmp.Driver = car.Driver;
                    cartmp.IS_truck = car.IS_truck;
                    cartmp.Buy_Date = car.Buy_Date;
                    cartmp.Registration_Number = car.Registration_Number;
                    cartmp.is_available = car.is_available;
                    cartmp.Mileage = car.Mileage;
                    cartmp.loadingsize= car.loadingsize;
                    _authcontext.SaveChanges();

                }
                else
                {
                    return NotFound();
                }
            }


            return Ok(new
            {
                Message = "Poprawna edycja pojazdu."
            });
        }

    }
}
