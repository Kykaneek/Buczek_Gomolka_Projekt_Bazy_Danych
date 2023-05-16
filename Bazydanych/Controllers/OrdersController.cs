using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var TraceTest = await _authcontext.Traces.FirstOrDefaultAsync(x => x.Id == order.contractorID);
            if (TraceTest == null)
            {
                return BadRequest(new
                {
                    Message = "Brak Trasy"
                });
            }
            if (TraceTest.ContractorId != order.contractorID)
            {
                return BadRequest(new
                {
                    Message = "Kontrahent nie posiada takiej trasy"
                });
            }

          


            string query = @"insert into dbo.loading values ()";
            string query1 = @"insert into dbo.unloading values ()";

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

                        command.ExecuteNonQuery();

                    }

                    using (SqlCommand command = new SqlCommand(query1, connection,transaction))
                    {

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

    }
}
