using Bazydanych.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

    }
}
