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
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select CategroyId,CategoryName,
                                   CategoryDescription,ImageOfCategory
                              from Category ";

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
        public JsonResult Post(Category cat)
        {
            string query = @"
                             insert into Category
                             values (@CategoryName, @CategoryDescription,@ImageOfCategory)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                    myCommand.Parameters.AddWithValue("@CategoryDescription", cat.CategoryDescription);
                    myCommand.Parameters.AddWithValue("@ImageOfCategory", cat.ImageOfCategory);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Category cat)
        {
            string query = @"
                           update Category
                           set CategoryName= @CategoryName,
                            CategoryDescription=@CategoryDescription,
                            ImageOfCategory=@ImageOfCategory
                            where CategroyId=@CategroyId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CategroyId", cat.CategroyId);
                    myCommand.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                    myCommand.Parameters.AddWithValue("@CategoryDescription", cat.CategoryDescription);
                    myCommand.Parameters.AddWithValue("@ImageOfCategory", cat.ImageOfCategory);
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
                           delete from Category
                            where CategroyId=@CategroyId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CategroyId", id);

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
