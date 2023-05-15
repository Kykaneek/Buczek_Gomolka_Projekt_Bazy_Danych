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

            string query = @"select c.name Contractor,l.Name src_location,lc.Name des_location,t.distance,CONVERT(VARCHAR(5), t.travel_time, 108) travel_time from Trace t
                            join Contractors c on c.ID = t.contractor_id
                            join Location l on l.ID = t.Start_location
                            join Location lc on lc.ID = t.Finish_location
                            ";
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
                        command.Parameters.AddWithValue("@id", trace1.Id);
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
            var TraceTest = await _authcontext.Contractors.FirstOrDefaultAsync(x => x.Id == trace1.ContractorId);
            if (TraceTest == null)
            {
                return BadRequest(new
                {
                    Message = "Brak kontrahenta"
                });
            }


            string query = @"insert into dbo.trace
                            values (@contractorid,@start_lok,@end_lok,@distance,@timetravel)";
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
                        command.Parameters.AddWithValue("@contractorid", trace1.ContractorId);
                        command.Parameters.AddWithValue("@start_lok", trace1.StartLocation);
                        command.Parameters.AddWithValue("@end_lok", trace1.FinishLocation);
                        command.Parameters.AddWithValue("@distance", trace1.Distance);
                        command.Parameters.AddWithValue("@timetravel", trace1.TravelTime);
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
                connection.Close();
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono Trasę."
            });
        }
    }





}

