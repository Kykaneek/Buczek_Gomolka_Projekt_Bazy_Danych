using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TraceController : ControllerBase
    {


        private readonly AppDB _authcontext;
        private readonly IConfiguration _conn;


        public TraceController(AppDB authcontext, IConfiguration conn)
        {
            _authcontext = authcontext;
            _conn = conn;
        }

        [Authorize]
        [HttpGet]
        [ActionName("Getall")]
        public JsonResult GetAllTraces()
        {

            string query = @"select * from Trace";
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
        [ActionName("DeleteTrace")]
        public async Task<IActionResult> DeleteTrace(Trace trace1)
        {

            string query = @"delete from trace where id = @id";
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
                        command.Parameters.AddWithValue("@id", trace1.ContractorId);
                        command.ExecuteNonQuery();
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
                Message = "Poprawnie usunięto trasę."
            });
        }


        [Authorize]
        [HttpPost]
        [ActionName("AddTrace")]
        public async Task<IActionResult> AddTrace(Trace trace1)
        {
            if (trace1 == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            var TraceTest = await _authcontext.Traces.FirstOrDefaultAsync(x => x.PlannedTraces == trace1.PlannedTraces);
            if (TraceTest != null)
            {
                return BadRequest(new
                {
                    Message = "Trasa nie istnieje"
                });
            }


            string query = @"insert into dbo.trace
                            values (@start_lok,@end_lok,@distance,@timetravel)";
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
                        command.Parameters.AddWithValue("@start_lok", trace1.StartLocation);
                        command.Parameters.AddWithValue("@end_lok", trace1.FinishLocation);
                        command.Parameters.AddWithValue("@distance", trace1.Distance);
                        command.Parameters.AddWithValue("@timetravel", trace1.TravelTime);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    BadRequest(new
                    {
                        Message = "Trasa o tej nazwie istnieje."
                    });
                }
                connection.Close();
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono Trasę."
            });
        }
    }





}

