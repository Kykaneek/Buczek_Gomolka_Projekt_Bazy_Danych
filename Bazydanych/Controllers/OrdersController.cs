using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDB _authcontext;
        private readonly IConfiguration _conn;
        public OrdersController(AppDB authcontext, IConfiguration conn)
        {
            _authcontext = authcontext;
            _conn = conn;
        }


        [Authorize]
        [HttpGet]
        [ActionName("GetAll")]
        public JsonResult GetAll()
        {
            string query = @"select * from OrdersListItemView";
            DataTable data = new DataTable();
            SqlDataReader reader;
            string Datasource = _conn.GetConnectionString("DBCon");
            SqlTransaction transaction;

            using (SqlConnection connection = new SqlConnection(Datasource))
            {
                connection.Open();
                transaction= connection.BeginTransaction();
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
                }catch(Exception ex)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return new JsonResult(data);
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("ADD")]
        public async Task<IActionResult> ADD (Orders order)
        {
            if (order == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            
            var ContractorTest = await _authcontext.Contractors.FirstOrDefaultAsync(x => x.Id == order.contractorID);
            if (ContractorTest == null)
            {
                return BadRequest(new
                {
                    Message = "Brak takiego kontrahenta"
                });
            }
            var carTest = await _authcontext.Car.FirstOrDefaultAsync(x => x.Id == order.CarId);
            if (carTest == null)
            {
                return BadRequest(new
                {
                    Message = "Brak pojazdu"
                });
            }
            var tracetest = await _authcontext.Traces.FirstOrDefaultAsync(x => x.Id == order.TraceId);
            if (tracetest == null)
            {
                return BadRequest(new
                {
                    Message = "Brak trasy"
                });
            }
           

          


            string query = @"insert into dbo.loading values (@contractor,@trace,@car,@pickupdate,@timetoloading,null)";
            string query1 = @"insert into dbo.unloading values (@timetounloading,(select TOP 1 id from loading where contractorID = @contractor order by id desc))";

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
                        command.Parameters.AddWithValue("@contractor", order.contractorID);
                        command.Parameters.AddWithValue("@trace", order.TraceId);
                        command.Parameters.AddWithValue("@car", order.CarId);
                        command.Parameters.AddWithValue("@pickupdate", order.Pickupdate);
                        command.Parameters.AddWithValue("@timetoloading", order.Time_To_Loading);
                        command.ExecuteNonQuery();

                    }

                    using (SqlCommand command = new SqlCommand(query1, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@timetounloading", order.Time_To_Unloading);
                        command.Parameters.AddWithValue("@contractor", order.contractorID);
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
                Message = "Poprawnie utworzono Zlecenie."
            });
        }

    }
}
