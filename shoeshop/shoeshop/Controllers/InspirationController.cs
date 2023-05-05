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
    public class InspirationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public InspirationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select PostId ,Title,
                                   DatePosted,Description ,ImageName
                              from Inspiration ";

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
        public JsonResult Post(Inspiration i)
        {
            string query = @"
                             insert into Inspiration 
                             values (@Title,@DatePosted,@Description ,@ImageName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Title", i.Title);
                    myCommand.Parameters.AddWithValue("@DatePosted", i.DatePosted);
                    myCommand.Parameters.AddWithValue("@Description ", i.Description);
                    myCommand.Parameters.AddWithValue("@ImageName", i.ImageName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Inspiration i)
        {
            string query = @"
                           update Inspiration 
                           set Title= @Title,
                            DatePosted=@DatePosted,
                            Description =@Description ,
                            ImageName=@ImageName
                            where PostId =@PostId 
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PostId ", i.PostId);
                    myCommand.Parameters.AddWithValue("@Title", i.Title);
                    myCommand.Parameters.AddWithValue("@DatePosted", i.DatePosted);
                    myCommand.Parameters.AddWithValue("@Description ", i.Description);
                    myCommand.Parameters.AddWithValue("@ImageName", i.ImageName);
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
                           delete from Inspiration 
                            where PostId =@PostId 
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PostId ", id);

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
