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
    public class DeliveryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public DeliveryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select DeliveryId,OrderId,DeliveryDate,
                             DeliveryAddress,DeliveryStatus 
                              from Delivery ";

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
        public JsonResult Post(Delivery d)
        {
            string query = @"
                             insert into Delivery
                             values (@OrderId, @DeliveryDate,@DeliveryAddress,@DeliveryStatus)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@OrderId", d.OrderId);
                    myCommand.Parameters.AddWithValue("@DeliveryDate", d.DeliveryDate);
                    myCommand.Parameters.AddWithValue("@DeliveryAddress", d.DeliveryAddress);
                    myCommand.Parameters.AddWithValue("@DeliveryStatus", d.DeliveryStatus);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Delivery d)
        {
            string query = @"
                           update Delivery
                           set OrderId= @OrderId,
                            DeliveryDate=@DeliveryDate,
                            DeliveryAddress=@DeliveryAddress,
                            DeliveryStatus=@DeliveryStatus
                            where DeliveryId=@DeliveryId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DeliveryId", d.DeliveryId); ;
                    myCommand.Parameters.AddWithValue("@OrderId", d.OrderId);
                    myCommand.Parameters.AddWithValue("@DeliveryDate", d.DeliveryDate);
                    myCommand.Parameters.AddWithValue("@DeliveryAddress", d.DeliveryAddress);
                    myCommand.Parameters.AddWithValue("@DeliveryStatus", d.DeliveryStatus);
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
                           delete from Delivery
                            where DeliveryId=@DeliveryId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DeliveryId", id);

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
