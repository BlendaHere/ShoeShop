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
    public class BrandController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public BrandController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select BrandId,BrandName,
                                   BrandDescription,ImageOfLogo,Website
                              from Brand ";

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
        public JsonResult Post(Brand bra)
        {
            string query = @"
                             insert into Brand
                             values (@BrandName, @BrandDescription,@ImageOfLogo,@Website)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BrandName", bra.BrandName);
                    myCommand.Parameters.AddWithValue("@BrandDescription", bra.BrandDescription);
                    myCommand.Parameters.AddWithValue("@ImageOfLogo", bra.ImageOfLogo);
                    myCommand.Parameters.AddWithValue("@Website", bra.Website);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Brand bra)
        {
            string query = @"
                           update Brand
                           set BrandName= @BrandName,
                            BrandDescription=@BrandDescription,
                            ImageOfLogo=@ImageOfLogo,
                            Website=@Website
                            where BrandId=@BrandId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BrandId", bra.BrandId);
                    myCommand.Parameters.AddWithValue("@BrandName", bra.BrandName);
                    myCommand.Parameters.AddWithValue("@BrandDescription", bra.BrandDescription);
                    myCommand.Parameters.AddWithValue("@ImageOfLogo", bra.ImageOfLogo);
                    myCommand.Parameters.AddWithValue("@Website", bra.Website);
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
                           delete from Brand
                            where BrandId=@BrandId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BrandId", id);

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
