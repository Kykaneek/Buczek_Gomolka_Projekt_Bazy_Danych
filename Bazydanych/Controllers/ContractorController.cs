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
using Newtonsoft.Json;

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
                    return BadRequest(new
                    {
                        Message = ex.Message
                    });
                }
                connection.Close();
            }
            return Ok(new
            {
                Message = "Poprawnie utworzono kontrahenta."
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
            if (Contractor.Nip!= null)
            {
                if (!Validate.IsValidNIP(Contractor.Nip))
                {
                    return BadRequest(new
                    {
                        Message = "Kontrahent posiada nieprawidłowy NIP"
                    });
                }
            }
           
            if(Contractor.Pesel != null)
            {
                if (!Validate.IsValidPESEL(Contractor.Pesel))
                {
                    return BadRequest(new
                    {
                        Message = "Kontrahent posiada nieprawidłowy PESEL"
                    });
                }
            }
            if (Contractor.Pesel == null && Contractor.Nip == null)
            {
                return BadRequest(new
                {
                    Message = "Wymagane jest podanie PESEL lub NIP"
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
                        if(Contractor.Pesel != null) {
                            command.Parameters.AddWithValue("@PESEL", Contractor.Pesel);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@PESEL", DBNull.Value);
                        }

                        if (Contractor.Nip != null)
                        {
                            command.Parameters.AddWithValue("@NIP", Contractor.Nip);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@NIP", DBNull.Value);
                        }
                        
                       
                        command.Parameters.AddWithValue("@locationID", Contractor.LocationId);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
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
                Message = "Poprawnie utworzono Kontrahenta."
            });
        }


        [Authorize]
        [HttpPut]
        [ActionName("update")]
        public async Task<IActionResult> Reg(Contractor Contractor)
        {
            if (Contractor == null)
            {
                return BadRequest(new
                {
                    Message = "Błędne dane"
                });
            }
            if (Contractor.Name == null)
            {
                return BadRequest(new
                {
                    Message = "Użytkownik musi posiadać przypisaną role"
                });
            }
            var Contractortmp = await _authcontext.Contractors.FirstOrDefaultAsync(x => x.Id == Contractor.Id);
            var owner = _authcontext.Contractors.Where(x => x.Name == Contractor.Name);

            if (Contractor.Nip != null && Contractor.Nip != "")
            {
                if (!Validate.IsValidNIP(Contractor.Nip))
                {
                    return BadRequest(new
                    {
                        Message = "Kontrahent posiada nieprawidłowy NIP"
                    });
                }
            }

            if (Contractor.Pesel != null && Contractor.Pesel != "")
            {
                if (!Validate.IsValidPESEL(Contractor.Pesel))
                {
                    return BadRequest(new
                    {
                        Message = "Kontrahent posiada nieprawidłowy PESEL"
                    });
                }
            }
            if (Contractor.Pesel == null && Contractor.Nip == null || Contractor.Pesel == null && Contractor.Nip == "" || Contractor.Pesel == "" && Contractor.Nip == null || Contractor.Pesel == "" && Contractor.Nip == "")
            {
                return BadRequest(new
                {
                    Message = "Wymagane jest podanie PESEL lub NIP"
                });
            }

            if (owner.Count() >= 1)
            {
                var check = await _authcontext.Contractors.FirstOrDefaultAsync(x => x.Name == Contractor.Name);
                if (check != Contractortmp)
                {
                    return BadRequest(new
                    {
                        Message = "Zmina nazwy kontrahenta nie jest możliwa - taki kontrahenta istnieje"
                    });
                }
            }
            if (Contractortmp == null)
            {
                return BadRequest(new
                {
                    Message = "kontrahenta musi posiadać nazwe"
                });
            }
            
            using (_authcontext)
            {
                if (Contractortmp != null)
                {
                    Contractortmp.Name = Contractor.Name;
                    if (Contractor.Pesel != null && Contractor.Pesel != "")
                    {
                        Contractortmp.Pesel = Contractor.Pesel;
                    }
                    else
                    {
                        Contractortmp.Pesel = null;
                    }
                    if (Contractor.Nip != null && Contractor.Nip != "")
                    {
                        Contractortmp.Nip = Contractor.Nip;
                    }
                    else
                    {
                        Contractortmp.Nip = null;
                    }
                    _authcontext.SaveChanges();

                }
                else
                {
                    return NotFound();
                }
            }


            return Ok(new
            {
                Message = "Poprawnie edytowano kontrahenta."
            });
        }

        [Authorize]
        [HttpGet]
        [ActionName("getcontractor")]
        public JsonResult GetAllContractors(string contractor)
        {

            string query = @"select * from contractors where id = @contracotrID";
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
                        command.Parameters.AddWithValue("@contracotrID", contractor);
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
