using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
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
    public class ClientOrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ClientOrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select OrderId, convert(varchar(10),OrderDate,120) as OrderDate ,
                                   ClientId,ProductId,Quantity,Size,Color,Price,PaymentMethod
                              from ClientOrder ";

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
        public JsonResult Post(ClientOrder co)
        {
            string query = @"
                             insert into ClientOrder( OrderDate ,
                                   ClientId,ProductId,Quantity,Size,Color,Price,PaymentMethod)
                             values (@OrderDate, @ClientId,@ProductId,@Quantity,@Size,@Color,@Price,@PaymentMethod)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@OrderDate", co.OrderDate);
                    myCommand.Parameters.AddWithValue("@ClientId", co.ClientId);
                    myCommand.Parameters.AddWithValue("@ProductId", co.ProductId);
                    myCommand.Parameters.AddWithValue("@Quantity", co.Quantity);
                    myCommand.Parameters.AddWithValue("@Size", co.Size);
                    myCommand.Parameters.AddWithValue("@Color", co.Color);
                    myCommand.Parameters.AddWithValue("@Price", co.Price);
                    myCommand.Parameters.AddWithValue("@PaymentMethod", co.PaymentMethod);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(ClientOrder co)
        {
            string query = @"
                           update ClientOrder
                           set OrderDate= @OrderDate,
                            ClientId=@ClientId,
                            ProductId=@ProductId,
                            Quantity=@Quantity,
                            Size=@Size,
                            Color=@Color,
                            Price=@Price,
                            PaymentMethod=@PaymentMethod
                            where OrderId=@OrderId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@OrderId", co.OrderId);
                    myCommand.Parameters.AddWithValue("@OrderDate", co.OrderDate);
                    myCommand.Parameters.AddWithValue("@ClientId", co.ClientId);
                    myCommand.Parameters.AddWithValue("@ProductId", co.ProductId);
                    myCommand.Parameters.AddWithValue("@Quantity", co.Quantity);
                    myCommand.Parameters.AddWithValue("@Size" , co.Size );
                    myCommand.Parameters.AddWithValue("@Color", co.Color);
                    myCommand.Parameters.AddWithValue("@Price", co.Price);
                    myCommand.Parameters.AddWithValue("@PaymentMethod", co.PaymentMethod);

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
                           delete from ClientOrder
                            where OrderId=@OrderId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@OrderId", id);

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
