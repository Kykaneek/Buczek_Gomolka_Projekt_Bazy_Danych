using Bazydanych.Context;
using Bazydanych.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Bazydanych.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly AppDB _authcontext;
        private readonly IConfiguration _conn;


        public ContractorController(AppDB authcontext, IConfiguration conn)
        {
            _authcontext = authcontext;
            _conn = conn;
        }

        [Authorize]
        [HttpGet]
        [ActionName("Getall")]
        public JsonResult GetAllContractors()
        {

            string query = @"select * from Contractors";
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
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteContractor(Contractor contractor1)
        {

            string query = @"delete from contractors where id = @id";
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
                        command.Parameters.AddWithValue("@id", contractor1.Id);
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
                Message = "Poprawnie utworzono użytkownika."
            });
        }


        [Authorize]
        [HttpPost]
        [ActionName("AddContractor")]
        public async Task<IActionResult> AddContractor(Contractor Contractor)
        {
            if (Contractor == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            var ContractorTest = await _authcontext.Contractors.FirstOrDefaultAsync(x => x.Name == Contractor.Name);
            if (ContractorTest != null)
            {
                return BadRequest(new
                {
                    Message = "Kontrahent o tej nazwie istnieje"
                });
            }


            string query = @"insert into dbo.contractors
                            values (@name,@NIP,@PESEL,@locationID)";
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
                        command.Parameters.AddWithValue("@name", Contractor.Name);
                        command.Parameters.AddWithValue("@NIP", Contractor.Nip);
                        command.Parameters.AddWithValue("@PESEL", Contractor.Pesel);
                        command.Parameters.AddWithValue("@locationID", "");
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    BadRequest(new
                    {
                        Message = "Kontrahent o tej nazwie istnieje"
                    });
                }
                connection.Close();
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono Kontrahenta."
            });
        }
    }
}
