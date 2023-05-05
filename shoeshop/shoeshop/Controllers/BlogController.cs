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
    public class BlogController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public BlogController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select BlogId,Title,
                                   Author,DatePosted,Content,ImageName
                              from Blog ";

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
        public JsonResult Post(Blog b)
        {
            string query = @"
                             insert into Blog
                             values (@Title, @Author,@DatePosted,@Content,@ImageName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Title", b.Title);
                    myCommand.Parameters.AddWithValue("@Author", b.Author);
                    myCommand.Parameters.AddWithValue("@DatePosted", b.DatePosted);
                    myCommand.Parameters.AddWithValue("@Content", b.Content);
                    myCommand.Parameters.AddWithValue("@ImageName", b.ImageName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Blog b)
        {
            string query = @"
                           update Blog
                           set Title= @Title,
                            Author=@Author,
                            DatePosted=@DatePosted,
                            Content=@Content,
                            ImageName=@ImageName
                            where BlogId=@BlogId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BlogId", b.BlogId);
                    myCommand.Parameters.AddWithValue("@Title", b.Title);
                    myCommand.Parameters.AddWithValue("@Author", b.Author);
                    myCommand.Parameters.AddWithValue("@DatePosted", b.DatePosted);
                    myCommand.Parameters.AddWithValue("@Content", b.Content);
                    myCommand.Parameters.AddWithValue("@ImageName", b.ImageName);
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
                           delete from Blog
                            where BlogId=@BlogId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BlogId", id);

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
