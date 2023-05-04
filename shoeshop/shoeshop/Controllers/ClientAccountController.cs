using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using shoeshop.Model;
using System.IO;


namespace shoeshop.Controllers
    
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ClientAccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ClientId,FirstName,
                                   LastName,Email,Password,Address,City,PostalCode,PhoneNumber,Gender
                              from ClientAccount ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(ClientAccount ca)
        {
            string query = @"
                             insert into ClientAccount
                             values (@FirstName, @LastName,@Email,@Password,@Address,@City,@PostalCode,@PhoneNumber,@Gender)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", ca.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", ca.LastName);
                    myCommand.Parameters.AddWithValue("@Email", ca.Email);
                    myCommand.Parameters.AddWithValue("@Password", ca.Password);
                    myCommand.Parameters.AddWithValue("@Address", ca.Address);
                    myCommand.Parameters.AddWithValue("@City", ca.City);
                    myCommand.Parameters.AddWithValue("@PostalCode", ca.PostalCode);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", ca.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@Gender", ca.Gender);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(ClientAccount ca)
        {
            string query = @"
                           update ClientAccount
                           set FirstName= @FirstName,
                            LastName=@LastName,
                            Email=@Email,
                            Password=@Password,
                            Address=@Address,
                            City=@City,
                            PostalCode=@PostalCode,
                            PhoneNumber=@PhoneNumber,
                            Gender=@Gender



                            where ClientId=@ClientId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ClientId", ca.ClientId);
                    myCommand.Parameters.AddWithValue("@FirstName", ca.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", ca.LastName);
                    myCommand.Parameters.AddWithValue("@Email", ca.Email);
                    myCommand.Parameters.AddWithValue("@Password", ca.Password);
                    myCommand.Parameters.AddWithValue("@Address", ca.Address);
                    myCommand.Parameters.AddWithValue("@City", ca.City);
                    myCommand.Parameters.AddWithValue("@PostalCode", ca.PostalCode);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", ca.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@Gender", ca.Gender);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from ClientAccount
                            where ClientId=@ClientId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ClientId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }




    }
}
