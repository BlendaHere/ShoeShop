using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using shoeshop.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace shoeshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ProductId,ProductName,Brand,Color,Size,
                            Price,Quantity,ImageName
                              from Product ";

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
        public JsonResult Post(Product pro)
        {
            string query = @"
                             insert into Product
                             values (@ProductName,@Brand,@Color,@Size,
                            @Price,@Quantity,@ImageName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProductName", pro.ProductName);
                    myCommand.Parameters.AddWithValue("@Brand", pro.Brand);
                    myCommand.Parameters.AddWithValue("@Color", pro.Color);
                    myCommand.Parameters.AddWithValue("@Size", pro.Size);
                    myCommand.Parameters.AddWithValue("@Price", pro.Price);
                    myCommand.Parameters.AddWithValue("@Quantity", pro.Quantity);
                    myCommand.Parameters.AddWithValue("@ImageName", pro.ImageName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Product pro)
        {
            string query = @"
                           update Product
                           set ProductName= @ProductName,
                            Brand=@Brand,
                            Color=@Color,
                            Size=@Size,
                            Price=@Price,
                            Quantity=@Quantity,
                            ImageName=@ImageName
                            where ProductId=@ProductId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProductId", pro.ProductId);
                    myCommand.Parameters.AddWithValue("@ProductName", pro.ProductName);
                    myCommand.Parameters.AddWithValue("@Brand", pro.Brand);
                    myCommand.Parameters.AddWithValue("@Color", pro.Color);
                    myCommand.Parameters.AddWithValue("@Size", pro.Size);
                    myCommand.Parameters.AddWithValue("@Price", pro.Price);
                    myCommand.Parameters.AddWithValue("@Quantity", pro.Quantity);
                    myCommand.Parameters.AddWithValue("@ImageName", pro.ImageName);
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
                           delete from Product
                            where ProductId=@ProductId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProductId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("Image Added ");
            }
        }


    }

}


