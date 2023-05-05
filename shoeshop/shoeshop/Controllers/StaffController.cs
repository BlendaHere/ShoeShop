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
using System.Collections;


namespace shoeshop.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public StaffController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select StaffId ,FirstName,
                                   LastName,Email,HireDate ,Position ,EmploymentStatus ,ImageName
                              from Staff ";

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
        public JsonResult Post(Staff s)
        {
            string query = @"
                             insert into Staff 
                             values (@FirstName, @LastName,@Email,@HireDate ,@Position ,@EmploymentStatus ,@ImageName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", s.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", s.LastName);
                    myCommand.Parameters.AddWithValue("@Email", s.Email);
                    myCommand.Parameters.AddWithValue("@HireDate ", s.HireDate);
                    myCommand.Parameters.AddWithValue("@Position ", s.Position);
                    myCommand.Parameters.AddWithValue("@EmploymentStatus ", s.EmploymentStatus);
                    myCommand.Parameters.AddWithValue("@ImageName", s.ImageName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Staff s)
        {
            string query = @"
                           update Staff 
                           set FirstName= @FirstName,
                            LastName=@LastName,
                            Email=@Email,
                            HireDate =@HireDate ,
                            Position =@Position ,
                            EmploymentStatus =@EmploymentStatus,
                            ImageName=@ImageName
                            where StaffId =@StaffId 
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StaffId ", s.StaffId);
                    myCommand.Parameters.AddWithValue("@FirstName", s.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", s.LastName);
                    myCommand.Parameters.AddWithValue("@Email", s.Email);
                    myCommand.Parameters.AddWithValue("@HireDate ", s.HireDate);
                    myCommand.Parameters.AddWithValue("@Position ", s.Position);
                    myCommand.Parameters.AddWithValue("@EmploymentStatus ", s.EmploymentStatus);
                    myCommand.Parameters.AddWithValue("@ImageName", s.ImageName);

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
                           delete from Staff
                            where StaffId =@StaffId 
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StaffId ", id);

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


    
